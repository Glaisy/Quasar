//-----------------------------------------------------------------------
// <copyright file="GLMatrixFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;

using Quasar.Graphics;

using Space.Core.DependencyInjection;
using Space.Core.Mathematics;

namespace Quasar.OpenGL.Graphics.Factories
{
    /// <summary>
    /// OpenGL graphics transformation matrix factory interface definition (left-handed).
    /// The matrix representation could be either row or column major representation depending on the graphics platform.
    /// </summary>
    /// <seealso cref="IMatrixFactory" />
    [Export]
    [Singleton]
    internal sealed class GLMatrixFactory : IMatrixFactory
    {
        /// <inheritdoc/>
        public void CreateModelMatrix(ITransform transform, ref Matrix4 result)
        {
            Matrix4 translationMatrix, rotationMatrix, scaleMatrix, tempMatrix;
            scaleMatrix.FromScale(transform.Scale);
            rotationMatrix.FromQuaternion(transform.Rotation, false);
            translationMatrix.FromTranslation(transform.Position, false);

            Matrix4.Multiply(scaleMatrix, rotationMatrix, ref tempMatrix);
            Matrix4.Multiply(tempMatrix, translationMatrix, ref result);
        }

        /// <inheritdoc/>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:Parameters should be on same line or separate lines", Justification = "Reviewed.")]
        public void CreateOrthographicProjectionMatrix(float width, float height, float nearPlane, float farPlane, ref Matrix4 result)
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(farPlane, nearPlane, nameof(farPlane));

            // pre-calculate values
            var xScale = 2.0f / width;
            var yScale = 2.0f / height;
            var negativeZPlaneDistance = nearPlane - farPlane;
            var zScale = 2.0f / negativeZPlaneDistance;
            var zFactor = nearPlane / negativeZPlaneDistance;

            // initialize matrix
            result = new Matrix4(
                xScale, 0.0f, 0.0f, 0.0f,
                0.0f, yScale, 0.0f, 0.0f,
                0.0f, 0.0f, zScale, 0.0f,
                0.0f, 0.0f, zFactor, 1.0f);
        }

        /// <inheritdoc/>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:Parameters should be on same line or separate lines", Justification = "Reviewed.")]
        public void CreatePerspectiveProjectionMatrix(float aspectRatio, float fieldOfView, float nearPlane, float farPlane, ref Matrix4 result)
        {
            // check arguments
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(fieldOfView, nameof(fieldOfView));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fieldOfView, 180, nameof(fieldOfView));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(nearPlane, nameof(nearPlane));
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(farPlane, nearPlane, nameof(farPlane));

            // pre-calculate values
            var yScale = 1.0f / MathF.Tan(0.5f * MathematicsConstants.DegreeToRadian * fieldOfView);
            var xScale = yScale / aspectRatio;
            var negativePlaneDistance = nearPlane - farPlane;
            var zScale = farPlane / negativePlaneDistance;
            var zFactor = farPlane * nearPlane / negativePlaneDistance;

            // initialize matrix
            result = new Matrix4(
                xScale, 0.0f, 0.0f, 0.0f,
                0.0f, yScale, 0.0f, 0.0f,
                0.0f, 0.0f, zScale, -1.0f,
                0.0f, 0.0f, zFactor, 0.0f);
        }

        /// <inheritdoc/>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:Parameters should be on same line or separate lines", Justification = "Reviewed.")]
        public void CreateUIProjectionMatrix(in Size viewportSize, ref Matrix4 result)
        {
            // pre-calculate values
            var xScale = 2.0f / viewportSize.Width;
            var yScale = 2.0f / viewportSize.Height;

            // initialize matrix
            result = new Matrix4(
                xScale, 0.0f, 0.0f, 0.0f,
                0.0f, yScale, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f,
                -1.0f, -1.0f, 0.0f, 1.0f);
        }

        /// <inheritdoc/>
        public unsafe void CreateViewMatrix(ITransform transform, ref Matrix4 result)
        {
            var xAxis = transform.PositiveX;
            var yAxis = transform.PositiveY;
            var zAxis = transform.NegativeZ;
            var position = transform.Position;

            fixed (Matrix4* matrix = &result)
            {
                var cells = (float*)matrix;
                cells[0] = xAxis.X;
                cells[1] = yAxis.X;
                cells[2] = zAxis.X;
                cells[3] = 0.0f;

                cells[4] = xAxis.Y;
                cells[5] = yAxis.Y;
                cells[6] = zAxis.Y;
                cells[7] = 0.0f;

                cells[8] = xAxis.Z;
                cells[9] = yAxis.Z;
                cells[10] = zAxis.Z;
                cells[11] = 0.0f;

                cells[12] = -xAxis.Dot(position);
                cells[13] = -yAxis.Dot(position);
                cells[14] = -zAxis.Dot(position);
                cells[15] = 1.0f;
            }
        }

        /// <inheritdoc/>
        public unsafe void CreateViewRotationMatrix(in Matrix4 viewMatrix, ref Matrix4 result)
        {
            result = viewMatrix;
            fixed (Matrix4* matrix = &result)
            {
                var cells = (float*)matrix;
                cells[12] = 0.0f;
                cells[13] = 0.0f;
                cells[14] = 0.0f;
            }
        }
    }
}
