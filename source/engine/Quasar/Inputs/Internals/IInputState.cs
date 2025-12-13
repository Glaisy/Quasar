//-----------------------------------------------------------------------
// <copyright file="IInputState.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Inputs.Internals
{
    /// <summary>
    /// Represents the internal state of the inputs.
    /// </summary>
    internal interface IInputState
    {
        /// <summary>
        /// Gets the down (pressed) state of the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        bool this[KeyCode key] { get; }

        /// <summary>
        /// Gets the down (pressed) state of the specified pointer button.
        /// </summary>
        /// <param name="pointerButton">The pointer button.</param>
        bool this[PointerButton pointerButton] { get; }

        /// <summary>
        /// Gets or sets the pointer movement filter value (default: 0.5) [0...1].
        /// 0.0 - no filtering.
        /// 1.0 - maximum filtering.
        /// </summary>
        float PointerMovementFilter { get; set; }


        /// <summary>
        /// Binds a hotkey.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="keyCodes">The key codes.</param>
        /// <returns>The hotkey binding.</returns>
        IDisposable BindHotkey(Action handler, KeyCode[] keyCodes);

        /// <summary>
        /// Gets the keyboard active modifiers.
        /// </summary>
        KeyModifiers GetActiveModifiers();

        /// <summary>
        /// Gets the filtered pointer movement during the last update frame.
        /// </summary>
        /// <returns>The filtered pointer movement.</returns>
        Vector2 GetPointerMovementFiltered();

        /// <summary>
        /// Gets the raw pointer movement during the last update frame.
        /// </summary>
        /// <returns>The pointer movement.</returns>
        Vector2 GetPointerMovement();

        /// <summary>
        /// Gets the pointer wheel movement during the last update frame.
        /// </summary>
        /// <returns>The signed pointer wheel movement.</returns>
        float GetPointerWheelMovement();

        /// <summary>
        /// Updates the internal state.
        /// </summary>
        void Update();
    }
}
