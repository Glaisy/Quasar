//-----------------------------------------------------------------------
// <copyright file="AudioSettings.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core;
using Space.Core.Settings;

namespace Quasar.Settings
{
    /// <summary>
    /// Audio settings implementation.
    /// </summary>
    /// <seealso cref="SettingsBase{IAudioSettings}" />
    /// <seealso cref="IAudioSettings" />
    public sealed class AudioSettings : SettingsBase<IAudioSettings>, IAudioSettings
    {
        /// <summary>
        /// The defaults.
        /// </summary>
        public static readonly IAudioSettings Defaults = new AudioSettings
        {
            MasterVolume = 0.5f,
            MusicVolume = 0.5f,
            UIVolume = 0.5f
        };


        private float masterVolume;
        /// <summary>
        /// Gets or sets the master volume [0...1].
        /// </summary>
        public float MasterVolume
        {
            get => masterVolume;
            set => masterVolume = Ranges.FloatUnit.Clamp(value);
        }

        private float musicVolume;
        /// <summary>
        /// Gets or sets the music volume [0...1].
        /// </summary>
        public float MusicVolume
        {
            get => musicVolume;
            set => musicVolume = Ranges.FloatUnit.Clamp(value);
        }

        private float uiVolume;
        /// <summary>
        /// Gets or sets the UI volume [0...1].
        /// </summary>
        public float UIVolume
        {
            get => uiVolume;
            set => uiVolume = Ranges.FloatUnit.Clamp(value);
        }


        /// <summary>
        /// Sets the default setting values.
        /// </summary>
        public override void SetDefaults()
        {
            Copy(Defaults);
        }


        /// <summary>
        /// Copies the settings from the source settings.
        /// </summary>
        /// <param name="source">The source settings.</param>
        protected override void CopyProperties(IAudioSettings source)
        {
            MasterVolume = source.MasterVolume;
            MusicVolume = source.MusicVolume;
            UIVolume = source.UIVolume;
        }
    }
}
