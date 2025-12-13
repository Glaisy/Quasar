//-----------------------------------------------------------------------
// <copyright file="PointerWheelEventArgs.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Inputs
{
    /// <summary>
    /// Pointer scroll event arguments structure.
    /// </summary>
    public readonly struct PointerWheelEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointerWheelEventArgs" /> struct.
        /// </summary>
        /// <param name="delta">The delta.</param>
        public PointerWheelEventArgs(float delta)
        {
            Delta = delta;
        }


        /// <summary>
        /// The delta value since the last event.
        /// </summary>
        public readonly float Delta;


        /// <summary>
        /// Converts to string.
        /// </summary>
        public override string ToString()
        {
            return $"(Delta: {Delta})";
        }
    }
}
