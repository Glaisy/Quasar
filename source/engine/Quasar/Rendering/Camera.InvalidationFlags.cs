//-----------------------------------------------------------------------
// <copyright file="Camera.InvalidationFlags.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Rendering
{
    /// <summary>
    /// Render camera implementation - Invalidation flags.
    /// </summary>
    public partial class Camera
    {
        private static class InvalidationFlags
        {
            /// <summary>
            /// The projection matrix invalidation flag.
            /// </summary>
            public const int ProjectionMatrix = 1;

            /// <summary>
            /// The view matrix invalidation flag.
            /// </summary>
            public const int ViewMatrix = 2;

            /// <summary>
            /// The view projection matrix invalidation flag.
            /// </summary>
            public const int ViewProjectionMatrix = 4;

            /// <summary>
            /// The view rotation projection (skybox) matrix invalidation flag.
            /// </summary>
            public const int ViewRotationProjectionMatrix = 8;

            /// <summary>
            /// The frustum invalidation flag.
            /// </summary>
            public const int Frustum = 16;

            /// <summary>
            /// The aspect ratio invalidation flag.
            /// </summary>
            public const int AspectRatio = 32;

            /// <summary>
            /// All projection flags.
            /// </summary>
            public const int AllProjections = ProjectionMatrix | ViewProjectionMatrix | ViewRotationProjectionMatrix | Frustum | AspectRatio;

            /// <summary>
            /// All view flags.
            /// </summary>
            public const int AllViews = ViewMatrix | ViewProjectionMatrix | ViewRotationProjectionMatrix | Frustum;

            /// <summary>
            /// All invalidation flags.
            /// </summary>
            public const int All = AllViews | AllProjections;
        }
    }
}
