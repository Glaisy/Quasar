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
        private static readonly AudioDevice EmptyAudioDevice = new AudioDevice(null, null);
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
            if (AL.IsExtensionPresent(IntPtr.Zero, ExtensionNames.ALC_ENUMERATION_EXT))
            {
                if (AL.IsExtensionPresent(IntPtr.Zero, ExtensionNames.ALC_ENUMERATE_ALL_EXT))
                {
                    deviceNames = AL.GetStrings(IntPtr.Zero, StringTypeExt.AllDeviceSpecifiers);
                }
                else
                {
                    deviceNames = AL.GetStrings(IntPtr.Zero, StringTypeExt.DeviceSpecifier);
                }

                foreach (var deviceName in deviceNames)
                {
                    var outputDevice = CreateAudioDevice(deviceName);
                    outputDevices.Add(outputDevice);
                }
            }

            // find default active output device
            var defaultDeviceName = AL.GetString(IntPtr.Zero, StringTypeExt.DefaultDeviceSpecifier);
            if (!String.IsNullOrEmpty(defaultDeviceName))
            {
                defaultActiveOutputDevice = outputDevices.Find(device => device.Name.Contains(defaultDeviceName));
            }

            defaultActiveOutputDevice ??= EmptyAudioDevice;
        }


        private AudioDevice CreateAudioDevice(string deviceName)
        {
            var deviceId = IntPtr.Zero;
            try
            {
                deviceId = AL.OpenDevice(deviceName);
                if (deviceId == IntPtr.Zero)
                {
                    return EmptyAudioDevice;
                }

                var vendor = AL.GetString(StringType.Vendor);
                return new AudioDevice(deviceName, vendor);
            }
            finally
            {
                if (deviceId != IntPtr.Zero)
                {
                    AL.CloseDevice(deviceId);
                }
            }
        }
    }
}
