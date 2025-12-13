//-----------------------------------------------------------------------
// <copyright file="DummyUIInputEventProcessor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Inputs;

using Space.Core.DependencyInjection;

namespace Quasar.UI.Internals
{
    /// <summary>
    /// Dummy UI input event processor implementation..
    /// </summary>
    /// <seealso cref="IUIInputEventProcessor" />
    [Export(typeof(IUIInputEventProcessor))]
    [Singleton]
    internal sealed class DummyUIInputEventProcessor : IUIInputEventProcessor
    {
        /// <inheritdoc/>
        public void ProcessKeyDown(in KeyEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void ProcessKeyUp(in KeyEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void ProcessPointerButtonDown(in PointerButtonEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void ProcessPointerButtonUp(in PointerButtonEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void ProcessPointerEnter(in PointerMoveEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void ProcessPointerLeave()
        {
        }

        /// <inheritdoc/>
        public void ProcessPointerMove(in PointerMoveEventArgs args)
        {
        }

        /// <inheritdoc/>
        public void ProcessPointerWheel(in PointerWheelEventArgs args)
        {
        }
    }
}
