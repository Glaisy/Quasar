//-----------------------------------------------------------------------
// <copyright file="AudioDeviceContextFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core.DependencyInjection;

namespace Quasar.Audio.Internals
{
    /// <summary>
    /// Audio device context factory implementation.
    /// </summary>
    /// <seealso cref="IAudioDeviceContextFactory" />
    [Export(typeof(IAudioDeviceContextFactory))]
    [Singleton]
    internal sealed class AudioDeviceContextFactory : IAudioDeviceContextFactory
    {
    }
}
