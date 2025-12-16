//-----------------------------------------------------------------------
// <copyright file="CameraCommand.cs" company="Space Development">
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
    /// Camera command structure.
    /// </summary>
    internal struct CameraCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraCommand" /> struct.
        /// </summary>
        /// <param name="camera">The camera.</param>
        /// <param name="type">The type.</param>
        public CameraCommand(ICamera camera, CameraCommandType type)
        {
            Camera = camera;
            Type = type;
        }


        /// <summary>
        /// The camera.
        /// </summary>
        public readonly ICamera Camera;

        /// <summary>
        /// The command type.
        /// </summary>
        public readonly CameraCommandType Type;


        /// <summary>
        /// The enabled flag value.
        /// </summary>
        public bool Enabled;
    }
}
