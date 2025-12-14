//-----------------------------------------------------------------------
// <copyright file="DebugSettings.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

#if DEBUG
using System;

using Quasar.Graphics;
using Quasar.Settings;

using Space.Core.Settings;

namespace Quasar.Diagnostics
{
    /// <summary>
    /// Debug settings implementation.
    /// </summary>
    /// <seealso cref="SettingsBase{IDebugSettings}" />
    /// <seealso cref="IDebugSettings" />
    [Settings(typeof(IDebugSettings))]
    public sealed class DebugSettings : SettingsBase<IDebugSettings>, IDebugSettings
    {
        private static readonly int DefaultDisplayedMessages = 5;
        private static readonly Color DefaultErrorColor = Color.Red;
        private static readonly float DefaultMessageDecayLengthSeconds = 0.3f;
        private static readonly float DefaultMessageDecayStartSeconds = 3.0f;
        private static readonly Color DefaultInfoColor = Color.White;
        private static readonly Color DefaultWarningColor = Color.Yellow;


        private static readonly DebugSettings defaults = new DebugSettings
        {
            DisplayedMessages = DefaultDisplayedMessages,
            ErrorColor = DefaultErrorColor,
            InfoColor = DefaultInfoColor,
            MessageDecayLengthSeconds = DefaultMessageDecayLengthSeconds,
            MessageDecayStartSeconds = DefaultMessageDecayStartSeconds,
            WarningColor = DefaultWarningColor
        };


        /// <summary>
        /// Gets the defaults.
        /// </summary>
        public static IDebugSettings Defaults => defaults;


        private int displayedMessages = DefaultDisplayedMessages;
        /// <summary>
        /// Gets or sets the number of displayed messages.
        /// </summary>
        public int DisplayedMessages
        {
            get => displayedMessages;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(DisplayedMessages));
                displayedMessages = value;
            }
        }

        /// <summary>
        /// Gets or sets the error text color.
        /// </summary>
        public Color ErrorColor { get; set; } = DefaultErrorColor;

        /// <summary>
        /// Gets or sets the information text color.
        /// </summary>
        public Color InfoColor { get; set; } = DefaultInfoColor;

        private float messageDecayLengthSeconds = DefaultMessageDecayLengthSeconds;
        /// <summary>
        /// Gets or sets the message decay length [s].
        /// </summary>
        public float MessageDecayLengthSeconds
        {
            get => messageDecayLengthSeconds;
            set => messageDecayLengthSeconds = MathF.Max(0, value);
        }

        private float messageDecayStartSeconds = DefaultMessageDecayStartSeconds;
        /// <summary>
        /// Gets or sets the message decay start [s].
        /// </summary>
        public float MessageDecayStartSeconds
        {
            get => messageDecayLengthSeconds;
            set => messageDecayStartSeconds = MathF.Max(0, value);
        }

        /// <summary>
        /// Gets or sets the warning text color.
        /// </summary>
        public Color WarningColor { get; set; } = DefaultWarningColor;


        /// <summary>
        /// Sets the default setting values.
        /// </summary>
        public override void SetDefaults()
        {
            CopyProperties(defaults);
        }


        /// <summary>
        /// Copies the settings from the source settings.
        /// </summary>
        /// <param name="source">The source settings.</param>
        protected override void CopyProperties(IDebugSettings source)
        {
            DisplayedMessages = source.DisplayedMessages;
            ErrorColor = source.ErrorColor;
            InfoColor = source.InfoColor;
            MessageDecayLengthSeconds = source.MessageDecayLengthSeconds;
            MessageDecayStartSeconds = source.MessageDecayStartSeconds;
            WarningColor = source.WarningColor;
        }
    }
}
#endif
