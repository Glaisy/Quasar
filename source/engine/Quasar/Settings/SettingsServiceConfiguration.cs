//-----------------------------------------------------------------------
// <copyright file="SettingsServiceConfiguration.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Reflection;

namespace Quasar.Settings
{
    /// <summary>
    /// Settings service configuration object to provide customization in application build time.
    /// </summary>
    public sealed class SettingsServiceConfiguration
    {
        /// <summary>
        /// Gets or sets the absolute/relative path for the settings file.
        /// Defaults are used if not set.
        /// </summary>
        public string SettingsFilePath { get; set; }

        /// <summary>
        /// Gets or sets the array of application assemblies which are scanned for settings types.
        /// Quasar assemblies are automatically scanned.
        /// </summary>
        public Assembly[] Assemblies { get; set; }
    }
}
