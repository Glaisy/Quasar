//-----------------------------------------------------------------------
// <copyright file="DebugTextService.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------
#if DEBUG
using System.Collections.Generic;

using Quasar.Collections;
using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Pipelines;
using Quasar.Rendering.Internals;
using Quasar.Settings;
using Quasar.UI.Internals;

using Space.Core;
using Space.Core.Collections;
using Space.Core.DependencyInjection;

namespace Quasar.Diagnostics.Pipeline.Internals
{
    /// <summary>
    /// Internal debug text service to create and manage debug text messages.
    /// </summary>
    /// <seealso cref="DebugTextService" />
    [Export]
    [Singleton]
    internal sealed class DebugTextService
    {
        private const int FontSize = 12;
        private const float DecayStart = 3f;
        private const float DecayLength = 0.3f;
        private static readonly Vector2 Margin = new Vector2(32.0f, 20.0f);


        private readonly ISettingsService settingsService;
        private readonly ITimeProvider timeProvider;
        private readonly TextMeshProvider textMeshProvider;
        private readonly IPool<DebugTextEntry> debugTextEntryPool;
        private readonly ValueTypeCollection<UIElement> uiElements = new ValueTypeCollection<UIElement>();
        private readonly List<DebugTextEntry> debugTextEntries = new List<DebugTextEntry>();
        private Font font;
        private IDebugSettings settings;


        /// <summary>
        /// Initializes a new instance of the <see cref="DebugTextService" /> class.
        /// </summary>
        /// <param name="textMeshProvider">The text mesh provider.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="timeProvider">The time provider.</param>
        /// <param name="poolFactory">The pool factory.</param>
        public DebugTextService(
            TextMeshProvider textMeshProvider,
            ISettingsService settingsService,
            ITimeProvider timeProvider,
            IPoolFactory poolFactory)
        {
            this.settingsService = settingsService;
            this.timeProvider = timeProvider;
            this.textMeshProvider = textMeshProvider;

            debugTextEntryPool = poolFactory.Create(true, () => new DebugTextEntry());
        }


        /// <summary>
        /// Adds the debug text.
        /// </summary>
        /// <param name="type">The text type.</param>
        /// <param name="text">The text value.</param>
        public void Add(DebugTextType type, string text)
        {
            Assertion.ThrowIfNullOrEmpty(text, nameof(text));

            lock (debugTextEntries)
            {
                // get text entry: remove last or create new
                DebugTextEntry debugTextEntry;
                if (debugTextEntries.Count == settings.DisplayedMessages)
                {
                    var lastIndex = debugTextEntries.Count - 1;
                    debugTextEntry = debugTextEntries[lastIndex];
                    debugTextEntries.RemoveAt(lastIndex);
                }
                else
                {
                    debugTextEntry = debugTextEntryPool.Allocate();
                }

                // initialize text entry
                var textSize = font.MeasureString(text);
                debugTextEntry.Height = textSize.Y;
                debugTextEntry.Text = text;
                debugTextEntry.Timestamp = timeProvider.Time;
                switch (type)
                {
                    case DebugTextType.Error:
                        debugTextEntry.Color = settings.ErrorColor;
                        break;
                    case DebugTextType.Warning:
                        debugTextEntry.Color = settings.WarningColor;
                        break;
                    default:
                        debugTextEntry.Color = settings.InfoColor;
                        break;
                }

                // insert the entry
                debugTextEntries.Insert(0, debugTextEntry);
            }
        }

        /// <summary>
        /// Gets the collection of UI elements to render.
        /// </summary>
        public ValueTypeCollection<UIElement> GetUIElements()
        {
            lock (debugTextEntries)
            {
                uiElements.Clear();

                // apply time decay
                var now = timeProvider.Time;
                for (var i = debugTextEntries.Count - 1; i >= 0; i--)
                {
                    var debugTextEntry = debugTextEntries[i];
                    var lifeTime = now - debugTextEntry.Timestamp;
                    if (lifeTime < DecayStart)
                    {
                        break;
                    }

                    lifeTime -= DecayStart;
                    if (lifeTime > DecayLength)
                    {
                        // lifetime exceeded, remove
                        debugTextEntries.RemoveAt(i);
                        debugTextEntryPool.Release(debugTextEntry);
                        continue;
                    }

                    // decaying
                    var alpha = 1.0f - lifeTime / DecayLength;
                    debugTextEntry.Color = new Color(debugTextEntry.Color, alpha);
                }

                // convert entries to elements
                var y = Margin.Y;
                foreach (var debugTextEntry in debugTextEntries)
                {
                    var position = new Vector2(Margin.X, y);
                    var mesh = textMeshProvider.Get(font, debugTextEntry.Text, 0, debugTextEntry.Text.Length);
                    uiElements.Add(new UIElement(position, Vector2.One, mesh, font.Texture, debugTextEntry.Color));

                    y += debugTextEntry.Height;
                }
            }

            return uiElements;
        }

        /// <summary>
        /// Initializes the service instance.
        /// </summary>
        public void Initialize()
        {
            font = new Font(FontFamilyConstants.DefaultName, FontSize);
            settings = settingsService.Get<IDebugSettings>();
        }
    }
}
#endif