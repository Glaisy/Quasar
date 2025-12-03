//-----------------------------------------------------------------------
// <copyright file="SettingsBase.Generic.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Settings
{
    /// <summary>
    /// Generic base class for settings object implementations.
    /// </summary>
    /// <typeparam name="TSettings">The settings interface type.</typeparam>
    /// <seealso cref="SettingsBase"/>
    public abstract class SettingsBase<TSettings> : SettingsBase
        where TSettings : ISettings
    {
        /// <inheritdoc/>
        public override sealed void Copy(ISettings source)
        {
            if (source is not TSettings settings)
            {
                return;
            }

            CopyProperties(settings);
        }


        /// <summary>
        /// Copies the settings from the source settings.
        /// </summary>
        /// <param name="source">The source settings.</param>
        protected abstract void CopyProperties(TSettings source);


        /// <summary>
        /// Gets the child property settings or initialize from the defaults if null.
        /// </summary>
        /// <typeparam name="TImpl">The property type.</typeparam>
        /// <typeparam name="T">The property interface type.</typeparam>
        /// <param name="settings">The settings property value.</param>
        /// <param name="defaults">The defaults.</param>
        protected static TImpl GetChildSettings<TImpl, T>(ref TImpl settings, T defaults)
            where TImpl : class, T, new()
            where T : ISettings
        {
            if (settings == null)
            {
                settings = new TImpl();
                settings.Copy(defaults);
            }

            return settings;
        }

        /// <summary>
        /// Sets the child property settings from the value or from the defaults if null.
        /// </summary>
        /// <typeparam name="TImpl">The property type.</typeparam>
        /// <typeparam name="T">The property interface type.</typeparam>
        /// <param name="settings">The settings property value.</param>
        /// <param name="value">The new value.</param>
        /// <param name="defaults">The defaults.</param>
        protected static void SetChildSettings<TImpl, T>(ref TImpl settings, TImpl value, T defaults)
            where TImpl : class, T, new()
            where T : ISettings
        {
            if (value == null)
            {
                settings ??= new TImpl();
                settings.Copy(defaults);
            }
            else
            {
                settings = value;
            }
        }
    }
}
