//-----------------------------------------------------------------------
// <copyright file="IInputService.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Inputs
{
    /// <summary>
    /// Input service interface definition.
    /// </summary>
    public interface IInputService
    {
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
        IDisposable BindHotkey(Action handler, params KeyCode[] keyCodes);

        /// <summary>
        /// Gets the active modifiers.
        /// </summary>
        KeyModifiers GetActiveModifiers();

        /// <summary>
        /// Gets the key down state.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True if the key is pressed otherwise false.</returns>
        bool GetKeyDown(KeyCode key);

        /// <summary>
        /// Gets the key up state.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True if the key is released otherwise false.</returns>
        bool GetKeyUp(KeyCode key);

        /// <summary>
        /// Gets the pointer button down state.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>True if the button is pressed otherwise false.</returns>
        bool GetPointerButtonDown(PointerButton button);

        /// <summary>
        /// Gets the pointer button up state.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>True if the button is released otherwise false.</returns>
        bool GetPointerButtonUp(PointerButton button);

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
    }
}
