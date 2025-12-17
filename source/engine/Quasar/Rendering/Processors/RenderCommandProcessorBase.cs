//-----------------------------------------------------------------------
// <copyright file="RenderCommandProcessorBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Rendering.Processors
{
    /// <summary>
    /// Abstract base class for render command processors.
    /// </summary>
    public abstract class RenderCommandProcessorBase
    {
        /// <summary>
        /// Executes the commands.
        /// </summary>
        internal abstract void ExecuteCommands();

        /// <summary>
        /// Internal start event handler.
        /// </summary>
        internal void Start()
        {
            OnStart();
        }

        /// <summary>
        /// Internal shutdown event handler.
        /// </summary>
        internal void Shutdown()
        {
            OnShutdown();
        }


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
