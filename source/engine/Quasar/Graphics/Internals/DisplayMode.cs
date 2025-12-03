//-----------------------------------------------------------------------
// <copyright file="DisplayMode.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Text.Json.Serialization;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Graphics display mode object implementation.
    /// </summary>
    /// <seealso cref="IDisplayMode" />
    internal sealed class DisplayMode : IDisplayMode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayMode"/> class.
        /// </summary>
        public DisplayMode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayMode"/> class.
        /// </summary>
        /// <param name="resolution">The resolution.</param>
        /// <param name="bitsPerPixel">The bits per pixel.</param>
        /// <param name="refreshRate">The refresh rate.</param>
        public DisplayMode(in Size resolution, int bitsPerPixel, int refreshRate)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(resolution.Width, nameof(resolution.Width));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(resolution.Height, nameof(resolution.Height));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bitsPerPixel, nameof(bitsPerPixel));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(refreshRate, nameof(refreshRate));

            this.resolution = resolution;
            this.bitsPerPixel = bitsPerPixel;
            this.refreshRate = refreshRate;
            UpdateIdentifier();
        }


        private int bitsPerPixel;
        /// <summary>
        /// Gets or sets the number of bits per pixel.
        /// </summary>
        [JsonRequired]
        public int BitsPerPixel
        {
            get => bitsPerPixel;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(BitsPerPixel));
                if (bitsPerPixel == value)
                {
                    return;
                }

                bitsPerPixel = value;
                UpdateIdentifier();
            }
        }

        /// <inheritdoc/>
        [JsonIgnore]
        public string Id { get; private set; }

        private Size resolution;
        /// <summary>
        /// Gets or sets the resolution.
        /// </summary>
        [JsonRequired]
        public Size Resolution
        {
            get => resolution;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value.Width, nameof(Resolution.Width));
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value.Height, nameof(Resolution.Height));

                if (resolution == value)
                {
                    return;
                }

                resolution = value;
                UpdateIdentifier();
            }
        }

        private int refreshRate;
        /// <summary>
        /// Gets or sets the refresh rate.
        /// </summary>
        [JsonRequired]
        public int RefreshRate
        {
            get => refreshRate;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(RefreshRate));

                if (refreshRate == value)
                {
                    return;
                }

                refreshRate = value;
                UpdateIdentifier();
            }
        }


        /// <inheritdoc/>
        public override string ToString()
        {
            return Id;
        }


        private void UpdateIdentifier()
        {
            Id = $"{resolution.Width}x{resolution.Height}/{bitsPerPixel}@{refreshRate}";
        }
    }
}
