//-----------------------------------------------------------------------
// <copyright file="VisualElement.Events.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Inputs;
using Quasar.UI.VisualElements.Internals;
using Quasar.UI.VisualElements.Styles;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Represents a basic UI visual element - Events.
    /// </summary>
    public partial class VisualElement
    {
        /// <summary>
        /// The process render event handler.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        internal void OnProcessRenderEvent(Canvas canvas)
        {
            if (Visibility != Visibility.Visible ||
                Display != DisplayMode.Display)
            {
                return;
            }

            OnRender(canvas);

            // propagate render event through the hierarchy
            foreach (var child in children)
            {
                child.OnProcessRenderEvent(canvas);
            }
        }

        /// <summary>
        /// The process update event handler.
        /// </summary>
        internal void OnProcessUpdateEvent()
        {
            // invoke custom update event handler
            OnUpdate();

            // propagate update event through the hierarchy
            foreach (var child in children)
            {
                child.OnProcessUpdateEvent();
            }
        }


        /// <summary>
        /// Got focus event handler.
        /// </summary>
        protected virtual void OnGotFocus()
        {
        }

        /// <summary>
        /// Key down event handler.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        protected virtual void OnKeyDown(in KeyEventArgs args)
        {
        }

        /// <summary>
        /// Key press event handler.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        protected virtual void OnKeyPress(in KeyEventArgs args)
        {
        }

        /// <summary>
        /// Key up event handler.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        protected virtual void OnKeyUp(in KeyEventArgs args)
        {
        }

        /// <summary>
        /// Load event handler. Invoked before the first update event of the visual element.
        /// </summary>
        protected virtual void OnLoad()
        {
        }

        /// <summary>
        /// Lost focus event handler.
        /// </summary>
        protected virtual void OnLostFocus()
        {
        }

        /// <summary>
        /// Pointer button down event handler.
        /// </summary>
        /// <param name="args">The <see cref="PointerButtonEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPointerButtonDown(in PointerButtonEventArgs args)
        {
        }

        /// <summary>
        /// Pointer button up event handler.
        /// </summary>
        /// <param name="args">The <see cref="PointerButtonEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPointerButtonUp(in PointerButtonEventArgs args)
        {
        }

        /// <summary>
        /// Pointer click event handler.
        /// </summary>
        /// <param name="args">The <see cref="PointerButtonEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPointerClick(in PointerButtonEventArgs args)
        {
        }

        /// <summary>
        /// Pointer double click event handler.
        /// </summary>
        /// <param name="args">The <see cref="PointerButtonEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPointerDoubleClick(in PointerButtonEventArgs args)
        {
        }

        /// <summary>
        /// Pointer enter event handler.
        /// </summary>
        protected virtual void OnPointerEnter()
        {
        }

        /// <summary>
        /// Pointer leave event handler.
        /// </summary>
        protected virtual void OnPointerLeave()
        {
        }

        /// <summary>
        /// Pointer move event handler.
        /// </summary>
        /// <param name="args">The <see cref="PointerMoveEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPointerMove(in PointerMoveEventArgs args)
        {
        }

        /// <summary>
        /// Pointer wheel event handler.
        /// </summary>
        /// <param name="args">The <see cref="PointerWheelEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPointerWheel(in PointerWheelEventArgs args)
        {
        }

        /// <summary>
        /// The render event handler.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        protected virtual void OnRender(ICanvas canvas)
        {
        }

        /// <summary>
        /// Unload event handler. Invoked before visual element is to be disposed.
        /// </summary>
        protected virtual void OnUnload()
        {
        }

        /// <summary>
        /// Update event handler.
        /// </summary>
        protected virtual void OnUpdate()
        {
        }
    }
}
