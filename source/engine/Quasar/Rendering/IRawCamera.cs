//-----------------------------------------------------------------------
// <copyright file="IRawCamera.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

using Space.Core;

namespace Quasar.Rendering
{
    /// <summary>
    /// Represents a camera object only with the raw rendering properties.
    /// </summary>
    /// <seealso cref="IIdentifierProvider{Int32}" />
    public interface IRawCamera : IIdentifierProvider<int>
    {
        /// <summary>
        /// Gets the frame buffer.
        /// </summary>
        IFrameBuffer FrameBuffer { get; }

        /// <summary>
        /// Gets the projection matrix.
        /// </summary>
        ref readonly Matrix4 ProjectionMatrix { get; }

        /// <summary>
        /// Gets the view matrix.
        /// </summary>
        ref readonly Matrix4 ViewMatrix { get; }

        /// <summary>
        /// Gets the view - projection matrix.
        /// </summary>
        ref readonly Matrix4 ViewProjectionMatrix { get; }

        /// <summary>
        /// Gets the view rotation - projection matrix.
        /// </summary>
        ref readonly Matrix4 ViewRotationProjectionMatrix { get; }
    }
}