//-----------------------------------------------------------------------
// <copyright file="FontSettings.cs" company="Space Development">
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

namespace Quasar.FontGenerator.Models
{
    /// <summary>
    /// Font settings object implementation.
    /// </summary>
    /// <seealso cref="SettingsBase{IFontSettings}" />
    /// <seealso cref="IFontSettings" />
    internal sealed class FontSettings : SettingsBase<IFontSettings>, IFontSettings
    {
        /// <summary>
        /// The defaults.
        /// </summary>
        public static readonly IFontSettings Defaults = new FontSettings
        {
            FamilyName = "Arial",
            Size = 20
        };


        private string familyName;
        /// <summary>
        /// Gets or sets the font family name.
        /// </summary>
        public string FamilyName
        {
            get => familyName;
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    value = Defaults.FamilyName;
                }

                familyName = value;
            }
        }

        private int size;
        /// <summary>
        /// Gets or sets the font size in pixels.
        /// </summary>
        public int Size
        {
            get => size;
            set
            {
                if (value <= 0)
                {
                    value = Defaults.Size;
                }

                size = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the font is bold.
        /// </summary>
        public bool Bold { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the font is italic.
        /// </summary>
        public bool Italic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the font is underline.
        /// </summary>
        public bool Underline { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the font is strikeout.
        /// </summary>
        public bool Strikeout { get; set; }


        /// <inheritdoc/>
        public override void SetDefaults()
        {
            Copy(Defaults);
        }


        /// <inheritdoc/>
        protected override void CopyProperties(IFontSettings source)
        {
            FamilyName = source.FamilyName;
            Size = source.Size;
            Bold = source.Bold;
            Italic = source.Italic;
            Underline = source.Underline;
            Strikeout = source.Strikeout;
        }
    }
}
