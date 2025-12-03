//-----------------------------------------------------------------------
// <copyright file="IInteropFunctionProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Core.Utilities
{
    /// <summary>
    /// Represents a operating system specific interop function provider.
    /// </summary>
    public interface IInteropFunctionProvider
    {
        /// <summary>
        /// Gets the function delegate by the specified name.
        /// </summary>
        /// <typeparam name="T">The delegate type.</typeparam>
        /// <returns>The delegate instance.</returns>
        T GetFunction<T>()
            where T : Delegate;
    }
}
