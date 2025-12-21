//-----------------------------------------------------------------------
// <copyright file="GridLength.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements.Layouts
{
    /// <summary>
    /// Represents the grid length structure (Immutable).
    /// </summary>
    public readonly struct GridLength
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridLength"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public GridLength(float value)
        {
            Type = GridLengthType.Pixel;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridLength"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        public GridLength(float value, GridLengthType type)
        {
            Type = type;
            Value = value;
        }


        /// <summary>
        /// The type.
        /// </summary>
        public readonly GridLengthType Type;

        /// <summary>
        /// The value.
        /// </summary>
        public readonly float Value;
    }
}
