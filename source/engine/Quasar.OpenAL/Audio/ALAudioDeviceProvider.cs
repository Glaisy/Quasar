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
    /// OpenAL audio device provider implementation.
    /// </summary>
    /// <seealso cref="IAudioDeviceProvider" />
    [Export]
    [Singleton]
    internal sealed class ALAudioDeviceProvider : IAudioDeviceProvider
    {
        private readonly List<string> deviceNames = new List<string>();


        /// <inheritdoc/>
        public IAudioInputDevice DefaultInputDevice { get; private set; }

        /// <inheritdoc/>
        public IAudioOutputDevice DefaultOutputDevice { get; private set; }

        private readonly List<AudioInputDevice> inputDevices = new List<AudioInputDevice>();
        /// <inheritdoc/>
        public IReadOnlyList<IAudioInputDevice> InputDevices => inputDevices;

        private readonly List<AudioOutputDevice> outputDevices = new List<AudioOutputDevice>();
        /// <inheritdoc/>
        public IReadOnlyList<IAudioOutputDevice> OutputDevices => outputDevices;


        /// <summary>
        /// Executes provider initialization.
        /// </summary>
        public void Initialize()
        {
            EnumerateAudioInputDevices();
            EnumerateAudioOutputDevices();
        }

        private void EnumerateAudioInputDevices()
        {
            try
            {
                var defaultDeviceName = AL.GetString(IntPtr.Zero, StringTypeExt.DefaultCaptureDeviceSpecifier);

                if (AL.IsExtensionPresent(IntPtr.Zero, ExtensionNames.ALC_ENUMERATION_EXT))
                {
                    // enumerate all input devices
                    AL.GetStrings(IntPtr.Zero, StringTypeExt.CaptureDeviceSpecifier, deviceNames);

                    foreach (var deviceName in deviceNames)
                    {
                        var inputDevice = CreateAudioInputDevice(deviceName);
                        inputDevices.Add(inputDevice);
                    }

                    // find default input device
                    DefaultInputDevice = inputDevices.Find(device => device.Name.Contains(defaultDeviceName));
                }
                else
                {
                    // create the default device
                    var inputDevice = CreateAudioInputDevice(defaultDeviceName);
                    inputDevices.Add(inputDevice);

                    DefaultInputDevice = inputDevice;
                }
            }
            catch
            {
                DefaultInputDevice = null;
                inputDevices.Clear();

                throw;
            }
        }

        private void EnumerateAudioOutputDevices()
        {
            try
            {
                var defaultDeviceName = AL.GetString(IntPtr.Zero, StringTypeExt.DefaultDeviceSpecifier);

                if (AL.IsExtensionPresent(IntPtr.Zero, ExtensionNames.ALC_ENUMERATION_EXT))
                {
                    // enumerate all output devices
                    AL.GetStrings(IntPtr.Zero, StringTypeExt.DeviceSpecifier, deviceNames);
                    foreach (var deviceName in deviceNames)
                    {
                        var outputDevice = CreateAudioOutputDevice(deviceName);
                        outputDevices.Add(outputDevice);
                    }

                    // find default output device
                    DefaultOutputDevice = outputDevices.Find(device => device.Name.Contains(defaultDeviceName));
                }
                else
                {
                    // create the default device
                    var outputDevice = CreateAudioOutputDevice(defaultDeviceName);
                    outputDevices.Add(outputDevice);

                    DefaultOutputDevice = outputDevice;
                }
            }
            catch
            {
                DefaultOutputDevice = null;
                outputDevices.Clear();

                throw;
            }
        }

        private static AudioInputDevice CreateAudioInputDevice(string deviceName)
        {
            return new AudioInputDevice(deviceName, null);
        }

        private static AudioOutputDevice CreateAudioOutputDevice(string deviceName)
        {
            return new AudioOutputDevice(deviceName, null);
        }
    }
}
