//-----------------------------------------------------------------------
// <copyright file="InputMapper.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Quasar.Inputs;
using Quasar.Windows.Interop.User32;

using Space.Core.DependencyInjection;

namespace Quasar.Windows.UI
{
    /// <summary>
    /// Windows specific input mapper object.
    /// </summary>
    [Export]
    [Singleton]
    internal sealed class InputMapper
    {
        #region Mappings
        private static readonly Dictionary<MouseButtons, PointerButton> mouseButtonMappings =
            new Dictionary<MouseButtons, PointerButton>
            {
                { MouseButtons.Left, PointerButton.Left },
                { MouseButtons.Middle, PointerButton.Middle },
                { MouseButtons.Right, PointerButton.Right },
            };

        private static readonly Dictionary<Keys, KeyCode> keysMappings = new Dictionary<Keys, KeyCode>
            {
                { Keys.A, KeyCode.A },
                { Keys.Oem7, KeyCode.Apostrophe },
                { Keys.B, KeyCode.B },
                { Keys.Oem5, KeyCode.Backslash },
                { Keys.Back, KeyCode.Backspace },
                { Keys.C, KeyCode.C },
                { Keys.CapsLock, KeyCode.CapsLock },
                { Keys.ControlKey, KeyCode.Control },
                { Keys.D, KeyCode.D },
                { Keys.D0, KeyCode.Digit0 },
                { Keys.D1, KeyCode.Digit1 },
                { Keys.D2, KeyCode.Digit2 },
                { Keys.D3, KeyCode.Digit3 },
                { Keys.D4, KeyCode.Digit4 },
                { Keys.D5, KeyCode.Digit5 },
                { Keys.D6, KeyCode.Digit6 },
                { Keys.D7, KeyCode.Digit7 },
                { Keys.D8, KeyCode.Digit8 },
                { Keys.D9, KeyCode.Digit9 },
                { Keys.Delete, KeyCode.Delete },
                { Keys.Down, KeyCode.Down },
                { Keys.E, KeyCode.E },
                { Keys.End, KeyCode.End },
                { Keys.Enter, KeyCode.Enter },
                { Keys.Oemplus, KeyCode.EqualSign },
                { Keys.Escape, KeyCode.Esc },
                { Keys.F, KeyCode.F },
                { Keys.F1, KeyCode.F1 },
                { Keys.F2, KeyCode.F2 },
                { Keys.F3, KeyCode.F3 },
                { Keys.F4, KeyCode.F4 },
                { Keys.F5, KeyCode.F5 },
                { Keys.F6, KeyCode.F6 },
                { Keys.F7, KeyCode.F7 },
                { Keys.F8, KeyCode.F8 },
                { Keys.F9, KeyCode.F9 },
                { Keys.F10, KeyCode.F10 },
                { Keys.F11, KeyCode.F11 },
                { Keys.F12, KeyCode.F12 },
                { Keys.G, KeyCode.G },
                { Keys.Oem3, KeyCode.GraveAccent },
                { Keys.H, KeyCode.H },
                { Keys.Home, KeyCode.Home },
                { Keys.I, KeyCode.I },
                { Keys.Insert, KeyCode.Insert },
                { Keys.J, KeyCode.J },
                { Keys.K, KeyCode.K },
                { Keys.NumPad0, KeyCode.NumPad0 },
                { Keys.NumPad1, KeyCode.NumPad1 },
                { Keys.NumPad2, KeyCode.NumPad2 },
                { Keys.NumPad3, KeyCode.NumPad3 },
                { Keys.NumPad4, KeyCode.NumPad4 },
                { Keys.NumPad5, KeyCode.NumPad5 },
                { Keys.NumPad6, KeyCode.NumPad6 },
                { Keys.NumPad7, KeyCode.NumPad7 },
                { Keys.NumPad8, KeyCode.NumPad8 },
                { Keys.NumPad9, KeyCode.NumPad9 },
                { Keys.Add, KeyCode.NumPadPlus },
                { Keys.Decimal, KeyCode.NumPadPeriod },
                { Keys.Divide, KeyCode.NumPadSlash },
                { Keys.Multiply, KeyCode.NumPadAsterisk },
                { Keys.Subtract, KeyCode.NumPadMinus },
                { Keys.ShiftKey, KeyCode.Shift },
                { Keys.L, KeyCode.L },
                { Keys.Left, KeyCode.Left },
                { Keys.LMenu, KeyCode.Alt },
                { Keys.LControlKey, KeyCode.Control },
                { Keys.LShiftKey, KeyCode.Shift },
                { Keys.M, KeyCode.M },
                { Keys.Menu, KeyCode.Alt },
                { Keys.OemMinus, KeyCode.MinusSign },
                { Keys.N, KeyCode.N },
                { Keys.NumLock, KeyCode.NumLock },
                { Keys.O, KeyCode.O },
                { Keys.Oem4, KeyCode.OpeningBracket },
                { Keys.Oemcomma, KeyCode.Comma },
                { Keys.P, KeyCode.P },
                { Keys.PageDown, KeyCode.PageDown },
                { Keys.PageUp, KeyCode.PageUp },
                { Keys.Pause, KeyCode.Pause },
                { Keys.OemPeriod, KeyCode.Period },
                { Keys.PrintScreen, KeyCode.PrintScreen },
                { Keys.Q, KeyCode.Q },
                { Keys.R, KeyCode.R },
                { Keys.Right, KeyCode.Right },
                { Keys.RMenu, KeyCode.Alt },
                { Keys.Oem6, KeyCode.ClosingBracket },
                { Keys.RControlKey, KeyCode.Control },
                { Keys.RShiftKey, KeyCode.Shift },
                { Keys.S, KeyCode.S },
                { Keys.Scroll, KeyCode.ScrollLock },
                { Keys.OemSemicolon, KeyCode.SemiColon },
                { Keys.Oem2, KeyCode.Slash },
                { Keys.Space, KeyCode.Space },
                { Keys.T, KeyCode.T },
                { Keys.Tab, KeyCode.Tab },
                { Keys.U, KeyCode.U },
                { Keys.V, KeyCode.V },
                { Keys.W, KeyCode.W },
                { Keys.X, KeyCode.X },
                { Keys.Y, KeyCode.Y },
                { Keys.Z, KeyCode.Z }
            };
        #endregion


