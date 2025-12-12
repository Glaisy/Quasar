//-----------------------------------------------------------------------
// <copyright file="InputEventProcessor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Quasar.UI.Internals;

using Space.Core;
using Space.Core.DependencyInjection;
using Space.Core.Mathematics;

namespace Quasar.Inputs.Internals
{
    /// <summary>
    /// Input event processor and input state implementation.
    /// </summary>
    /// <seealso cref="IInputEventProcessor" />
    /// <seealso cref="IInputState" />
    [Export(typeof(IInputEventProcessor))]
    [Export(typeof(IInputState))]
    [Singleton]
    internal sealed class InputEventProcessor : IInputEventProcessor, IInputState
    {
        private const float DefaultPointerMovementFilter = 0.5f;
        private const float MaxPointerMovementFiltering = 0.9f;


        private readonly IUIInputEventProcessor uiInputEventProcessor;
        private readonly bool[] keyDownStates = new bool[(int)KeyCode.KeyCount];
        private readonly bool[] pointerButtonDownStates = new bool[(int)PointerButton.ButtonCount];
        private readonly Dictionary<long, HotkeyBinding> hotkeyBindings = new Dictionary<long, HotkeyBinding>();
        private KeyModifiers activeModifiers = KeyModifiers.None;
        private Vector2 cummulatedPointerMovement;
        private float cummulatedPointerWheelMovement;
        private Vector2 pointerPosition;
        private Vector2 pointerMovement;
        private Vector2 pointerMovementFiltered;
        private float pointerWheelMovement;
        private float adjustedPointerMovementFilter;


        /// <summary>
        /// Initializes a new instance of the <see cref="InputEventProcessor" /> class.
        /// </summary>
        /// <param name="uiInputEventProcessor">The UI input event processor.</param>
        public InputEventProcessor(IUIInputEventProcessor uiInputEventProcessor)
        {
            this.uiInputEventProcessor = uiInputEventProcessor;

            PointerMovementFilter = DefaultPointerMovementFilter;
        }


        #region IInputState
        /// <inheritdoc/>
        public bool this[KeyCode key] => keyDownStates[(int)key];

        /// <inheritdoc/>
        public bool this[PointerButton pointerButton] => pointerButtonDownStates[(int)pointerButton];


        private float pointerMovementFilter;
        /// <inheritdoc/>
        public float PointerMovementFilter
        {
            get => pointerMovementFilter;
            set
            {
                value = Ranges.FloatUnit.Clamp(value);
                if (pointerMovementFilter == value)
                {
                    return;
                }

                pointerMovementFilter = value;
                adjustedPointerMovementFilter = MaxPointerMovementFiltering * value;
            }
        }


        /// <inheritdoc/>
        public IDisposable BindHotkey(Action handler, KeyCode[] keyCodes)
        {
            var id = GenerateHotKeyId(keyCodes);

            lock (hotkeyBindings)
            {
                if (!hotkeyBindings.TryGetValue(id, out var hotkeyBinding))
                {
                    hotkeyBinding = new HotkeyBinding(id, keyCodes, RemoveHotkeyBinding);
                    hotkeyBindings.Add(id, hotkeyBinding);
                }

                hotkeyBinding.Handler = handler;
                return hotkeyBinding;
            }
        }

        /// <inheritdoc/>
        public KeyModifiers GetActiveModifiers()
        {
            return activeModifiers;
        }

        /// <inheritdoc/>
        public Vector2 GetPointerMovementFiltered()
        {
            return pointerMovementFiltered;
        }

        /// <inheritdoc/>
        public Vector2 GetPointerMovement()
        {
            return pointerMovement;
        }

        /// <inheritdoc/>
        public float GetPointerWheelMovement()
        {
            return pointerWheelMovement;
        }

        /// <inheritdoc/>
        public void Update()
        {
            pointerMovement = cummulatedPointerMovement;
            pointerMovementFiltered = FilterValue(pointerMovementFiltered, cummulatedPointerMovement, adjustedPointerMovementFilter);

            pointerWheelMovement = cummulatedPointerWheelMovement;
            cummulatedPointerMovement = Vector2.Zero;
            cummulatedPointerWheelMovement = 0.0f;
        }
        #endregion

        #region IInputEventProcessor
        /// <inheritdoc/>
        void IInputEventProcessor.ProcessKeyDown(KeyCode keyCode, char character)
        {
            keyDownStates[(int)keyCode] = true;
            activeModifiers |= GetKeyModifierForKey(keyCode);

            EvaluateHotkeys();

            var args = new KeyEventArgs(keyCode, character, activeModifiers);
            uiInputEventProcessor.ProcessKeyDown(args);
        }

