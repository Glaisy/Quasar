//-----------------------------------------------------------------------
// <copyright file="RenderingView.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Rendering.Internals
{
    /// <summary>
    /// Represents an internal data structure of the current rendering view.
    /// </summary>
    public readonly struct RenderingView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingView" /> struct.
        /// </summary>
        /// <param name="camera">The camera.</param>
        public RenderingView(ICamera camera)
        {
            Camera = camera;
        }


        /// <summary>
        /// The camera.
        /// </summary>
        public readonly ICamera Camera;
    }
}
