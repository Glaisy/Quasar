//-----------------------------------------------------------------------
// <copyright file="INameProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar
{
    /// <summary>
    /// Represents an object with a name property.
    /// </summary>
    public interface INameProvider
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
    }
}
