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
        };


        /// <inheritdoc/>
        public override void SetDefaults()
        {
            Copy(Defaults);
        }

        /// <inheritdoc/>
        protected override void CopyProperties(IFontDataSettings source)
        {
        }
    }
}
