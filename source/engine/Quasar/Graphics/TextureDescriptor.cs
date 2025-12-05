//-----------------------------------------------------------------------
// <copyright file="TextureDescriptor.cs" company="Space Development">
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
    /// Represents a structure whics describes the creation properties of a Texture.
    /// </summary>
    public readonly struct TextureDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextureDescriptor" /> struct.
        /// </summary>
        /// <param name="anisotropyLevel">The anisotropic filtering level(0 - no filtering, >0: the number of texels used for anistropic filter or
        /// indicates that mipmapping should be used if anistropic filtering is not supported.
        /// </param>
        /// <param name="repeatX">The repeat mode in x direction.</param>
        /// <param name="repeatY">The repeat mode in y direction.</param>
        public TextureDescriptor(
            int anisotropyLevel,
            TextureRepeatMode repeatX = TextureRepeatMode.Clamped,
            TextureRepeatMode repeatY = TextureRepeatMode.Clamped)
        {
            AnisotropyLevel = anisotropyLevel;
            Filtered = anisotropyLevel > 0;
            RepeatX = repeatX;
            RepeatY = repeatY;
        }


        /// <summary>
        /// The anisotropic filtering level (if supported).
        /// </summary>
        public readonly int AnisotropyLevel;

        /// <summary>
        /// The value that indicates whether mipmapping/anitropic filtering is set for the texture.
        /// </summary>
        public readonly bool Filtered;

        /// <summary>
        /// The X repeat mode.
        /// </summary>
        public readonly TextureRepeatMode RepeatX;

        /// <summary>
        /// The Y repeat mode.
        /// </summary>
        public readonly TextureRepeatMode RepeatY;
    }
}
