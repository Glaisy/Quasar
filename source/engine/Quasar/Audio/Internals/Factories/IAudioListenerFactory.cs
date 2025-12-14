//-----------------------------------------------------------------------
// <copyright file="IAudioListenerFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Audio.Internals.Factories
{
    /// <summary>
    /// Represents the internal audio listener factory component.
    /// </summary>
    internal interface IAudioListenerFactory
    {
        /// <summary>
        /// Creates a new the audio listener instance.
        /// </summary>
        /// <returns>
        /// The created audio listener instance.
        /// </returns>
        AudioListenerBase Create();
    }
}
