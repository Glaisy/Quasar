//-----------------------------------------------------------------------
// <copyright file="KeyCode.cs" company="Space Development">
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
    /// Key code enumeration.
    /// </summary>
    public enum KeyCode : short
    {
        /// <summary>
        /// The unknown key.
        /// </summary>
        Unknown,

        /// <summary>
        /// The escape key.
        /// </summary>
        Esc,

        /// <summary>
        /// The F1 key.
        /// </summary>
        F1,

        /// <summary>
        /// The F2 key.
        /// </summary>
        F2,

        /// <summary>
        /// The F3 key.
        /// </summary>
        F3,

        /// <summary>
        /// The F4 key.
        /// </summary>
        F4,

        /// <summary>
        /// The F5 key.
        /// </summary>
        F5,

        /// <summary>
        /// The F6 key.
        /// </summary>
        F6,

        /// <summary>
        /// The F7 key.
        /// </summary>
        F7,

        /// <summary>
        /// The F8 key.
        /// </summary>
        F8,

        /// <summary>
        /// The F9 key.
        /// </summary>
        F9,

        /// <summary>
        /// The F10 key.
        /// </summary>
        F10,

        /// <summary>
        /// The F11 key.
        /// </summary>
        F11,

        /// <summary>
        /// The F12 key.
        /// </summary>
        F12,

        /// <summary>
        /// The PRTSC key.
        /// </summary>
        PrintScreen,

        /// <summary>
        /// The PAUSE key.
        /// </summary>
        Pause,

        /// <summary>
        /// The SCROLL LOCK key.
        /// </summary>
        ScrollLock,

        /// <summary>
        /// The ` key.
        /// </summary>
        GraveAccent,

        /// <summary>
        /// The - key.
        /// </summary>
        MinusSign,

        /// <summary>
        /// The = key.
        /// </summary>
        EqualSign,

        /// <summary>
        /// The BACKSPACE key.
        /// </summary>
        Backspace,

        /// <summary>
        /// The TAB key.
        /// </summary>
        Tab,

        /// <summary>
        /// The [ key.
        /// </summary>
        OpeningBracket,

        /// <summary>
        /// The ] key.
        /// </summary>
        ClosingBracket,

        /// <summary>
        /// The \ key.
        /// </summary>
        Backslash,

        /// <summary>
        /// The CAPS LOCK key.
        /// </summary>
        CapsLock,

        /// <summary>
        /// The ; key.
        /// </summary>
        SemiColon,

        /// <summary>
        /// The ' key.
        /// </summary>
        Apostrophe,

        /// <summary>
        /// The ENTER key.
        /// </summary>
        Enter,

        /// <summary>
        /// The SHIFT key.
        /// </summary>
        Shift,

        /// <summary>
        /// The comma key.
        /// </summary>
        Comma,

        /// <summary>
        /// The . key.
        /// </summary>
        Period,

        /// <summary>
        /// The / key.
        /// </summary>
        Slash,

        /// <summary>
        /// The CTRL key.
        /// </summary>
        Control,

        /// <summary>
        /// The ALT key.
        /// </summary>
        Alt,

        /// <summary>
        /// The SPACE key.
        /// </summary>
        Space,

        /// <summary>
        /// The HOME key.
        /// </summary>
        Home,

        /// <summary>
        /// The END key.
        /// </summary>
        End,

        /// <summary>
        /// The INSERT key.
        /// </summary>
        Insert,

        /// <summary>
        /// The DELETE key.
        /// </summary>
        Delete,

        /// <summary>
        /// The PAGE UP key.
        /// </summary>
        PageUp,

        /// <summary>
        /// The PAGE DOWN key.
        /// </summary>
        PageDown,

        /// <summary>
        /// The cursor up key.
        /// </summary>
        Up,

        /// <summary>
        /// The cursor down key.
        /// </summary>
        Down,

        /// <summary>
        /// The cursor left key.
        /// </summary>
        Left,

        /// <summary>
        /// The cursor right key.
        /// </summary>
        Right,

        /// <summary>
        /// The NUM LOCK key.
        /// </summary>
        NumLock,

        /// <summary>
        /// The number pad / key.
        /// </summary>
        NumPadSlash,

        /// <summary>
        /// The number pad * key.
        /// </summary>
        NumPadAsterisk,

        /// <summary>
        /// The number pad - key.
        /// </summary>
        NumPadMinus,

        /// <summary>
        /// The number pad . key.
        /// </summary>
        NumPadPeriod,

        /// <summary>
        /// The number pad + key.
        /// </summary>
        NumPadPlus,

        /// <summary>
        /// The number pad 0 key.
        /// </summary>
        NumPad0,

        /// <summary>
        /// The number pad 1 key.
        /// </summary>
        NumPad1,

        /// <summary>
        /// The number pad 2 key.
        /// </summary>
        NumPad2,

        /// <summary>
        /// The number pad 3 key.
        /// </summary>
        NumPad3,

        /// <summary>
        /// The number pad 4 key.
        /// </summary>
        NumPad4,

        /// <summary>
        /// The number pad 5 key.
        /// </summary>
        NumPad5,

        /// <summary>
        /// The number pad 6 key.
        /// </summary>
        NumPad6,

        /// <summary>
        /// The number pad 7 key.
        /// </summary>
        NumPad7,

        /// <summary>
        /// The number pad 8 key.
        /// </summary>
        NumPad8,

        /// <summary>
        /// The number pad 9 key.
        /// </summary>
        NumPad9,

        /// <summary>
        /// The A key.
        /// </summary>
        A,

        /// <summary>
        /// The B key.
        /// </summary>
        B,

        /// <summary>
        /// The C key.
        /// </summary>
        C,

        /// <summary>
        /// The D key.
        /// </summary>
        D,

        /// <summary>
        /// The E key.
        /// </summary>
        E,

        /// <summary>
        /// The F key.
        /// </summary>
        F,

        /// <summary>
        /// The G key.
        /// </summary>
        G,

        /// <summary>
        /// The H key.
        /// </summary>
        H,

        /// <summary>
        /// The I key.
        /// </summary>
        I,

        /// <summary>
        /// The J key.
        /// </summary>
        J,

        /// <summary>
        /// The K key.
        /// </summary>
        K,

        /// <summary>
        /// The L key.
        /// </summary>
        L,

        /// <summary>
        /// The M key.
        /// </summary>
        M,

        /// <summary>
        /// The N key.
        /// </summary>
        N,

        /// <summary>
        /// The O key.
        /// </summary>
        O,

        /// <summary>
        /// The P key.
        /// </summary>
        P,

        /// <summary>
        /// The Q key.
        /// </summary>
        Q,

        /// <summary>
        /// The R key.
        /// </summary>
        R,

        /// <summary>
        /// The S key.
        /// </summary>
        S,

        /// <summary>
        /// The T key.
        /// </summary>
        T,

        /// <summary>
        /// The U key.
        /// </summary>
        U,

        /// <summary>
        /// The V key.
        /// </summary>
        V,

        /// <summary>
        /// The W key.
        /// </summary>
        W,

        /// <summary>
        /// The X key.
        /// </summary>
        X,

        /// <summary>
        /// The Y key.
        /// </summary>
        Y,

        /// <summary>
        /// The Z key.
        /// </summary>
        Z,

        /// <summary>
        /// The 0 digit key.
        /// </summary>
        Digit0,

        /// <summary>
        /// The 1 digit key.
        /// </summary>
        Digit1,

        /// <summary>
        /// The 2 digit key.
        /// </summary>
        Digit2,

        /// <summary>
        /// The 3 digit key.
        /// </summary>
        Digit3,

        /// <summary>
        /// The 4 digit key.
        /// </summary>
        Digit4,

        /// <summary>
        /// The 5 digit key.
        /// </summary>
        Digit5,

        /// <summary>
        /// The 6 digit key.
        /// </summary>
        Digit6,

        /// <summary>
        /// The 7 digit key.
        /// </summary>
        Digit7,

        /// <summary>
        /// The 8 digit key.
        /// </summary>
        Digit8,

        /// <summary>
        /// The 9 digit key.
        /// </summary>
        Digit9,

        /// <summary>
        /// The key count value.
        /// </summary>
        KeyCount
    }
}
