//-----------------------------------------------------------------------
// <copyright file="RootVisualElement.cs" company="Space Development">
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
    /// Represents the root element of the Quasar UI system.
    /// </summary>
    /// <seealso cref="VisualElement" />
    /// <seealso cref="IUIEventProcessor" />
    /// <seealso cref="IUIInputEventProcessor" />
    [Export(typeof(IUIEventProcessor))]
    [Export(typeof(IUIInputEventProcessor))]
    [Singleton]
    internal sealed class RootVisualElement : VisualElement, IUIEventProcessor, IUIInputEventProcessor
    {
        private readonly Canvas canvas = new Canvas();


        #region IUIEventProcessor
        /// <inheritdoc/>
        void IUIEventProcessor.ProcessUpdateEvent()
        {
            OnProcessUpdateEvent();
        }

        /// <inheritdoc/>
        void IUIEventProcessor.ProcessRenderEvent(IRenderingContext context)
        {
            OnProcessRenderEvent(canvas);
            canvas.Render(context);
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
    }
}
