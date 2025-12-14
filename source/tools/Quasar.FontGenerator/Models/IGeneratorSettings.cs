//-----------------------------------------------------------------------
// <copyright file="IGeneratorSettings.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core.Settings;

namespace Quasar.FontGenerator.Models
{
    /// <summary>
    /// Represents the settings values for the generator tool.
    /// </summary>
    internal interface IGeneratorSettings : ISettings
    {
        /// <summary>
        /// Gets the export file path.
        /// </summary>
        string ExportFilePath { get; }

        /// <summary>
        /// Gets the font data settings.
        /// </summary>
        IFontDataSettings FontDataSettings { get; }
    }
}