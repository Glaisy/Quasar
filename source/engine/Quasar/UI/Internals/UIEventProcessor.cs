//-----------------------------------------------------------------------
// <copyright file="UIEventProcessor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Quasar.Inputs;
using Quasar.Pipelines;
using Quasar.Rendering;
using Quasar.UI.VisualElements;
using Quasar.UI.VisualElements.Internals;

using Space.Core;
using Space.Core.DependencyInjection;

namespace Quasar.UI.Internals
{
    /// <summary>
    /// Internal component to process all UI events.
    /// </summary>
    /// <seealso cref="IUIEventProcessor" />
    /// <seealso cref="IUIInputEventProcessor" />
    /// <seealso cref="IVisualElementEventProcessor" />
    [Export(typeof(IUIEventProcessor))]
    [Export(typeof(IUIInputEventProcessor))]
    [Export(typeof(IVisualElementEventProcessor))]
    [Singleton]
    internal sealed class UIEventProcessor : IUIEventProcessor, IUIInputEventProcessor, IVisualElementEventProcessor
    {
        private const double DoubleClickTimeLimit = 0.5;


        private readonly ITimeProvider timeProvider;
        private readonly List<VisualElement> loadList = new List<VisualElement>();
        private VisualElement rootVisualElement;
        private VisualElement focusedVisualElement;
        private VisualElement keyDownVisualElement;
        private VisualElement pointerButtonDownVisualElement;
        private VisualElement pointerOverVisualElement;
        private VisualElement lastClickedVisualElement;
        private double lastClickTime;
        private PointerButton pointerButtonDown;


        /// <summary>
        /// Initializes a new instance of the <see cref="UIEventProcessor"/> class.
        /// </summary>
        /// <param name="timeProvider">The time provider.</param>
        public UIEventProcessor(ITimeProvider timeProvider)
        {
            this.timeProvider = timeProvider;
        }


        #region IUIEventProcessor
        /// <inheritdoc/>
        void IUIEventProcessor.ProcessRenderEvent(IRenderingContext context)
        {
            rootVisualElement?.ProcessUpdateEvent();
        }

        /// <inheritdoc/>
        void IUIEventProcessor.ProcessViewportSizeChangedEvent()
        {
            rootVisualElement?.ProcessViewportSizeChangedEvent();
        }

        /// <inheritdoc/>
        void IUIEventProcessor.ProcessUpdateEvent()
        {
            rootVisualElement?.ProcessUpdateEvent();
        }
        #endregion

