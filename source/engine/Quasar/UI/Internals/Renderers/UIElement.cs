//-----------------------------------------------------------------------
// <copyright file="UIElement.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar.UI.Internals.Renderers
{
    /// <summary>
    /// UI element data structure.
    /// </summary>
    internal readonly struct UIElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UIElement" /> struct.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="mesh">The mesh.</param>
        /// <param name="texture">The texture.</param>
        /// <param name="color">The color.</param>
        public UIElement(in Vector2 position, IMesh mesh, ITexture texture, in Color color)
        {
            Position = position;
            RawMesh = new RawMesh(mesh);
            TextureHandle = texture.Handle;
            Color = color;
        }


        /// <summary>
        /// The color.
        /// </summary>
        public readonly Color Color;

        /// <summary>
        /// The raw mesh.
        /// </summary>
        public readonly RawMesh RawMesh;

        /// <summary>
        /// The position.
        /// </summary>
        public readonly Vector2 Position;

        /// <summary>
        /// The texture handle.
        /// </summary>
        public readonly int TextureHandle;
    }
}
