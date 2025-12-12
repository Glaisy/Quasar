//-----------------------------------------------------------------------
// <copyright file="IInputEventProcessor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Inputs.Internals
{
    /// <summary>
    /// Represents the input event processor component.
    /// Input events are consumed by the implementations of this interface.
    /// </summary>
    internal interface IInputEventProcessor
    {
        /// <summary>
        /// Processes the key is pressed down event.
        /// </summary>
        /// <param name="keyCode">The key code.</param>
        /// <param name="character">The character code.</param>
        void ProcessKeyDown(KeyCode keyCode, char character);

        /// <summary>
        /// Processes the key is released event.
        /// </summary>
        /// <param name="keyCode">Processes the key code.</param>
        /// <param name="character">The character code.</param>
        void ProcessKeyUp(KeyCode keyCode, char character);

        /// <summary>
        /// Processes the pointer button is pressed down event.
        /// </summary>
        /// <param name="button">The pointer button.</param>
        void ProcessPointerButtonDown(PointerButton button);

        /// <summary>
        /// Processes the pointer button is released event.
        /// </summary>
        /// <param name="button">The pointer button.</param>
        void ProcessPointerButtonUp(PointerButton button);

        /// <summary>
        /// Processes the pointer enters the window area event.
        /// </summary>
        /// <param name="position">The position.</param>
        void ProcessPointerEnter(Vector2 position);

        /// <summary>
        /// Processes the pointer leaves the window area event.
        /// </summary>
        void ProcessPointerLeave();

        /// <summary>
        /// Processes the pointer is moved event.
        /// </summary>
        /// <param name="position">The position.</param>
        void ProcessPointerMove(Vector2 position);

        /// <summary>
        /// Processes the pointer wheel is scrolled event.
        /// </summary>
        /// <param name="delta">The position delta.</param>
        void ProcessPointerWheel(float delta);
    }
}
