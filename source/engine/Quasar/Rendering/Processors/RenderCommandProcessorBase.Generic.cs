//-----------------------------------------------------------------------
// <copyright file="RenderCommandProcessorBase.Generic.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Quasar.Rendering.Processors
{
    /// <summary>
    /// Abstract base class for render command processors with generic command type.
    /// </summary>
    /// <typeparam name="TCommand">The command structure type.</typeparam>
    /// <seealso cref="RenderCommandProcessorBase" />
    public abstract class RenderCommandProcessorBase<TCommand> : RenderCommandProcessorBase
        where TCommand : struct
    {
        private readonly object syncRoot = new object();
        private List<TCommand> commands = new List<TCommand>();
        private List<TCommand> executingCommands = new List<TCommand>();


        /// <summary>
        /// Adds the command to the processor.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Add(in TCommand command)
        {
            lock (syncRoot)
            {
                commands.Add(command);
            }
        }


        /// <inheritdoc/>
        internal override sealed void ExecuteCommands()
        {
            // switch command and executing command lists
            executingCommands.Clear();
            lock (syncRoot)
            {
                var tempCommands = commands;
                commands = executingCommands;
                executingCommands = tempCommands;
            }

            // iterate through the executing commands
            foreach (var command in executingCommands)
            {
                OnExecuteCommand(command);
            }
        }

        /// <inheritdoc/>
        internal override sealed void Reset()
        {
            lock (syncRoot)
            {
                var command = CreateResetCommand();
                commands.Add(command);
            }
        }


        /// <summary>
        /// Creates a reset command.
        /// </summary>
        protected abstract TCommand CreateResetCommand();

        /// <summary>
        /// Command execution event handler.
        /// </summary>
        /// <param name="command">The command.</param>
        protected abstract void OnExecuteCommand(in TCommand command);
    }
}
