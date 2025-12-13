//-----------------------------------------------------------------------
// <copyright file="IAudioListenerProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Audio.Internals
{
    /// <summary>
    /// Internal audio listener provider interface definition.
    /// </summary>
    internal interface IAudioListenerProvider
    {
        /// <summary>
        /// Gets the audio listener.
        /// </summary>
        /// <returns>The audio listener instance.</returns>
        AudioListenerBase GetListener();
    }
}
