//-----------------------------------------------------------------------
// <copyright file="TransformBase.InvalidationFlags.cs" company="Space Development">
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
    /// Abstract base class for 3D space transformation objects - InvalidationType enumeration.
    /// </summary>
    public abstract partial class TransformBase
    {
        /// <summary>
        /// Transformation invalidation flags.
        /// </summary>
        protected static class InvalidationFlags
        {
            /// <summary>
            /// The none invalidation type.
            /// </summary>
            public const int None = 0;

            /// <summary>
            /// The world space position invalidation type.
            /// </summary>
            public const int Position = 1;

            /// <summary>
            /// The world space negative Z vector invalidation type.
            /// </summary>
            public const int NegativeX = 2;

            /// <summary>
            /// The world space negative Y vector invalidation type.
            /// </summary>
            public const int NegativeY = 4;

            /// <summary>
            /// The world space negative Z vector invalidation type.
            /// </summary>
            public const int NegativeZ = 8;

            /// <summary>
            /// The world space positive X vector invalidation type.
            /// </summary>
            public const int PositiveX = 16;

            /// <summary>
            /// The world space positive Y vector invalidation type.
            /// </summary>
            public const int PositiveY = 32;

            /// <summary>
            /// The world space positive Z vector invalidation type.
            /// </summary>
            public const int PositiveZ = 64;

            /// <summary>
            /// The world space scale invalidation type.
            /// </summary>
            public const int Scale = 128;

            /// <summary>
            /// The world space rotation invalidation type.
            /// </summary>
            public const int Rotation = 256;

            /// <summary>
            /// The world space inverse rotation invalidation type.
            /// </summary>
            public const int InverseRotation = 512;

            /// <summary>
            /// All vectors invalidation type.
            /// </summary>
            public const int AllVectors = NegativeX | NegativeY | NegativeZ | PositiveX | PositiveY | PositiveZ;

            /// <summary>
            /// The all invalidation types.
            /// </summary>
            public const int All = Position | Rotation | InverseRotation | Scale | AllVectors;
        }
    }
}
