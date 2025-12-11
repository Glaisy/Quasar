//-----------------------------------------------------------------------
// <copyright file="TransformBase.InvalidationType.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar
{
    /// <summary>
    /// Abstract base class for 3D space transformation objects - InvalidationType enumeration.
    /// </summary>
    public abstract partial class TransformBase
    {
        /// <summary>
        /// Transformation invalidation type flags.
        /// </summary>
        [Flags]
        protected enum InvalidationType
        {
            /// <summary>
            /// The none invalidation type.
            /// </summary>
            None = 0,

            /// <summary>
            /// The world space position invalidation type.
            /// </summary>
            Position = 1,

            /// <summary>
            /// The world space negative Z vector invalidation type.
            /// </summary>
            NegativeX = 2,

            /// <summary>
            /// The world space negative Y vector invalidation type.
            /// </summary>
            NegativeY = 4,

            /// <summary>
            /// The world space negative Z vector invalidation type.
            /// </summary>
            NegativeZ = 8,

            /// <summary>
            /// The world space positive X vector invalidation type.
            /// </summary>
            PositiveX = 16,

            /// <summary>
            /// The world space positive Y vector invalidation type.
            /// </summary>
            PositiveY = 32,

            /// <summary>
            /// The world space positive Z vector invalidation type.
            /// </summary>
            PositiveZ = 64,

            /// <summary>
            /// The world space scale invalidation type.
            /// </summary>
            Scale = 128,

            /// <summary>
            /// The world space rotation invalidation type.
            /// </summary>
            Rotation = 256,

            /// <summary>
            /// The world space inverse rotation invalidation type.
            /// </summary>
            InverseRotation = 512,

            /// <summary>
            /// All vectors invalidation type.
            /// </summary>
            AllVectors = NegativeX | NegativeY | NegativeZ | PositiveX | PositiveY | PositiveZ,

            /// <summary>
            /// The all invalidation types.
            /// </summary>
            All = Position | Rotation | InverseRotation | Scale | AllVectors
        }
    }
}
