//-----------------------------------------------------------------------
// <copyright file="ITransform.cs" company="Space Development">
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
    /// Single precision 3D transformation interface definition.
    /// </summary>
    /// <seealso cref="INameProvider" />
    public interface ITransform : INameProvider
    {
        /// <summary>
        /// Gets the world space inverse rotation.
        /// </summary>
        Quaternion InverseRotation { get; }

        /// <summary>
        /// Gets the position relative to the parent transformation.
        /// </summary>
        Vector3 LocalPosition { get; }

        /// <summary>
        /// Gets the rotation relative to the parent transformation.
        /// </summary>
        Quaternion LocalRotation { get; }

        /// <summary>
        /// Gets the scale relative to the parent transformation.
        /// </summary>
        Vector3 LocalScale { get; }

        /// <summary>
        /// Gets the world space -X direction.
        /// </summary>
        Vector3 NegativeX { get; }

        /// <summary>
        /// Gets the world space -Y direction.
        /// </summary>
        Vector3 NegativeY { get; }

        /// <summary>
        /// Gets the world space -Z direction.
        /// </summary>
        Vector3 NegativeZ { get; }

        /// <summary>
        /// Gets the parent transformation.
        /// </summary>
        ITransform Parent { get; }

        /// <summary>
        /// Gets the world space position.
        /// </summary>
        Vector3 Position { get; }

        /// <summary>
        /// Gets the world space +X direction.
        /// </summary>
        Vector3 PositiveX { get; }

        /// <summary>
        /// Gets the world space +Y direction.
        /// </summary>
        Vector3 PositiveY { get; }

        /// <summary>
        /// Gets the world space +Z direction.
        /// </summary>
        Vector3 PositiveZ { get; }

        /// <summary>
        /// Gets the world space rotation.
        /// </summary>
        Quaternion Rotation { get; }

        /// <summary>
        /// Gets the world space scale.
        /// </summary>
        Vector3 Scale { get; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        int Timestamp { get; }


        /// <summary>
        /// Converts the world space direction into local space.
        /// </summary>
        /// <param name="direction">The direction.</param>
        Vector3 ToLocalDirection(in Vector3 direction);

        /// <summary>
        /// Converts the world space position into local space.
        /// </summary>
        /// <param name="position">The position.</param>
        Vector3 ToLocalPosition(in Vector3 position);

        /// <summary>
        /// Converts the local space direction into world space.
        /// </summary>
        /// <param name="direction">The direction.</param>
        Vector3 ToWorldDirection(in Vector3 direction);

        /// <summary>
        /// Converts the local space position into world space.
        /// </summary>
        /// <param name="position">The position.</param>
        Vector3 ToWorldPosition(in Vector3 position);
    }
}
