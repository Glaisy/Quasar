//-----------------------------------------------------------------------
// <copyright file="IMatrixFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics
{
    /// <summary>
    /// Graphics transformation matrix factory interface definition (left-handed).
    /// The matrix representation could be either row or column major representation depending on the graphics platform.
    /// </summary>
    public interface IMatrixFactory
    {
        /// <summary>
        /// Creates the model matrix for the specified transformation.
        /// </summary>
        /// <param name="transform">The camera transform.</param>
        /// <param name="result">The result.</param>
        void CreateModelMatrix(ITransform transform, ref Matrix4 result);

        /// <summary>
        /// Creates an orthographic projection matrix for the specified viewport and planes.
        /// </summary>
        /// <param name="width">The viewport width.</param>
        /// <param name="height">The viewport height.</param>
        /// <param name="nearPlane">The near plane.</param>
        /// <param name="farPlane">The far plane.</param>
        /// <param name="result">The result.</param>
        void CreateOrthographicProjectionMatrix(float width, float height, float nearPlane, float farPlane, ref Matrix4 result);

        /// <summary>
        /// Creates the perspective projection matrix for the specified aspect ratio, fov and plane.
        /// </summary>
        /// <param name="aspectRatio">The aspect ratio.</param>
        /// <param name="fieldOfView">The field of view [angle].</param>
        /// <param name="nearPlane">The near plane.</param>
        /// <param name="farPlane">The far plane.</param>
        /// <param name="result">The result.</param>
        void CreatePerspectiveProjectionMatrix(float aspectRatio, float fieldOfView, float nearPlane, float farPlane, ref Matrix4 result);

        /// <summary>
        /// Creates an UI projection matrix for the specified viewport.
        /// </summary>
        /// <param name="viewportSize">Size of the viewport.</param>
        /// <param name="result">The result.</param>
        void CreateUIProjectionMatrix(in Size viewportSize, ref Matrix4 result);

        /// <summary>
        /// Creates the view matrix for the specified camera transformation.
        /// </summary>
        /// <param name="transform">The camera transform.</param>
        /// <param name="result">The result.</param>
        void CreateViewMatrix(ITransform transform, ref Matrix4 result);

        /// <summary>
        /// Creates the view rotation matrix from the view matrix.
        /// </summary>
        /// <param name="viewMatrix">The view matrix.</param>
        /// <param name="result">The result.</param>
        void CreateViewRotationMatrix(in Matrix4 viewMatrix, ref Matrix4 result);
    }
}
