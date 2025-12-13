//-----------------------------------------------------------------------
// <copyright file="UpdatePipeline.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core.DependencyInjection;

namespace Quasar.Pipelines.Internals
{
    /// <summary>
    /// Quasar's update pipeline implementation.
    /// </summary>
    /// <seealso cref="PipelineBase{UpdatePipelineStageBase, Object}" />
    [Export]
    [Singleton]
    internal sealed class UpdatePipeline : PipelineBase<UpdatePipelineStageBase, object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePipeline" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public UpdatePipeline(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }


        /// <inheritdoc/>
        protected override object Context => this;
    }
}
