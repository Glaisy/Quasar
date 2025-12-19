//-----------------------------------------------------------------------
// <copyright file="LightSource.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Threading;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics;
using Quasar.Rendering.Processors.Internals;

using Space.Core;

namespace Quasar.Rendering
{
    /// <summary>
    /// Quasar rendering light source object implementation.
    /// </summary>
    /// <seealso cref="ILightSource" />
    public class LightSource : ILightSource
    {
        private static readonly Range<float> fovRange = new Range<float>(0.0f, 180.0f);

        private static int lastLightSourceId = 0;
        private static LightSourceCommandProcessor commandProcessor;


        /// <summary>
        /// Initializes a new instance of the <see cref="LightSource" /> class.
        /// </summary>
        /// <param name="type">The light source type.</param>
        /// <param name="isEnabled">The initial value of the IsEnabled property.</param>
        public LightSource(LightSourceType type = LightSourceType.Directional, bool isEnabled = true)
        {
            Type = type;
            this.isEnabled = isEnabled;

            Id = Interlocked.Increment(ref lastLightSourceId);
            UpdateEffectiveColor();

            SendCreateCommand();
        }


        private Color color = Color.White;
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color
        {
            get => color;
            set
            {
                if (color == value)
                {
                    return;
                }

                color = value;
                UpdateEffectiveColor();
            }
        }

        /// <inheritdoc/>
        public Color EffectiveColor { get; private set; }

        private bool isEnabled;
        /// <summary>
        /// Gets or sets a value indicating whether the light source is enabled or not.
        /// </summary>
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (isEnabled == value)
                {
                    return;
                }

                isEnabled = value;
                SendEnabledChangedCommand(value);
            }
        }


        private float fieldOfView = 30.0f;
        /// <summary>
        /// Gets or sets field of view angle [0...180][degrees] (only for spot light sources).
        /// </summary>
        public float FieldOfView
        {
            get => fieldOfView;
            set => fieldOfView = fovRange.Clamp(value);
        }

        /// <inheritdoc/>
        public int Id { get; }

        private float intensity = 1.0f;
        /// <summary>
        /// Gets or sets the intensity [0...1].
        /// </summary>
        public float Intensity
        {
            get => intensity;
            set
            {
                value = Ranges.FloatUnit.Clamp(value);
                if (intensity == value)
                {
                    return;
                }

                intensity = value;
                UpdateEffectiveColor();
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => Transform.Name;
            set => Transform.Name = value;
        }

        private float radius = 10.0f;
        /// <summary>
        /// Gets or sets the effective radius (only for spot and point light sources).
        /// </summary>
        public float Radius
        {
            get => radius;
            set => radius = Ranges.FloatPositive.Clamp(value);
        }

        /// <inheritdoc/>
        public int Timestamp => Transform.Timestamp;

        /// <summary>
        /// The transformation.
        /// </summary>
        public readonly Transform Transform = new Transform();

        /// <inheritdoc/>
        ITransform IRawLightSource.Transform => Transform;

        /// <inheritdoc/>
        public LightSourceType Type { get; }


        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not LightSource other)
            {
                return false;
            }

            return Id == other.Id;
        }

        /// <inheritdoc/>
        public bool Equals(IRawLightSource other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name ?? $"{Type} {nameof(LightSource)} #{Id}";
        }


        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            commandProcessor = serviceProvider.GetRequiredService<LightSourceCommandProcessor>();
        }

        private void SendCreateCommand()
        {
            commandProcessor.Add(new LightSourceCommand(this, LightSourceCommandType.EnabledChanged)
            {
                IsEnabled = IsEnabled
            });
        }

        private void SendEnabledChangedCommand(bool enabled)
        {
            commandProcessor.Add(new LightSourceCommand(this, LightSourceCommandType.EnabledChanged)
            {
                IsEnabled = IsEnabled
            });
        }

        private void UpdateEffectiveColor()
        {
            EffectiveColor = new Color(color.R * intensity, color.G * intensity, color.B * intensity, 1.0f);
        }
    }
}
