//-----------------------------------------------------------------------
// <copyright file="PhysicsContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core.DependencyInjection;

namespace Quasar.Physics.Pipelines.Internals
{
    /// <summary>
    /// The Quasar physics pipeline's context object implementation.
    /// </summary>
    /// <seealso cref="IPhysicsContext" />
    [Export(typeof(IPhysicsContext))]
    internal sealed class PhysicsContext : IPhysicsContext
    {
    }
}
