//-----------------------------------------------------------------------
// <copyright file="ALAudioDeviceProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Quasar.Audio;
using Quasar.Audio.Internals;
using Quasar.OpenAL.Api;

using Space.Core.DependencyInjection;

namespace Quasar.OpenAL.Audio
{
    /// <summary>
    /// OpenAL audio output provider implementation.
    /// </summary>
    /// <seealso cref="IAudioDeviceProvider" />
    [Export(typeof(IAudioDeviceProvider), AudioPlatform.OpenAL)]
    [Singleton]
    internal sealed class ALAudioDeviceProvider : IAudioDeviceProvider
    {
        private static readonly IAudioDevice EmptyAudioDevice = new AudioDevice(null, null, null);
        private readonly List<IAudioDevice> outputDevices = new List<IAudioDevice>();
        private IAudioDevice defaultActiveOutputDevice;


        /// <inheritdoc/>
        public IReadOnlyList<IAudioDevice> GetOutputDevices()
        {
            return outputDevices;
        }

        /// <inheritdoc/>
        public IAudioDevice GetActiveOutputDevice()
        {
            return defaultActiveOutputDevice;
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            // enumerate output devices
            List<string> deviceNames;
            if (AL.IsExtensionPresent(IntPtr.Zero, ExtensionNames.ALC_enumeration_EXT))
            {
                if (AL.IsExtensionPresent(IntPtr.Zero, ExtensionNames.ALC_enumerate_all_EXT))
                {
                    deviceNames = AL.GetStrings(IntPtr.Zero, StringType.AllDeviceSpecifiers);
                }
                else
                {
                    deviceNames = AL.GetStrings(IntPtr.Zero, StringType.DeviceSpecifier);
                }

                foreach (var deviceName in deviceNames)
                {
                    var outputDevice = new AudioDevice(deviceName, deviceName, null);
                    outputDevices.Add(outputDevice);
                }
            }

            // find default active output device
            var defaultDeviceName = AL.GetString(IntPtr.Zero, StringType.DefaultDeviceSpecifier);
            if (!String.IsNullOrEmpty(defaultDeviceName))
            {
                defaultActiveOutputDevice = outputDevices.Find(device => device.Name == defaultDeviceName);
            }
            else
            {
                defaultActiveOutputDevice = EmptyAudioDevice;
            }
        }
    }
}
