//-----------------------------------------------------------------------
// <copyright file="UIContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core.DependencyInjection;

namespace Quasar.UI.Internals
{
    /// <summary>
    /// The Quasar UI context object implementation.
    /// </summary>
    /// <seealso cref="IUIContext" />
    [Export(typeof(IUIContext))]
    [Singleton]
    internal sealed class UIContext : IUIContext
    {
        private readonly int executionThreadId;


        /// <summary>
        /// Initializes a new instance of the <see cref="UIContext" /> class.
        /// </summary>
        public UIContext()
        {
            executionThreadId = Environment.CurrentManagedThreadId;
        }


        /// <inheritdoc/>
        public void Validate()
        {
            if (Environment.CurrentManagedThreadId == executionThreadId)
            {
                return;
            }

            throw new UIException("UI operations should be executed on the main thread.");
        }
    }
}
