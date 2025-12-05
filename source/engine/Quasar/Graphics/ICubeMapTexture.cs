//-----------------------------------------------------------------------
// <copyright file="ICubeMapTexture.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core;

namespace Quasar.Graphics
{
    /// <summary>
    /// Cubemap texture interface definition.
    /// </summary>
    /// <seealso cref="IGraphicsResource" />
    /// <seealso cref="IIdentifierProvider{String}" />
    public interface ICubeMapTexture : IGraphicsResource, IIdentifierProvider<string>
    {
        /// <summary>
        /// Gets the size.
        /// </summary>
        Size Size { get; }
    }
}
