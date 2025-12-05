//-----------------------------------------------------------------------
// <copyright file="IComputeShader.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents a compute shader program.
    /// </summary>
    /// <seealso cref="ICoreShader" />
    /// <seealso cref="IEquatable{IComputeShader}" />
    public interface IComputeShader : ICoreShader, IEquatable<IComputeShader>
    {
    }
}
