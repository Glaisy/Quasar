//-----------------------------------------------------------------------
// <copyright file="PointerMoveEventArgs.cs" company="Space Development">
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
    /// Pointer move event arguments structure.
    /// </summary>
    public readonly struct PointerMoveEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointerMoveEventArgs"/> struct.
        /// </summary>
        /// <param name="position">The position.</param>
        public PointerMoveEventArgs(in Vector2 position)
        {
            Position = position;
        }


        /// <summary>
        /// The position.
        /// </summary>
        public readonly Vector2 Position;


        /// <summary>
        /// Converts to string.
        /// </summary>
        public override string ToString()
        {
            return $"(Position: {Position})";
        }
    }
}
