//-----------------------------------------------------------------------
// <copyright file="GraphicsResourceBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;

using Space.Core.Threading;

namespace Quasar.Graphics
{
    /// <summary>
    /// Abstract base class for graphics resources.
    /// </summary>
    /// <seealso cref="IDisposable" />
    /// <seealso cref="IGraphicsResource" />
    public abstract class GraphicsResourceBase : IDisposable, IGraphicsResource
    {
        private static IDispatcher dispatcher;


        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsResourceBase" /> class.
        /// </summary>
        /// <param name="descriptor">The resource descriptor.</param>
        protected GraphicsResourceBase(in GraphicsResourceDescriptor descriptor)
        {
            GraphicsDevice = descriptor.GraphicsDevice;
            Usage = descriptor.Usage;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="GraphicsResourceBase"/> class.
        /// </summary>
        ~GraphicsResourceBase()
        {
            dispatcher.Dispatch(FinalizeInternal);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            dispatcher.Dispatch(DisposeInternal);
        }


        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public IGraphicsDevice GraphicsDevice { get; private set; }

        /// <summary>
        /// Gets the internal resource handle.
        /// </summary>
        public abstract int Handle { get; }

        /// <summary>
        /// Gets the usage.
        /// </summary>
        public GraphicsResourceUsage Usage { get; private set; }


        /// <inheritdoc/>
        public bool Equals(IGraphicsResource other)
        {
            return Handle == other.Handle;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not GraphicsResourceBase other)
            {
                return false;
            }

            return Handle == other.Handle;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Handle;
        }


        /// <summary>
        /// Activates the resource.
        /// </summary>
        internal abstract void Activate();

        /// <summary>
        /// Deactivates the resource.
        /// </summary>
        internal abstract void Deactivate();

        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="services">The services.</param>
        internal static void InitializeServices(IServiceProvider services)
        {
            dispatcher = services.GetRequiredService<IDispatcher>();
        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected abstract void Dispose(bool disposing);


        private void DisposeInternal()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void FinalizeInternal()
        {
            Dispose(false);
        }
    }
}
