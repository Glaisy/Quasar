//-----------------------------------------------------------------------
// <copyright file="ALAudioDeviceContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Audio;
using Quasar.Audio.Internals;
using Quasar.Audio.Internals.Factories;
using Quasar.OpenAL.Api;
using Quasar.OpenAL.Audio.Factories;
using Quasar.OpenAL.Internals.Audio.Factories;
using Quasar.Utilities;

using Space.Core;
using Space.Core.DependencyInjection;

namespace Quasar.OpenAL.Audio
{
    /// <summary>
    /// OpenAL audio device context implementation.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IAudioDeviceContext" />
    [Export(typeof(AudioDeviceContextBase), AudioPlatform.OpenAL)]
    [Singleton]
    internal sealed class ALAudioDeviceContext : AudioDeviceContextBase, IAudioDeviceContext
    {
        private readonly IInteropFunctionProvider interopFunctionProvider;
        private readonly IServiceProvider serviceProvider;
        private readonly IServiceLoader serviceLoader;
        private IntPtr outputDeviceId;
        private IntPtr outputDeviceContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ALAudioDeviceContext" /> class.
        /// </summary>
        /// <param name="interopFunctionProvider">The interop function provider.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="serviceLoader">The service loader.</param>
        public ALAudioDeviceContext(
            [FromKeyedServices(AudioPlatform.OpenAL)] IInteropFunctionProvider interopFunctionProvider,
            IServiceProvider serviceProvider,
            IServiceLoader serviceLoader)
        {
            this.interopFunctionProvider = interopFunctionProvider;
            this.serviceProvider = serviceProvider;
            this.serviceLoader = serviceLoader;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (outputDeviceContext != IntPtr.Zero)
            {
                AL.DestroyContext(outputDeviceContext);
                outputDeviceContext = IntPtr.Zero;
            }

            if (outputDeviceId == IntPtr.Zero)
            {
                AL.CloseDevice(outputDeviceId);
                outputDeviceId = IntPtr.Zero;
            }

            outputDevice = null;
            inputDevice = null;
            version = null;
        }


        private IAudioInputDevice inputDevice;
        /// <inheritdoc/>
        public override IAudioInputDevice InputDevice => inputDevice;

        private IAudioOutputDevice outputDevice;
        /// <inheritdoc/>
        public override IAudioOutputDevice OutputDevice => outputDevice;

        /// <inheritdoc/>
        public override AudioPlatform Platform => AudioPlatform.OpenAL;

        private Version version;
        /// <inheritdoc/>
        public override Version Version => version;


        /// <inheritdoc/>
        public override void Initialize()
        {
            AL.Initialize(interopFunctionProvider);

            // initialize audio device provider
            var audioDeviceProvider = AddOpenALServiceImplementation<IAudioDeviceProvider, ALAudioDeviceProvider>();
            audioDeviceProvider.Initialize();

            // initialize active output device and version
            outputDevice = audioDeviceProvider.DefaultOutputDevice;
            outputDeviceId = AL.OpenDevice(OutputDevice.Name);
            if (outputDeviceId == IntPtr.Zero)
            {
                throw new OpenALException("Unable to open audio output device.");
            }

            // initialize output device context
            outputDeviceContext = AL.CreateContext(outputDeviceId, IntPtr.Zero);
            if (outputDeviceContext == IntPtr.Zero)
            {
                throw new OpenALException("Unable to create audio output device context.");
            }

            AL.MakeContextCurrent(outputDeviceContext);

            // initialize version
            var versionString = AL.GetString(StringType.Version);
            version = new Version(versionString);

            // initialize internal output components
            var audioListenerFactory = AddOpenALServiceImplementation<IAudioListenerFactory, ALAudioListenerFactory>();
            audioListenerFactory.Initialize(this);

            var audionSourceFactory = AddOpenALServiceImplementation<IAudioSourceFactory, ALAudioSourceFactory>();
            audionSourceFactory.Initialize(this);

            var soundEffectFactory = AddOpenALServiceImplementation<ISoundEffectFactory, ALSoundEffectFactory>();
            soundEffectFactory.Initialize(this);
        }


        private TImpl AddOpenALServiceImplementation<T, TImpl>()
            where TImpl : T
        {
            var service = serviceProvider.GetRequiredService<TImpl>();
            serviceLoader.AddSingleton(service);

            return service;
        }
    }
}
