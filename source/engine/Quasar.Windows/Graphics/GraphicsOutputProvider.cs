//-----------------------------------------------------------------------
// <copyright file="GraphicsOutputProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Windows.Forms;

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Utilities;
using Quasar.Windows.Interop.User32;

using Space.Core.DependencyInjection;

namespace Quasar.Windows.Graphics
{
    /// <summary>
    /// Windows specific graphics output provider implementation.
    /// </summary>
    /// <seealso cref="IGraphicsOutputProvider" />
    [Export(typeof(IGraphicsOutputProvider))]
    [Singleton]
    internal sealed class GraphicsOutputProvider : IGraphicsOutputProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsOutputProvider"/> class.
        /// </summary>
        public GraphicsOutputProvider()
        {
            activeGraphicsOutput = CreateGraphicsOutput(Screen.PrimaryScreen);
            graphicsOutputs = new List<GraphicsOutput>(Screen.AllScreens.Length);
            foreach (var screen in Screen.AllScreens)
            {
                graphicsOutputs.Add(CreateGraphicsOutput(screen));
            }
        }


        private readonly GraphicsOutput activeGraphicsOutput;
        /// <inheritdoc/>
        public IGraphicsOutput ActiveGraphicsOutput => activeGraphicsOutput;

        private readonly List<GraphicsOutput> graphicsOutputs;
        /// <inheritdoc/>
        public IReadOnlyList<IGraphicsOutput> GraphicsOutputs => graphicsOutputs;


        private static unsafe GraphicsOutput CreateGraphicsOutput(Screen screen)
        {
            var displayDevice = GetDisplayDevice(screen.DeviceName);
            if (!displayDevice.HasValue)
            {
                throw new GraphicsException($"Unable to detect display device name for '{screen.DeviceName}'.");
            }

            var currentDisplayMode = GetCurrentDisplayMode(screen);
            if (currentDisplayMode == null)
            {
                throw new GraphicsException($"Unable to detect display settings for '{screen.DeviceName}'.");
            }

            // enumerate supported display modes
            var supportedDisplayModes = new List<DisplayMode>();
            for (var displayModeIndex = 0; ; displayModeIndex++)
            {
                var displayMode = GetDisplayMode(screen, displayModeIndex);
                if (displayMode == null)
                {
                    break;
                }

                supportedDisplayModes.Add(displayMode);
            }

            // create graphics output wrapper
            var displayDeviceInstance = displayDevice.Value;
            var deviceString = UnsafeUtility.GetString(displayDeviceInstance.DeviceString);
            return new GraphicsOutput(screen.DeviceName, deviceString, currentDisplayMode, supportedDisplayModes);
        }

        private static DisplayMode GetCurrentDisplayMode(Screen screen)
        {
            return GetDisplayMode(screen, (int)SettingsEnumerationType.Current);
        }

        private static DisplayMode GetDisplayMode(Screen screen, int displayModeIndex)
        {
            var deviceMode = default(DevMode);
            deviceMode.Init();

            if (!User32.EnumDisplaySettings(screen.DeviceName, displayModeIndex, ref deviceMode))
            {
                return null;
            }

            return new DisplayMode(
                new Size(deviceMode.PelsWidth, deviceMode.PelsHeight),
                deviceMode.BitsPerPel,
                deviceMode.DisplayFrequency);
        }

        private static DisplayDevice? GetDisplayDevice(string deviceName)
        {
            var displayDevice = default(DisplayDevice);
            displayDevice.Init();

            if (!User32.EnumDisplayDevices(deviceName, 0, ref displayDevice))
            {
                return default;
            }

            return displayDevice;
        }
    }
}
