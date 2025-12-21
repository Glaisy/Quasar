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

using System.Collections.Generic;
using System.Threading;

using Quasar.UI.VisualElements;

using Space.Core.Collections;
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
        /// Initializes a new instance of the <see cref="UIContext"/> class.
        /// </summary>
        /// <param name="poolFactory">The pool factory.</param>
        public UIContext(IPoolFactory poolFactory)
        {
            executionThreadId = Thread.CurrentThread.ManagedThreadId;

            VisualElementListPool = poolFactory.Create(false, () => new List<VisualElement>(), x => x.Clear());
        }


        /// <inheritdoc/>
        public IPool<List<VisualElement>> VisualElementListPool { get; }


        /// <inheritdoc/>
        public void Validate()
        {
            if (Thread.CurrentThread.ManagedThreadId == executionThreadId)
            {
                return;
            }

            throw new UIException("UI operations should be executed on the main thread.");
        }
    }
}
