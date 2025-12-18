//-----------------------------------------------------------------------
// <copyright file="RenderModel.InvalidationFlags.cs" company="Space Development">
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
    /// General render model object (mesh + material + transform) - Invalidation flags.
    /// </summary>
    public partial class RenderModel
    {
        private static class InvalidationFlags
        {
            /// <summary>
            /// The bounding box invalidation flag.
            /// </summary>
            public const int BoundingBox = 1;

            /// <summary>
            /// The model matrix invalidation flag.
            /// </summary>
            public const int ModelMatrix = 2;

            /// <summary>
            /// All invalidation flags.
            /// </summary>
            public const int All = BoundingBox | ModelMatrix;
        }
    }
}
