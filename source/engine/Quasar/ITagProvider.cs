//-----------------------------------------------------------------------
// <copyright file="ITagProvider.cs" company="Space Development">
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
    /// Represents an object with a tag value property.
    /// </summary>
    public interface ITagProvider
    {
        /// <summary>
        /// Gets the tag value.
        /// </summary>
        string Tag { get; }
    }
}
