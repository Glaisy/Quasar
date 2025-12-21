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

using System.Runtime.CompilerServices;

using Quasar.Graphics;
using Quasar.Inputs;
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
        private VisualElement rootVisualElement;
        private VisualElement focusedVisualElement;
        private VisualElement keyDownVisualElement;


        #region IUIEventProcessor
        /// <inheritdoc/>
        void IUIEventProcessor.Initialize()
        {
        }

        /// <inheritdoc/>
        void IUIEventProcessor.ProcessRenderEvent(IRenderingContext context)
        {
        }

        /// <inheritdoc/>
        void IUIEventProcessor.ProcessSizeChangedEvent(in Size size)
        {
        }

        /// <inheritdoc/>
        void IUIEventProcessor.ProcessUpdateEvent()
        {
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
            ////keyDownVisualElement.ProcessKeyDownEvent(args);
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessKeyUpEvent(in KeyEventArgs args)
        {
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessPointerButtonDownEvent(in PointerButtonEventArgs args)
        {
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessPointerButtonUpEvent(in PointerButtonEventArgs args)
        {
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessPointerEnterEvent(in PointerMoveEventArgs args)
        {
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessPointerLeaveEvent()
        {
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessPointerMoveEvent(in PointerMoveEventArgs args)
        {
        }

        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessPointerWheelEvent(in PointerWheelEventArgs args)
        {
        }
        #endregion

        #region IVisualElementEventProcessor
        /// <inheritdoc/>
        void IVisualElementEventProcessor.AddToLoadList(VisualElement visualElement)
        {
        }

        /// <inheritdoc/>
        void IVisualElementEventProcessor.RemoveFromLoadList(VisualElement visualElement)
        {
        }

        /// <inheritdoc/>
        void IVisualElementEventProcessor.ProcessFocusChanged(VisualElement visualElement)
        {
        }

        /// <inheritdoc/>
        void IVisualElementEventProcessor.ProcessUpdate()
        {
        }

        /// <inheritdoc/>
        void IVisualElementEventProcessor.ProcessRootVisualElementChanged(VisualElement visualElement)
        {
            rootVisualElement = visualElement;
        }
        #endregion

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
    }
}
