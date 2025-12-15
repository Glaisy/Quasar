//-----------------------------------------------------------------------
// <copyright file="ISettingsProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core.Settings;

namespace Quasar.Settings
{
    /// <summary>
    /// Represents the settings provider to access the configured settings objects.
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Gets the current value for the specified type.
        /// </summary>
        /// <typeparam name="T">The settings type.</typeparam>
        T Get<T>()
            where T : ISettings;

        /// <summary>
        /// Subscribes the observer to the changes of the settings by the specified type.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <typeparam name="T">The settings type.</typeparam>
        IDisposable Subscribe<T>(IObserver<T> observer)
            where T : ISettings;
    }
}
