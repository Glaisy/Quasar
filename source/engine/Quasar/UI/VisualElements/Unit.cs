//-----------------------------------------------------------------------
// <copyright file="Unit.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Unit structure (Immutable).
    /// </summary>
    public readonly struct Unit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        public Unit(float value, UnitType type)
        {
            Value = value;
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="source">The source.</param>
        public Unit(float value, in Unit source)
        {
            Value = value;
            Type = source.Type;
        }


        /// <summary>
        /// The type.
        /// </summary>
        public readonly UnitType Type;

        /// <summary>
        /// The value.
        /// </summary>
        public readonly float Value;
    }
}
