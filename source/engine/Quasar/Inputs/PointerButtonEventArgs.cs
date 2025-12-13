//-----------------------------------------------------------------------
// <copyright file="PointerButtonEventArgs.cs" company="Space Development">
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
    /// Pointer button event arguments structure.
    /// </summary>
    public readonly struct PointerButtonEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointerButtonEventArgs" /> struct.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="modifiers">The modifiers.</param>
        /// <param name="position">The position.</param>
        public PointerButtonEventArgs(PointerButton button, KeyModifiers modifiers, in Vector2 position)
        {
            Button = button;
            Modifiers = modifiers;
            Position = position;
        }


        /// <summary>
        /// The pointer button.
        /// </summary>
        public readonly PointerButton Button;

        /// <summary>
        /// The modifiers.
        /// </summary>
        public readonly KeyModifiers Modifiers;

        /// <summary>
        /// The pointer position.
        /// </summary>
        public readonly Vector2 Position;


        /// <summary>
        /// Converts to string.
        /// </summary>
        public override string ToString()
        {
            return $"(Button: {Button}, Modifiers: {Modifiers}, Position: {Position})";
        }
    }
}
