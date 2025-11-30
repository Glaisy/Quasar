//-----------------------------------------------------------------------
// <copyright file="StringOperationContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Text;

using Space.Core.Collections;
using Space.Core.DependencyInjection;

namespace Quasar.Utilities.Internals
{
    /// <summary>
    /// String operation context implementation.
    /// </summary>
    /// <seealso cref="IStringOperationContext" />
    [Export(typeof(IStringOperationContext))]
    [Singleton]
    internal sealed class StringOperationContext : IStringOperationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringOperationContext"/> class.
        /// </summary>
        /// <param name="poolFactory">The pool factory.</param>
        public StringOperationContext(IPoolFactory poolFactory)
        {
            BuilderPool = poolFactory.Create(true, () => new StringBuilder(), x => x.Clear());
            ListPool = poolFactory.Create(true, () => new List<string>(), x => x.Clear());
        }


        /// <inheritdoc/>
        public IPool<StringBuilder> BuilderPool { get; }

        /// <inheritdoc/>
        public IPool<List<string>> ListPool { get; }
    }
}
