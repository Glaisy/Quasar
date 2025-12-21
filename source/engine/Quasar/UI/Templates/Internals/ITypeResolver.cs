//-----------------------------------------------------------------------
// <copyright file="ITypeResolver.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.UI.Templates.Internals
{
    /// <summary>
    /// Represents an internal type resolver component.
    /// </summary>
    internal interface ITypeResolver
    {
        /// <summary>
        /// Resolves a type by the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The resolved type or null if not resolvable.</returns>
        Type Resolve(string name);
    }
}
