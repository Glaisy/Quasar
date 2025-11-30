//-----------------------------------------------------------------------
// <copyright file="ISettingsService.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Reflection;

namespace Quasar.Settings
{
    /// <summary>
    /// Settings service interface definition.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Gets the current value for the specified type.
        /// </summary>
        /// <typeparam name="T">The settings type.</typeparam>
        T Get<T>()
            where T : ISettings;

        /// <summary>
        /// Loads the latest settings from the permanent storage.
        /// </summary>
        void Load();

        /// <summary>
        /// Saves all settings to the permanent storage.
        /// </summary>
        /// <param name="createBackup">if set to <c>true</c> a backup is created from the previously saved settings.</param>
        void Save(bool createBackup = false);

        /// <summary>
        /// Scans the specified assemblies for setting types.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        void ScanAssembliesForSettings(params Assembly[] assemblies);

        /// <summary>
        /// Sets the default settings for all types.
        /// </summary>
        void SetDefaults();

        /// <summary>
        /// Applies the default settings for the specified type.
        /// </summary>
        /// <typeparam name="T">The settings type.</typeparam>
        void SetDefaults<T>()
            where T : ISettings;

        /// <summary>
        /// Subscribes the observer to the changes of the settings by the specified type.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <typeparam name="T">The settings type.</typeparam>
        IDisposable Subscribe<T>(IObserver<T> observer)
            where T : ISettings;

        /// <summary>
        /// Updates the specified settings.
        /// </summary>
        /// <typeparam name="T">The settings type.</typeparam>
        /// <param name="settings">The settings.</param>
        void Update<T>(T settings)
            where T : ISettings;
    }
}
