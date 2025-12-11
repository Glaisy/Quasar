//-----------------------------------------------------------------------
// <copyright file="ITransformD.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar
{
    /// <summary>
    /// Double precision 3D transformation interface definition.
    /// </summary>
    /// <seealso cref="INameProvider" />
    public interface ITransformD : INameProvider
    {
        /// <summary>
        /// Gets the world space inverse rotation.
        /// </summary>
        QuaternionD InverseRotation { get; }

        /// <summary>
        /// Gets the position relative to the parent transformation.
        /// </summary>
        Vector3D LocalPosition { get; }

        /// <summary>
        /// Gets the rotation relative to the parent transformation.
        /// </summary>
        QuaternionD LocalRotation { get; }

        /// <summary>
        /// Gets the scale relative to the parent transformation.
        /// </summary>
        Vector3D LocalScale { get; }

        /// <summary>
        /// Gets the world space -X direction.
        /// </summary>
        Vector3D NegativeX { get; }

        /// <summary>
        /// Gets the world space -Y direction.
        /// </summary>
        Vector3D NegativeY { get; }

        /// <summary>
        /// Gets the world space -Z direction.
        /// </summary>
        Vector3D NegativeZ { get; }

        /// <summary>
        /// Gets the parent transformation.
        /// </summary>
        ITransformD Parent { get; }

        /// <summary>
        /// Gets the world space position.
        /// </summary>
        Vector3D Position { get; }

        /// <summary>
        /// Gets the world space +X direction.
        /// </summary>
        Vector3D PositiveX { get; }

        /// <summary>
        /// Gets the world space +Y direction.
        /// </summary>
        Vector3D PositiveY { get; }

        /// <summary>
        /// Gets the world space +Z direction.
        /// </summary>
        Vector3D PositiveZ { get; }

        /// <summary>
        /// Gets the world space rotation.
        /// </summary>
        QuaternionD Rotation { get; }

        /// <summary>
        /// Gets the world space scale.
        /// </summary>
        Vector3D Scale { get; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        int Timestamp { get; }


        /// <summary>
        /// Converts the world space direction into local space.
        /// </summary>
        /// <param name="direction">The direction.</param>
        Vector3D ToLocalDirection(in Vector3D direction);

        /// <summary>
        /// Converts the world space position into local space.
        /// </summary>
        /// <param name="position">The position.</param>
        Vector3D ToLocalPosition(in Vector3D position);

        /// <summary>
        /// Converts the local space direction into world space.
        /// </summary>
        /// <param name="direction">The direction.</param>
        Vector3D ToWorldDirection(in Vector3D direction);

        /// <summary>
        /// Converts the local space position into world space.
        /// </summary>
        /// <param name="position">The position.</param>
        Vector3D ToWorldPosition(in Vector3D position);
    }
}
