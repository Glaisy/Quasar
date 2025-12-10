//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

using Quasar.FontGenerator.Models;
using Quasar.FontGenerator.Services;

namespace Quasar.UI
{
    /// <summary>
    /// Application's main form.
    /// </summary>
    /// <seealso cref="Form" />
    public partial class MainForm : Form
    {
        private const string SettingsFileName = "settings.config";

        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        private readonly GeneratorService generatorService = new GeneratorService();
        private readonly string settingsFilePath;
        private GeneratorSettings settings;
        private Bitmap previewBitmap;


        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsFileName);
        }


        /// <inheritdoc/>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadGeneratorSettings();

            // TODO: remove this test code.
            var font = new Font("Segoe UI", 32, FontStyle.Regular, GraphicsUnit.Pixel);
            previewBitmap = generatorService.GeneratePreviewBitmap(settings.FontDataSettings, font, Color.White, Color.Black);
        }

        /// <inheritdoc/>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            SaveGeneratorSettings();
        }


        private void PaintPreviewPanel(object sender, PaintEventArgs e)
        {
            if (previewBitmap == null)
            {
                return;
            }

            e.Graphics.DrawImage(previewBitmap, Point.Empty);
        }

        private void LoadGeneratorSettings()
        {
            Stream stream = null;
            try
            {
                stream = File.OpenRead(settingsFilePath);

                settings = JsonSerializer.Deserialize<GeneratorSettings>(stream, jsonSerializerOptions);
            }
            catch
            {
                settings = new GeneratorSettings();
                settings.SetDefaults();
            }
            finally
            {
                stream?.Dispose();
            }
        }

        private void SaveGeneratorSettings()
        {
            Stream stream = null;
            try
            {
                stream = File.Create(settingsFilePath);
                JsonSerializer.Serialize(stream, settings, jsonSerializerOptions);
            }
            catch
            {
            }
            finally
            {
                stream?.Dispose();
            }
        }
    }
}
