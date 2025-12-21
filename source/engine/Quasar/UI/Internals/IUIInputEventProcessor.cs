//-----------------------------------------------------------------------
// <copyright file="IUIInputEventProcessor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Inputs;

namespace Quasar.UI.Internals
{
    /// <summary>
    /// Represent a UI component to process input events.
    /// </summary>
    internal interface IUIInputEventProcessor
    {
        /// <summary>
        /// Processes the event when a key is pressed down.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        void ProcessKeyDownEvent(in KeyEventArgs args);

        /// <summary>
        /// Processes the event when a key is released.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        void ProcessKeyUpEvent(in KeyEventArgs args);

        /// <summary>
        /// Processes the event when a pointer button is pressed down.
        /// </summary>
        /// <param name="args">The <see cref="PointerButtonEventArgs"/> instance containing the event data.</param>
        void ProcessPointerButtonDownEvent(in PointerButtonEventArgs args);

        /// <summary>
        /// Processes the event when a pointer button is released.
        /// </summary>
        /// <param name="args">The <see cref="PointerButtonEventArgs"/> instance containing the event data.</param>
        void ProcessPointerButtonUpEvent(in PointerButtonEventArgs args);

        /// <summary>
        /// Processes the event when pointer cursor enters the application area.
        /// </summary>
        /// <param name="args">The <see cref="PointerMoveEventArgs"/> instance containing the event data.</param>
        void ProcessPointerEnterEvent(in PointerMoveEventArgs args);

        /// <summary>
        /// Processes the event when pointer cursor leaves the application area.
        /// </summary>
        void ProcessPointerLeaveEvent();

        /// <summary>
        /// Processes the event when the pointer is moved.
        /// </summary>
        /// <param name="args">The <see cref="PointerMoveEventArgs"/> instance containing the event data.</param>
        void ProcessPointerMoveEvent(in PointerMoveEventArgs args);

        /// <summary>
        /// Processes the event when the pointer wheel is scrolled.
        /// </summary>
        /// <param name="args">The <see cref="PointerWheelEventArgs"/> instance containing the event data.</param>
        void ProcessPointerWheelEvent(in PointerWheelEventArgs args);
    }
}
