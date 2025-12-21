//-----------------------------------------------------------------------
// <copyright file="VisualElement.cs" company="Space Development">
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

using Quasar.Graphics;
using Quasar.UI.VisualElements.Internals;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Represents a basic UI visual element.
    /// </summary>
    /// <seealso cref="InvalidatableBase" />
    /// <seealso cref="IDisposable" />
    public partial class VisualElement : InvalidatableBase, IDisposable
    {
        private static readonly Canvas canvas = new Canvas();
        private static ITextureRepository textureRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="VisualElement"/> class.
        /// </summary>
        public VisualElement()
        {
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="VisualElement"/> class.
        /// </summary>
        ~VisualElement()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }


        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            textureRepository = serviceProvider.GetRequiredService<ITextureRepository>();
        }
    }
}
