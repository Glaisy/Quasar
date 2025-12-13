//-----------------------------------------------------------------------
// <copyright file="InputService.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core.DependencyInjection;

namespace Quasar.Inputs.Internals
{
    /// <summary>
    /// Input service implementation.
    /// </summary>
    /// <seealso cref="IInputService" />
    [Export(typeof(IInputService))]
    [Singleton]
    internal sealed class InputService : IInputService
    {
        private readonly IInputState inputState;
        private readonly IInputEventProcessor inputEventProcessor;


        /// <summary>
        /// Initializes a new instance of the <see cref="InputService"/> class.
        /// </summary>
        /// <param name="inputState">State of the input.</param>
        /// <param name="inputEventProcessor">The input event processor.</param>
        public InputService(IInputState inputState, IInputEventProcessor inputEventProcessor)
        {
            this.inputState = inputState;
            this.inputEventProcessor = inputEventProcessor;
        }


        /// <inheritdoc/>
        public float PointerMovementFilter
        {
            get => inputState.PointerMovementFilter;
            set => inputState.PointerMovementFilter = value;
        }


        /// <inheritdoc/>
        public IDisposable BindHotkey(Action handler, params KeyCode[] keyCodes)
        {
            ArgumentNullException.ThrowIfNull(handler, nameof(handler));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(keyCodes.Length, nameof(keyCodes.Length));

            return inputState.BindHotkey(handler, keyCodes);
        }

        /// <inheritdoc/>
        public KeyModifiers GetActiveModifiers()
        {
            return inputState.GetActiveModifiers();
        }

        /// <inheritdoc/>
        public bool GetKeyDown(KeyCode key)
        {
            return inputState[key];
        }

        /// <inheritdoc/>
        public bool GetKeyUp(KeyCode key)
        {
            return !inputState[key];
        }

        /// <inheritdoc/>
        public bool GetPointerButtonDown(PointerButton button)
        {
            return inputState[button];
        }

        /// <inheritdoc/>
        public bool GetPointerButtonUp(PointerButton button)
        {
            return !inputState[button];
        }

        /// <inheritdoc/>
        public Vector2 GetPointerMovementFiltered()
        {
            return inputState.GetPointerMovementFiltered();
        }

        /// <inheritdoc/>
        public Vector2 GetPointerMovement()
        {
            return inputState.GetPointerMovement();
        }

        /// <inheritdoc/>
        public float GetPointerWheelMovement()
        {
            return inputState.GetPointerWheelMovement();
        }
    }
}
