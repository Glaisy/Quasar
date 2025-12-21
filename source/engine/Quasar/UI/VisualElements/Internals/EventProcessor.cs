//-----------------------------------------------------------------------
// <copyright file="EventProcessor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Inputs;
using Quasar.Rendering;
using Quasar.UI.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.UI.VisualElements.Internals
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
    internal sealed class EventProcessor : IUIEventProcessor, IUIInputEventProcessor, IVisualElementEventProcessor
    {
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
        void IUIEventProcessor.ProcessUpdateEvent()
        {
        }
        #endregion

        #region IUIInputEventProcessor
        /// <inheritdoc/>
        void IUIInputEventProcessor.ProcessKeyDownEvent(in KeyEventArgs args)
        {
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
        void IVisualElementEventProcessor.Reset()
        {
        }
        #endregion
    }
}
