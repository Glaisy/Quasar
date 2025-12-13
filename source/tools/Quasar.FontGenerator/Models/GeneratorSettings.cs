//-----------------------------------------------------------------------
// <copyright file="GeneratorSettings.cs" company="Space Development">
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
    internal sealed class GeneratorSettings : SettingsBase<IGeneratorSettings>, IGeneratorSettings
    {
        /// <summary>
        /// Initializes static members of the <see cref="GeneratorSettings"/> class.
        /// </summary>
        static GeneratorSettings()
        {
            var fontDataSettings = new FontDataSettings();
            fontDataSettings.SetDefaults();

            Defaults = new GeneratorSettings
            {
                fontDataSettings = fontDataSettings
            };
        }


        /// <summary>
        /// The defaults.
        /// </summary>
        public static IGeneratorSettings Defaults;


        /// <summary>
        /// Gets or sets the export folder path.
        /// </summary>
        public string ExportFolderPath { get; set; }

        private FontDataSettings fontDataSettings;
        /// <summary>
        /// Gets or sets the font data settings.
        /// </summary>
        public FontDataSettings FontDataSettings
        {
            get => fontDataSettings;
            set
            {
                if (value == null)
                {
                    value = new FontDataSettings();
                    value.SetDefaults();
                }

                fontDataSettings = value;
            }
        }

        /// <inheritdoc/>
        IFontDataSettings IGeneratorSettings.FontDataSettings => fontDataSettings;


        /// <inheritdoc/>
        public override void SetDefaults()
        {
            Copy(Defaults);
        }


        /// <inheritdoc/>
        protected override void CopyProperties(IGeneratorSettings source)
        {
            ExportFolderPath = source.ExportFolderPath;

            if (source.FontDataSettings == null)
            {
                FontDataSettings = null;
            }
            else
            {
                fontDataSettings ??= new FontDataSettings();
                fontDataSettings.Copy(source.FontDataSettings);
            }
        }
    }
}
