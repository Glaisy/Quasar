//-----------------------------------------------------------------------
// <copyright file="KeyEventArgs.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace Quasar.Inputs
{
    /// <summary>
    /// Keyboard event arguments structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct KeyEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEventArgs" /> struct.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="character">The character.</param>
        /// <param name="modifiers">The modifiers.</param>
        public KeyEventArgs(KeyCode key, char character, KeyModifiers modifiers)
        {
            Key = key;
            Character = character;
            Modifiers = modifiers;
        }


        /// <summary>
        /// The character code.
        /// </summary>
        public readonly char Character;

        /// <summary>
        /// The key code.
        /// </summary>
        public readonly KeyCode Key;

        /// <summary>
        /// The active modifiers.
        /// </summary>
        public readonly KeyModifiers Modifiers;


        /// <summary>
        /// Converts to string.
        /// </summary>
        public override string ToString()
        {
            return $"(key: {Key}, character: {Character}, modifiers: {Modifiers})";
        }
    }
}
