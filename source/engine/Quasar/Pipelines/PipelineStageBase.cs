//-----------------------------------------------------------------------
// <copyright file="PipelineStageBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;

namespace Quasar.Pipelines
{
    /// <summary>
    /// Represents an abstract base class for Quasar pipeline's processing stages.
    /// </summary>
    /// <typeparam name="TContext">The pipeline context type.</typeparam>
    public abstract class PipelineStageBase<TContext>
    {
        /// <summary>
        /// Invokes the execute event handler for the current the pipeline stage.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void InvokeExecute()
        {
            OnExecute();
        }

        /// <summary>
        /// Invokes the start event handler for the current the pipeline stage.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="context">The pipeline context.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void InvokeStart(IServiceProvider serviceProvider, TContext context)
        {
            ServiceProvider = serviceProvider;
            Context = context;

            OnStart();
        }

        /// <summary>
        /// Invokes the shutdown event handler for the current the pipeline stage.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void InvokeShutdown()
        {
            OnShutdown();
        }


        /// <summary>
        /// Gets the pipeline context.
        /// </summary>
        protected TContext Context { get; private set; }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        protected IServiceProvider ServiceProvider { get; private set; }


        /// <summary>
        /// Execute event handler.
        /// </summary>
        protected abstract void OnExecute();

        /// <summary>
        /// Start event handler.
        /// </summary>
        protected virtual void OnStart()
        {
        }

        /// <summary>
        /// Shutdown event handler.
        /// </summary>
        protected virtual void OnShutdown()
        {
        }
    }
}
