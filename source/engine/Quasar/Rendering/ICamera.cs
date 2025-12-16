//-----------------------------------------------------------------------
// <copyright file="ICamera.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar.Rendering
{
    /// <summary>
    /// Render camera inteface definition.
    /// </summary>
    /// <seealso cref="IRawCamera" />
    /// <seealso cref="INameProvider" />
    public interface ICamera : IRawCamera, INameProvider
    {
        /// <summary>
        /// Gets the aspect ratio (frame buffer width/height).
        /// </summary>
        float AspectRatio { get; }

        /// <summary>
        /// Gets the clear color.
        /// </summary>
        Color ClearColor { get; }

        /// <summary>
        /// Gets the clear type.
        /// </summary>
        public CameraClearType ClearType { get; }

        /// <summary>
        /// Gets a value indicating whether the camera is enabled or not.
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Gets the field of view angle [0...180][degrees].
        /// </summary>
        float FieldOfView { get; }

        /// <summary>
        /// Gets the camera frustum.
        /// </summary>
        IFrustum Frustum { get; }

        /// <summary>
        /// Gets the layer mask.
        /// </summary>
        LayerMask LayerMask { get; }

        /// <summary>
        /// Gets the projection type.
        /// </summary>
        ProjectionType ProjectionType { get; }

        /// <summary>
        /// Gets the transformation.
        /// </summary>
        ITransform Transform { get; }

        /// <summary>
        /// Gets the far z-plane (0...+Inf).
        /// </summary>
        float ZFar { get; }

        /// <summary>
        /// Gets the near z-plane (0...+Inf).
        /// </summary>
        float ZNear { get; }
    }
}