//-----------------------------------------------------------------------
// <copyright file="IRawLightSource.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Graphics;

using Space.Core;

namespace Quasar.Rendering
{
    /// <summary>
    /// Represents a light source object only with the raw rendering properties.
    /// </summary>
    /// <seealso cref="IIdentifierProvider{Int32}" />
    /// <seealso cref="IEquatable{ICamera}" />
    public interface IRawLightSource : IIdentifierProvider<int>, IEquatable<IRawLightSource>
    {
        /// <summary>
        /// Gets the effective color (color * intensity).
        /// </summary>
        Color EffectiveColor { get; }

        /// <summary>
        /// Gets the field of view angle [0...180][degrees] (only for spot light sources).
        /// </summary>
        float FieldOfView { get; }

        /// <summary>
        /// Gets the effective radius (only for spot and point light sources).
        /// </summary>
        float Radius { get; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        int Timestamp { get; }

        /// <summary>
        /// Gets the transformation.
        /// </summary>
        ITransform Transform { get; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        LightSourceType Type { get; }
    }
}
