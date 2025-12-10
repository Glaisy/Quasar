//-----------------------------------------------------------------------
// <copyright file="FontDataSettings.cs" company="Space Development">
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
    /// Font data settings object implementation.
    /// </summary>
    /// <seealso cref="SettingsBase{IFontDataSettings}" />
    /// <seealso cref="IFontDataSettings" />
    internal sealed class FontDataSettings : SettingsBase<IFontDataSettings>, IFontDataSettings
    {
        /// <summary>
        /// The defaults.
        /// </summary>
        public static readonly IFontDataSettings Defaults = new FontDataSettings
        {
            FirstCharacter = ' ',
            HorizontalScale = 1.0f,
            OffsetX = 0,
            OffsetY = 0,
            Padding = 0,
            PageCount = 1,
        };

        /// <summary>
        /// Gets or sets the first character.
        /// </summary>
        public char FirstCharacter { get; set; }

        private float horizontalScale;
        /// <summary>
        /// Gets or sets the horizontal scale.
        /// </summary>
        public float HorizontalScale
        {
            get => horizontalScale;
            set
            {
                if (value <= 0.0f)
                {
                    value = 1.0f;
                }

                horizontalScale = value;
            }
        }

        /// <summary>
        /// Gets or sets the horizontal offset.
        /// </summary>
        public int OffsetX { get; set; }

        /// <summary>
        /// Gets or sets the vertical offset.
        /// </summary>
        public int OffsetY { get; set; }

        private int padding;
        /// <summary>
        /// Gets or sets the padding.
        /// </summary>
        public int Padding
        {
            get => padding;
            set => padding = Math.Max(0, value);
        }

        private int pageCount;
        /// <summary>
        /// Gets or sets the number of character pages.
        /// </summary>
        public int PageCount
        {
            get => pageCount;
            set => pageCount = Math.Max(1, value);
        }


        /// <inheritdoc/>
        public override void SetDefaults()
        {
            Copy(Defaults);
        }


        /// <inheritdoc/>
        protected override void CopyProperties(IFontDataSettings source)
        {
            FirstCharacter = source.FirstCharacter;
            HorizontalScale = source.HorizontalScale;
            OffsetX = source.OffsetX;
            OffsetY = source.OffsetY;
            Padding = source.Padding;
            PageCount = source.PageCount;
        }
    }
}
