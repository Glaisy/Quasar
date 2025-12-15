//-----------------------------------------------------------------------
// <copyright file="PhysicsPipeline.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Pipelines.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.Physics.Pipelines.Internals
{
    /// <summary>
    /// The Quasar physics pipeline's main class provides entry point for all physics operations.
    /// </summary>
    /// <seealso cref="PipelineBase{PhysicsPipelineStageBase,IPhysicsContext}" />
    [Export]
    [Singleton]
    internal sealed class PhysicsPipeline : PipelineBase<PhysicsPipelineStageBase, IPhysicsContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicsPipeline" /> class.
        /// </summary>
        /// <param name="physicsContext">The physics context.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public PhysicsPipeline(
            IPhysicsContext physicsContext,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            Context = physicsContext;
        }


        /// <inheritdoc/>
        protected override IPhysicsContext Context { get; }
    }
}
