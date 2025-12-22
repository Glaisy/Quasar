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

using System;
using System.Runtime.CompilerServices;

using Quasar.Inputs;
using Quasar.UI.VisualElements.Internals;
using Quasar.UI.VisualElements.Styles;
using Quasar.UI.VisualElements.Themes;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Represents a basic UI visual element - Events.
    /// </summary>
    public partial class VisualElement
    {
        /// <summary>
        /// Executes the got focus event processing.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessGotFocusEvent()
        {
            OnGotFocus();
        }

        /// <summary>
        /// Executes the key down event processing.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessKeyDownEvent(in KeyEventArgs args)
        {
            OnKeyDown(args);
        }

        /// <summary>
        /// Executes the key press event processing.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessKeyPressEvent(in KeyEventArgs args)
        {
            OnKeyPress(args);
        }

        /// <summary>
        /// Executes the key up event processing.
        /// </summary>
        /// <param name="args">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessKeyUpEvent(in KeyEventArgs args)
        {
            OnKeyUp(args);
        }

        /// <summary>
        /// Executes the layout change processing.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessLayoutChanges()
        {
            // update visual layou
            if (HasInvalidationFlags(InvalidationFlags.Layout))
            {
                layoutManager.Arrange(this);
                ClearInvalidationFlags(InvalidationFlags.Layout);
            }

            // update content alignment
            if (HasInvalidationFlags(InvalidationFlags.ContentAlignment))
            {
                UpdateContentAlignment();
                ClearInvalidationFlags(InvalidationFlags.ContentAlignment);
            }

            // propagate layout change processing through the hierarchy
            foreach (var child in children)
            {
                child.ProcessLayoutChanges();
            }
        }

        /// <summary>
        /// Executes the load event processing.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessLoadEvent()
        {
            OnLoad();
        }

        /// <summary>
        /// Executes the lost focus event processing.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessLostFocusEvent()
        {
            OnLostFocus();
        }

        /// <summary>
        /// Executes the optimized update events processing.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessOptimizedUpdateEvents()
        {
            if ((mergedInvalidationFlags & (InvalidationFlags.Styles | InvalidationFlags.PseudoClass)) != 0)
            {
                ProcessStyleAnPseudoClassChanges();
            }

            if ((mergedInvalidationFlags & InvalidationFlags.PreferredSize) != 0)
            {
                ProcessPreferredSizeChanges();
            }

            if ((mergedInvalidationFlags & InvalidationFlags.Layout) != 0)
            {
                ProcessLayoutChanges();
            }

            mergedInvalidationFlags = 0;

            ProcessUpdateEvent();
        }

        /// <summary>
        /// Executes the pointer button down event processing.
        /// </summary>
        /// <param name="args">The <see cref="PointerButtonEventArgs"/> instance containing the event data.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessPointerButtonDownEvent(in PointerButtonEventArgs args)
        {
            OnPointerButtonDown(args);
        }

        /// <summary>
        /// Executes the pointer button up event processing.
        /// </summary>
        /// <param name="args">The <see cref="PointerButtonEventArgs"/> instance containing the event data.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessPointerButtonUpEvent(in PointerButtonEventArgs args)
        {
            OnPointerButtonUp(args);
        }

        /// <summary>
        /// Executes the pointer click event processing.
        /// </summary>
        /// <param name="args">The <see cref="PointerButtonEventArgs"/> instance containing the event data.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessPointerClickEvent(in PointerButtonEventArgs args)
        {
            OnPointerClick(args);
        }

        /// <summary>
        /// Executes the pointer double click event processing.
        /// </summary>
        /// <param name="args">The <see cref="PointerButtonEventArgs"/> instance containing the event data.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessPointerDoubleClickEvent(in PointerButtonEventArgs args)
        {
            OnPointerDoubleClick(args);
        }

        /// <summary>
        /// Executes the pointer move event processing.
        /// </summary>
        /// <param name="args">The <see cref="PointerButtonEventArgs"/> instance containing the event data.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessPointerMoveEvent(in PointerMoveEventArgs args)
        {
            OnPointerMove(args);
        }

        /// <summary>
        /// Executes the pointer wheel event processing.
        /// </summary>
        /// <param name="args">The <see cref="PointerWheelEventArgs"/> instance containing the event data.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessPointerWheelEvent(in PointerWheelEventArgs args)
        {
            OnPointerWheel(args);
        }

        /// <summary>
        /// Executes the style and pseudo class change processing.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessStyleAnPseudoClassChanges()
        {
            ITheme theme = null;
            var shouldResolve = false;

            // update styles
            if (HasInvalidationFlags(InvalidationFlags.Styles))
            {
                theme = themeProvider.CurrentTheme;
                MergeStyles(theme);
                ClearInvalidationFlags(InvalidationFlags.Styles);
                shouldResolve = true;
            }

            // update pseudo class
            if (HasInvalidationFlags(InvalidationFlags.PseudoClass))
            {
                pseudoClass = GetPseudoClass();
                theme ??= themeProvider.CurrentTheme;
                MergePseudoClassStyle(theme);
                shouldResolve = true;
                ClearInvalidationFlags(InvalidationFlags.PseudoClass);
            }

            // resolve style
            if (shouldResolve)
            {
                ResolveStyleProperties();
            }

            // propagate update event through the hierarchy
            foreach (var child in children)
            {
                child.ProcessStyleAnPseudoClassChanges();
            }
        }

        /// <summary>
        /// Executes the render event processing.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessRenderEvent(Canvas canvas)
        {
            if (Visibility != Visibility.Visible ||
                Display != DisplayStyle.Display)
            {
                return;
            }

            canvas.Offset = CanvasPosition;
            OnRender(canvas);

            // propagate render event through the hierarchy
            foreach (var child in children)
            {
                child.ProcessRenderEvent(canvas);
            }
        }

        /// <summary>
        /// Executes the preferred size change processing.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessPreferredSizeChanges()
        {
            // propagate size change processing through the hierarchy
            foreach (var child in children)
            {
                child.ProcessPreferredSizeChanges();
            }

            // update preferred size of this visual element
            if (HasInvalidationFlags(InvalidationFlags.PreferredSize))
            {
                PreferredSize = layoutManager.CalculatePreferredBoundingBoxSize(this);
                ClearInvalidationFlags(InvalidationFlags.PreferredSize);
            }
        }

        /// <summary>
        /// Executes the update event processing.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessUpdateEvent()
        {
            // invoke custom update event handler
            OnUpdate();

            // propagate update event through the hierarchy
            foreach (var child in children)
            {
                child.ProcessUpdateEvent();
            }
        }

        /// <summary>
        /// Executes the viewport changed event processing (NOTE: root visual element only).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessViewportSizeChangedEvent()
        {
            Invalidate(InvalidationFlags.Layout);
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
            if (background.Texture != null)
            {
                canvas.DrawSprite(background, Vector2.Zero, PaddingBox.Size, backgroundColor);
            }

            if (!String.IsNullOrEmpty(text))
            {
                canvas.DrawText(text, font, textPosition, Color);
            }
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
