//-----------------------------------------------------------------------
// <copyright file="IDebugSettings.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

#if DEBUG
using Quasar.Graphics;

using Space.Core.Settings;

namespace Quasar.Diagnostics
{
    /// <summary>
    /// Debug settings interface definition.
    /// </summary>
    public interface IDebugSettings : ISettings
    {
        /// <summary>
        /// Gets the number of displayed messages [1...+Inf].
        /// </summary>
        int DisplayedMessages { get; }

        /// <summary>
        /// Gets the error text color.
        /// </summary>
        Color ErrorColor { get; }

        /// <summary>
        /// Gets the information text color.
        /// </summary>
        Color InfoColor { get; }

        /// <summary>
        /// Gets the message decay length [s].
        /// </summary>
        float MessageDecayLengthSeconds { get; }

        /// <summary>
        /// Gets the message decay start [s].
        /// </summary>
        float MessageDecayStartSeconds { get; }

        /// <summary>
        /// Gets the warning text color.
        /// </summary>
        Color WarningColor { get; }
    }
}
#endif