        /// <inheritdoc/>
        void IInputEventProcessor.ProcessKeyUp(KeyCode keyCode, char character)
        {
            keyDownStates[(int)keyCode] = false;
            activeModifiers &= ~GetKeyModifierForKey(keyCode);

            var args = new KeyEventArgs(keyCode, character, activeModifiers);
            uiInputEventProcessor.ProcessKeyUp(args);
        }

        /// <inheritdoc/>
        void IInputEventProcessor.ProcessPointerButtonDown(PointerButton button)
        {
            pointerButtonDownStates[(int)button] = true;

            var args = new PointerButtonEventArgs(button, activeModifiers, pointerPosition);
            uiInputEventProcessor.ProcessPointerButtonDown(args);
        }

        /// <inheritdoc/>
        void IInputEventProcessor.ProcessPointerButtonUp(PointerButton button)
        {
            pointerButtonDownStates[(int)button] = false;

            var args = new PointerButtonEventArgs(button, activeModifiers, pointerPosition);
            uiInputEventProcessor.ProcessPointerButtonUp(args);
        }

        /// <inheritdoc/>
        void IInputEventProcessor.ProcessPointerEnter(Vector2 position)
        {
            pointerPosition = position;
            pointerMovement = Vector2.Zero;
            pointerMovementFiltered = Vector2.Zero;
            cummulatedPointerMovement = Vector2.Zero;

            uiInputEventProcessor.ProcessPointerEnter(new PointerMoveEventArgs(pointerPosition));
        }

        /// <inheritdoc/>
        void IInputEventProcessor.ProcessPointerLeave()
        {
            pointerPosition = Vector2.Zero;
            pointerMovement = Vector2.Zero;
            pointerMovementFiltered = Vector2.Zero;
            cummulatedPointerMovement = Vector2.Zero;

            uiInputEventProcessor.ProcessPointerLeave();
        }

        /// <inheritdoc/>
        void IInputEventProcessor.ProcessPointerMove(Vector2 position)
        {
            var delta = position - pointerPosition;
            cummulatedPointerMovement += delta;
            pointerPosition = position;

            uiInputEventProcessor.ProcessPointerMove(new PointerMoveEventArgs(pointerPosition));
        }

        /// <inheritdoc/>
        void IInputEventProcessor.ProcessPointerWheel(float delta)
        {
            cummulatedPointerWheelMovement += delta;

            uiInputEventProcessor.ProcessPointerWheel(new PointerWheelEventArgs(delta));
        }
        #endregion


        private void EvaluateHotkeys()
        {
            HotkeyBinding hotkeyBinding = null;
            Action handler = null;
            lock (hotkeyBindings)
            {
                hotkeyBinding = FindActiveHotkey();
                handler = hotkeyBinding?.Handler;
            }

            if (hotkeyBinding != null)
            {
                handler();
            }
        }

        private static Vector2 FilterValue(Vector2 previousValue, Vector2 newValue, float filter)
        {
            return new Vector2(
                MathematicsHelper.LerpUnclamped(newValue.X, previousValue.X, filter),
                MathematicsHelper.LerpUnclamped(newValue.Y, previousValue.Y, filter));
        }

        private HotkeyBinding FindActiveHotkey()
        {
            foreach (var hotkeyBinding in hotkeyBindings.Values)
            {
                // find matching keys
                var keyCodes = hotkeyBinding.KeyCodes;
                var matchingKeys = 0;
                for (var j = 0; j < keyCodes.Length; j++)
                {
                    if (keyDownStates[(int)keyCodes[j]])
                    {
                        matchingKeys++;
                    }
                }

                // all keys matched?
                if (matchingKeys == keyCodes.Length)
                {
                    return hotkeyBinding;
                }
            }

            return null;
        }

        private static long GenerateHotKeyId(KeyCode[] keyCodes)
        {
            Array.Sort(keyCodes, (a, b) => a.CompareTo(b));

            var id = 0L;
            for (var i = 0; i < keyCodes.Length; i++)
            {
                id += (long)keyCodes[i];
                id <<= 7;
            }

            return id;
        }

        private static KeyModifiers GetKeyModifierForKey(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.Alt:
                    return KeyModifiers.Alt;
                case KeyCode.Control:
                    return KeyModifiers.Control;
                case KeyCode.Shift:
                    return KeyModifiers.Shift;
                default:
                    return KeyModifiers.None;
            }
        }

        private void RemoveHotkeyBinding(HotkeyBinding hotkeyBinding)
        {
            lock (hotkeyBindings)
            {
                hotkeyBindings.Remove(hotkeyBinding.Id);
            }
        }
    }
}
