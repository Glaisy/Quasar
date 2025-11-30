//-----------------------------------------------------------------------
// <copyright file="SettingsBase.cs" company="Space Development">
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
    /// Abstract base class for settings objects.
    /// </summary>
    /// <seealso cref="ISettings" />
    public abstract class SettingsBase : ISettings
    {
        /// <inheritdoc/>
        public abstract void Copy(ISettings source);

        /// <inheritdoc/>
        public abstract void SetDefaults();
    }
}
