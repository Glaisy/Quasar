//-----------------------------------------------------------------------
// <copyright file="IQuasarSettings.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core.Diagnostics;

namespace Quasar.Settings
{
    /// <summary>
    /// Represents the main settings file for the Quasar engine.
    /// </summary>
    /// <seealso cref="ISettings" />
    public interface IQuasarSettings : ISettings
    {
        /// <summary>
        /// Gets the log level.
        /// </summary>
        LogLevel LogLevel { get; }
    }
}