        #region IUIInputEventProcessor
        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessKeyDownEvent(in KeyEventArgs args)
        {
            if (!EnsureKeyboardFocusIsAssigned())
            {
                return;
            }

            keyDownVisualElement = focusedVisualElement;
            keyDownVisualElement.ProcessKeyDownEvent(args);
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessKeyUpEvent(in KeyEventArgs args)
        {
            if (!EnsureKeyboardFocusIsAssigned())
            {
                return;
            }

            if (keyDownVisualElement == focusedVisualElement)
            {
                keyDownVisualElement.ProcessKeyPressEvent(args);
            }

            keyDownVisualElement?.ProcessKeyUpEvent(args);
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessPointerButtonDownEvent(in PointerButtonEventArgs args)
        {
            if (!IsProcessablePointerEvent())
            {
                return;
            }

            // update focus
            if (focusedVisualElement == null)
            {
                pointerOverVisualElement.Focus();
            }
            else if (focusedVisualElement != pointerOverVisualElement)
            {
                keyDownVisualElement = null;
                focusedVisualElement.ProcessLostFocusEvent();
                focusedVisualElement = pointerOverVisualElement;
                focusedVisualElement.ProcessGotFocusEvent();
            }

            // process pointer down event
            var relativePosition = args.Position - pointerOverVisualElement.AbsolutePosition;
            var visualElementArgs = new PointerButtonEventArgs(args.Button, args.Modifiers, relativePosition);
            pointerButtonDown = args.Button;
            pointerButtonDownVisualElement = pointerOverVisualElement;
            pointerButtonDownVisualElement.ProcessPointerButtonDownEvent(visualElementArgs);
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessPointerButtonUpEvent(in PointerButtonEventArgs args)
        {
            if (!IsProcessablePointerEvent())
            {
                return;
            }

            // send click/pointer up events for the pointed control
            var relativePosition = args.Position - pointerOverVisualElement.AbsolutePosition;
            var visualElementArgs = new PointerButtonEventArgs(args.Button, args.Modifiers, relativePosition);
            if (pointerOverVisualElement == pointerButtonDownVisualElement &&
                args.Button == pointerButtonDown)
            {
                // click or double-click
                if (args.Button == PointerButton.Left)
                {
                    // first send a simple left click event
                    pointerOverVisualElement.ProcessPointerClickEvent(visualElementArgs);

                    if (lastClickedVisualElement == pointerOverVisualElement &&
                        timeProvider.Time - lastClickTime < DoubleClickTimeLimit)
                    {
                        // send double click event
                        pointerOverVisualElement.ProcessPointerDoubleClickEvent(visualElementArgs);
                    }
                    else
                    {
                        // update last click time
                        lastClickedVisualElement = pointerOverVisualElement;
                        lastClickTime = timeProvider.Time;
                    }
                }
                else
                {
                    // not left button click
                    pointerOverVisualElement.ProcessPointerClickEvent(visualElementArgs);
                }
            }

            ClearPointerButtonDownElement();

            // send pointer up event if the pointed control is still active
            pointerOverVisualElement?.ProcessPointerButtonUpEvent(visualElementArgs);
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessPointerEnterEvent(in PointerMoveEventArgs args)
        {
            if (rootVisualElement == null)
            {
                return;
            }

            // check whether the cursor is over the UI
            var visualElement = FindVisualElementAtPosition(rootVisualElement, args.Position);
            if (visualElement == null)
            {
                // update keyboard focus to to the root visual element.
                if (focusedVisualElement == null)
                {
                    PropagateInputFocus(rootVisualElement);
                }

                return;
            }

            // update pointer over visual element
            Assertion.ThrowIfNotEqual(
                pointerOverVisualElement == null,
                true,
                $"{nameof(pointerOverVisualElement)} should have been null.");

            pointerOverVisualElement = visualElement;
            pointerOverVisualElement.PointerOver = true;

            // update keyboard focus
            if (focusedVisualElement == null)
            {
                PropagateInputFocus(pointerOverVisualElement);
            }
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessPointerLeaveEvent()
        {
            ClearAllStates();
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessPointerMoveEvent(in PointerMoveEventArgs args)
        {
            if (rootVisualElement == null)
            {
                return;
            }

            // update pointer over control
            var visualElement = FindVisualElementAtPosition(rootVisualElement, args.Position);
            if (visualElement == null)
            {
                ClearPointerOverElement();
                ClearPointerButtonDownElement();
                return;
            }

            if (pointerOverVisualElement != visualElement)
            {
                if (pointerOverVisualElement != null)
                {
                    pointerOverVisualElement.PointerOver = false;
                }

                ClearPointerButtonDownElement();
                pointerOverVisualElement = visualElement;
                pointerOverVisualElement.PointerOver = true;
            }

            // send pointer move event to the pointed control
            var relativePosition = args.Position - pointerOverVisualElement.AbsolutePosition;
            pointerOverVisualElement.ProcessPointerMoveEvent(new PointerMoveEventArgs(relativePosition));
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessPointerWheelEvent(in PointerWheelEventArgs args)
        {
            if (!IsProcessablePointerEvent())
            {
                return;
            }

            pointerOverVisualElement.ProcessPointerWheelEvent(args);
        }
        #endregion

        #region IVisualElementEventProcessor
        /// <inheritdoc/>
        void IVisualElementEventProcessor.AddToLoadList(VisualElement visualElement)
        {
            Assertion.ThrowIfNull(visualElement, nameof(visualElement));

            lock (loadList)
            {
                loadList.Add(visualElement);
            }
        }

        /// <inheritdoc/>
        void IVisualElementEventProcessor.ProcessFocusChanged(VisualElement visualElement)
        {
            Assertion.ThrowIfNull(visualElement, nameof(visualElement));
            Assertion.ThrowIfNotEqual(focusedVisualElement != visualElement, true, nameof(visualElement));

            keyDownVisualElement = null;
            if (focusedVisualElement != null)
            {
                focusedVisualElement.ProcessLostFocusEvent();
            }

            focusedVisualElement = visualElement;
            focusedVisualElement.ProcessGotFocusEvent();
        }

        /// <inheritdoc/>
        void IVisualElementEventProcessor.ProcessUpdate()
        {
            ProcessLoadList();

            if (rootVisualElement == null)
            {
                return;
            }

            rootVisualElement.ProcessStyleAnPseudoClassChanges();
            rootVisualElement.ProcessSizeChanges();
            rootVisualElement.ProcessLayoutChanges();
            rootVisualElement.ProcessUpdateEvent();
        }

        /// <inheritdoc/>
        void IVisualElementEventProcessor.ProcessRootVisualElementChanged(VisualElement visualElement)
        {
            rootVisualElement = visualElement;
            ClearAllStates();
        }

        /// <inheritdoc/>
        void IVisualElementEventProcessor.RemoveFromLoadList(VisualElement visualElement)
        {
            Assertion.ThrowIfNull(visualElement, nameof(visualElement));

            lock (loadList)
            {
                loadList.Remove(visualElement);
            }
        }
        #endregion


        private void ClearAllStates()
        {
            ClearFocus();
            ClearPointerOverElement();
            ClearPointerButtonDownElement();
        }

        private void ClearFocus()
        {
            keyDownVisualElement = null;
            if (focusedVisualElement == null)
            {
                return;
            }

            focusedVisualElement.ProcessLostFocusEvent();
            focusedVisualElement = null;
        }

        private void ClearPointerButtonDownElement()
        {
            pointerButtonDownVisualElement = lastClickedVisualElement = null;
            pointerButtonDown = PointerButton.None;
            lastClickTime = 0.0;
        }

        private void ClearPointerOverElement()
        {
            if (pointerOverVisualElement == null)
            {
                return;
            }

            pointerOverVisualElement.PointerOver = false;
            pointerOverVisualElement = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool EnsureKeyboardFocusIsAssigned()
        {
            if (focusedVisualElement != null)
            {
                return true;
            }

            Assertion.ThrowIfNotEqual(keyDownVisualElement == null, true, $"{nameof(keyDownVisualElement)} should have been null.");
            focusedVisualElement = rootVisualElement;
            return focusedVisualElement != null;
        }

        private static VisualElement FindVisualElementAtPosition(VisualElement visualElement, Vector2 absolutePosition)
        {
            if (!visualElement.IsVisible)
            {
                return null;
            }

            // skip the rest of the tree if the position is not inside the current control
            var relativePosition = absolutePosition - visualElement.AbsolutePosition;
            if (!IsPositionInsideVisualElement(visualElement, relativePosition))
            {
                return null;
            }

            // check child hierarchy
            for (var i = visualElement.Children.Count - 1; i >= 0; i--)
            {
                var childElement = visualElement.Children[i];
                var pointedChildElement = FindVisualElementAtPosition(childElement, absolutePosition);
                if (pointedChildElement != null)
                {
                    return pointedChildElement;
                }
            }

            // the visual element itself is under the pointer
            return visualElement;
        }

        private static bool IsPositionInsideVisualElement(VisualElement visualElement, Vector2 position)
        {
            return position.X >= 0 &&
                position.Y >= 0 &&
                position.X < visualElement.PaddingBox.Size.X &&
                position.Y < visualElement.PaddingBox.Size.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsProcessablePointerEvent()
        {
            if (pointerOverVisualElement != null &&
                pointerOverVisualElement.IsEnabled)
            {
                return true;
            }

            Assertion.ThrowIfNotEqual(
                pointerButtonDownVisualElement == null,
                true,
                $"{nameof(pointerButtonDownVisualElement)} should have been null.");
            return false;
        }

        private void ProcessLoadList()
        {
            VisualElement visualElement = null;
            while (true)
            {
                lock (loadList)
                {
                    if (loadList.Count == 0)
                    {
                        break;
                    }

                    visualElement = loadList[^1];
                    loadList.RemoveAt(loadList.Count - 1);
                }

                visualElement.ProcessLoadEvent();
            }
        }

        private void PropagateInputFocus(VisualElement visualElement)
        {
            while (visualElement != null)
            {
                if (visualElement.Focus())
                {
                    focusedVisualElement = visualElement;
                    return;
                }

                visualElement = visualElement.Parent;
            }
        }
    }
}
