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

using System;
using System.IO;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics;

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
        private const string PlatformSpecificAssemblyNameFormatStringP1 = $"{nameof(Quasar)}.{{0}}.dll";


        private readonly IServiceProvider serviceProvider;
        private readonly IServiceLoader serviceLoader;
        private readonly IAudioDeviceContext[] audioDeviceContexts;


        /// <summary>
        /// Initializes a new instance of the <see cref="AudioDeviceContextFactory" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="serviceLoader">The service loader.</param>
        public AudioDeviceContextFactory(
            IServiceProvider serviceProvider,
            IServiceLoader serviceLoader)
        {
            this.serviceProvider = serviceProvider;
            this.serviceLoader = serviceLoader;

            var numberOfAudioPlatforms = Enum.GetValues<AudioPlatform>().Length;
            audioDeviceContexts = new IAudioDeviceContext[numberOfAudioPlatforms];
        }


        /// <inheritdoc/>
        public IAudioDeviceContext Create(AudioPlatform audioPlatform)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(audioPlatform == AudioPlatform.Unknown, true, nameof(audioPlatform));

            var audioDeviceContextIndex = (int)audioPlatform;
            var audioDeviceContext = audioDeviceContexts[audioDeviceContextIndex];
            if (audioDeviceContext == null)
            {
                audioDeviceContext = CreateAudioDeviceContext(audioPlatform);
                audioDeviceContexts[audioDeviceContextIndex] = audioDeviceContext;
            }

            return audioDeviceContext;
        }


        private void AddPlatformSpecificServices(AudioPlatform audioPlatform)
        {
            var platformSpecificAssemblyName = String.Format(PlatformSpecificAssemblyNameFormatStringP1, audioPlatform);
            var platformSpecificAssemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, platformSpecificAssemblyName);
            var platformSpecificAssembly = Assembly.LoadFile(platformSpecificAssemblyPath);
            if (platformSpecificAssembly == null)
            {
                throw new GraphicsException($"Audio platform specific assembly not found: {platformSpecificAssemblyPath}");
            }

            serviceLoader.AddExportedServices(platformSpecificAssembly);
        }

        private IAudioDeviceContext CreateAudioDeviceContext(AudioPlatform audioPlatform)
        {
            AddPlatformSpecificServices(audioPlatform);

            var audioDeviceContext = serviceProvider.GetRequiredKeyedService<IAudioDeviceContext>(audioPlatform);
            if (audioDeviceContext == null)
            {
                throw new AudioException($"Audio device context is not registered for graphics platform: {audioPlatform}");
            }

            return audioDeviceContext;
        }
    }
}
