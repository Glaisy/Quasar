//-----------------------------------------------------------------------
// <copyright file="LightSourceCommand.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Rendering.Processors.Internals
{
    /// <summary>
    /// Light source command data structure.
    /// </summary>
    internal struct LightSourceCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LightSourceCommand" /> struct.
        /// </summary>
        /// <param name="lightSource">The light source.</param>
        /// <param name="type">The command type..</param>
        public LightSourceCommand(LightSource lightSource, LightSourceCommandType type)
        {
            LightSource = lightSource;
            Type = type;
        }


        /// <summary>
        /// The light source value.
        /// </summary>
        public readonly LightSource LightSource;

        /// <summary>
        /// The command type.
        /// </summary>
        public readonly LightSourceCommandType Type;

        /// <summary>
        /// The enabled flag value.
        /// </summary>
        public bool IsEnabled;
    }
}
