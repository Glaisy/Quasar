//-----------------------------------------------------------------------
// <copyright file="IStringOperationContext.cs" company="Space Development">
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

namespace Quasar.Utilities
{
    /// <summary>
    /// Represents the context for string operations.
    /// </summary>
    public interface IStringOperationContext
    {
        /// <summary>
        /// Gets the string builder pool.
        /// </summary>
        IPool<StringBuilder> BuilderPool { get; }

        /// <summary>
        /// Gets the string list pool.
        /// </summary>
        IPool<List<string>> ListPool { get; }
    }
}
