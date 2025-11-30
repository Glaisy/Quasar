//-----------------------------------------------------------------------
// <copyright file="QuasarSettings.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core.Diagnostics;

namespace Quasar.Settings
{
    /// <summary>
    /// The Quasar settings object implementation.
    /// </summary>
    /// <seealso cref="SettingsBase{IQuasarSettings}" />
    /// <seealso cref="IQuasarSettings" />
    [Settings(typeof(IQuasarSettings))]
    public sealed class QuasarSettings : SettingsBase<IQuasarSettings>, IQuasarSettings
    {
        /// <summary>
        /// The defaults.
        /// </summary>
        public static readonly IQuasarSettings Defaults = new QuasarSettings
        {
            LogLevel = LogLevel.Trace
        };

        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        public LogLevel LogLevel { get; set; }


        /// <inheritdoc/>
        public override void SetDefaults()
        {
            Copy(Defaults);
        }


        /// <inheritdoc/>
        protected override void CopyProperties(IQuasarSettings source)
        {
            LogLevel = source.LogLevel;
        }
    }
}