        private readonly StringBuilder conversionBuffer = new StringBuilder(32);
        private readonly byte[] keyboardState = new byte[256];


        /// <summary>
        /// Maps the specified key to Quasar key code and character value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="isShiftPressed">SHIFT key pressed flag.</param>
        /// <returns>
        /// The key code and character pair.
        /// </returns>
        public (KeyCode KeyCode, char Character) MapKey(Keys key, bool isShiftPressed)
        {
            // map key code
            if (!keysMappings.TryGetValue(key, out var keyCode))
            {
                keyCode = KeyCode.Unknown;
            }

            // map character
            conversionBuffer.Clear();
            keyboardState[(int)Keys.ShiftKey] = isShiftPressed ? (byte)0xFF : (byte)0x00;
            User32.ToUnicode(key, 0, keyboardState, conversionBuffer, conversionBuffer.Capacity, 0);
            var character = conversionBuffer.Length > 0 ? conversionBuffer[0] : '\0';

            return (keyCode, character);
        }

        /// <summary>
        /// Maps the specified mouse button to Quasar pointer button.
        /// </summary>
        /// <param name="button">The button.</param>
        public PointerButton MapMouseButton(MouseButtons button)
        {
            if (mouseButtonMappings.TryGetValue(button, out var pointerButton))
            {
                return pointerButton;
            }

            return PointerButton.None;
        }
    }
}
