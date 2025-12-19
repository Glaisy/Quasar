//-----------------------------------------------------------------------
// <copyright file="ILightSource.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar.Rendering
{
    /// <summary>
    /// Represents a rendering Light source object.
    /// </summary>
    /// <seealso cref="IRawLightSource" />
    /// <seealso cref="INameProvider" />
    public interface ILightSource : IRawLightSource, INameProvider
    {
        /// <summary>
        /// Gets the color value.
        /// </summary>
        Color Color { get; }

        /// <summary>
        /// Gets a value indicating whether the light source is enabled or not.
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Gets the intensity [0...1].
        /// </summary>
        float Intensity { get; }
    }
}
