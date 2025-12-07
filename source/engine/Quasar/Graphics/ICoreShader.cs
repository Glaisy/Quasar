//-----------------------------------------------------------------------
// <copyright file="ICoreShader.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents the core properties of a shader program.
    /// </summary>
    /// <seealso cref="IGraphicsResource" />
    /// <seealso cref="IIdentifierProvider{String}" />
    /// <seealso cref="IEquatable{ICoreShader}" />
    public interface ICoreShader :
        IGraphicsResource,
        IIdentifierProvider<string>,
        ITagProvider,
        IEquatable<ICoreShader>
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        ShaderType Type { get; }
    }
}
