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
using Quasar.Utilities;

using Space.Core;
using Space.Core.DependencyInjection;

namespace Quasar.OpenAL.Audio
{
    /// <summary>
    /// OpenAL graphics context implementation.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IAudioDeviceContext" />
    [Export(typeof(IAudioDeviceContext), AudioPlatform.OpenAL)]
    [Singleton]
    internal sealed class ALAudioDeviceContext : DisposableBase, IAudioDeviceContext
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IServiceLoader serviceLoader;
        private IntPtr deviceId;
        private IntPtr deviceContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="ALAudioDeviceContext" /> class.
        /// </summary>
        /// <param name="audioDeviceProvider">The audio device provider.</param>
        /// <param name="interopFunctionProvider">The interop function provider.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="serviceLoader">The service loader.</param>
        public ALAudioDeviceContext(
            [FromKeyedServices(AudioPlatform.OpenAL)] IAudioDeviceProvider audioDeviceProvider,
            [FromKeyedServices(AudioPlatform.OpenAL)] IInteropFunctionProvider interopFunctionProvider,
            IServiceProvider serviceProvider,
            IServiceLoader serviceLoader)
        {
            this.serviceProvider = serviceProvider;
            this.serviceLoader = serviceLoader;

            AL.Initialize(interopFunctionProvider);
            audioDeviceProvider.Initialize();

            // initialize active output device and version
            OutputDevice = audioDeviceProvider.GetActiveOutputDevice();
            if (!String.IsNullOrEmpty(OutputDevice.Name))
            {
                deviceId = AL.OpenDevice(OutputDevice.Name);
                AL.CheckErrors();

                // initialize device context
                deviceContext = AL.CreateContext(deviceId, IntPtr.Zero);
                AL.MakeContextCurrent(deviceContext);
                AL.CheckErrors();
            }

            var majorVersion = AL.GetInteger(deviceId, IntegerType.MajorVersion);
            var minorVersion = AL.GetInteger(deviceId, IntegerType.MinorVersion);
            Version = new Version(majorVersion, minorVersion);

            // initialize internal components
            AddOpenALServiceImplementation<IAudioDeviceProvider>();
            AddOpenALServiceImplementation<IAudioListenerProvider>();
            AddOpenALServiceImplementation<IAudioSourceFactory>();
            AddOpenALServiceImplementation<ISoundEffectFactory>();
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (deviceContext != IntPtr.Zero)
            {
                AL.DestroyContext(deviceContext);
                deviceContext = IntPtr.Zero;
            }

            if (deviceId == IntPtr.Zero)
            {
                AL.CloseDevice(deviceId);
                deviceId = IntPtr.Zero;
            }

            OutputDevice = null;
            Version = null;
        }


        /// <inheritdoc/>
        public IAudioDevice OutputDevice { get; private set; }

        /// <inheritdoc/>
        public AudioPlatform Platform => AudioPlatform.OpenAL;

        /// <inheritdoc/>
        public Version Version { get; private set; }


        private T AddOpenALServiceImplementation<T>()
        {
            var service = serviceProvider.GetRequiredKeyedService<T>(AudioPlatform.OpenAL);
            serviceLoader.AddSingleton(service);

            return service;
        }
    }
}
