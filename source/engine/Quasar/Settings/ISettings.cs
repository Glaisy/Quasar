//-----------------------------------------------------------------------
// <copyright file="ISettings.cs" company="Space Development">
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
    /// General interface for settings objects.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Copies the settings values from the source.
        /// </summary>
        /// <param name="source">The source.</param>
        void Copy(ISettings source);

        /// <summary>
        /// Sets the default setting values.
        /// </summary>
        void SetDefaults();
    }
}
