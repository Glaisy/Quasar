//-----------------------------------------------------------------------
// <copyright file="RenderObject.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Threading;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics;

using Space.Core;

namespace Quasar.Rendering
{
    /// <summary>
    /// Abstract base class for render objects.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IIdentifierProvider{Int32}" />
    /// <seealso cref="INameProvider" />
    /// <seealso cref="IEquatable{RenderObject}" />
    public abstract class RenderObject : DisposableBase, IIdentifierProvider<int>, INameProvider, IEquatable<RenderObject>
    {
        private static int lastIdentifier;
        private bool isDisposed;


        /// <summary>
        /// Initializes a new instance of the <see cref="RenderObject"/> class.
        /// </summary>
        /// <param name="enabled">The initial value of the Enabled property.</param>
        protected RenderObject(bool enabled)
        {
            Id = Interlocked.Increment(ref lastIdentifier);
            Enabled = enabled;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            Enabled = false;
            isDisposed = true;
        }


        /// <inheritdoc/>
        public int Id { get; }

        private bool enabled;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        public bool Enabled
        {
            get => enabled;
            set
            {
                EnsureIsNotDisposed();

                if (enabled == value)
                {
                    return;
                }

                enabled = value;
                SendEnabledChangedCommand(value);
            }
        }

        private string name;
        /// <inheritdoc/>
        public string Name
        {
            get => name;
            set
            {
                EnsureIsNotDisposed();

                name = value;
                Transform.Name = value;
            }
        }

        /// <summary>
        /// Gets the transform.
        /// </summary>
        public Transform Transform { get; } = new Transform();


        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not RenderObject renderObject)
            {
                return false;
            }

            return Id == renderObject.Id;
        }

        /// <inheritdoc/>
        public bool Equals(RenderObject other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            if (String.IsNullOrEmpty(name))
            {
                return Id.ToString();
            }

            return $"{name}:{Id}";
        }


        /// <summary>
        /// Initializes static the services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            MatrixFactory = serviceProvider.GetRequiredService<IMatrixFactory>();
            RenderingContext = serviceProvider.GetRequiredService<IRenderingContext>();
        }


        /// <summary>
        /// Gets the matrix factory.
        /// </summary>
        protected static IMatrixFactory MatrixFactory { get; private set; }

        /// <summary>
        /// Gets the rendering context.
        /// </summary>
        protected static IRenderingContext RenderingContext { get; private set; }


        /// <summary>
        /// Ensures the render object is not disposed.
        /// </summary>
        protected void EnsureIsNotDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException($"{GetType().FullName} - [#{Id}:{Name}].");
            }
        }

        /// <summary>
        /// Sends the Enabled changed command to the processor.
        /// </summary>
        /// <param name="enabled">The new value of the enabled flag.</param>
        protected abstract void SendEnabledChangedCommand(bool enabled);
    }
}
