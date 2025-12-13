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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private List<DataSourceItem<FontFamily>> fontFamilyDataSource;
        private GeneratorSettings settings;
        private bool isRefreshEnabled;
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

            PopulateFontFamilies();
            LoadGeneratorSettings();
            ApplyGeneratorSettings();
            RefreshPreview();
        }


        /// <inheritdoc/>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            SaveGeneratorSettings();
        }


        private void ApplyGeneratorSettings()
        {
            isRefreshEnabled = false;

            var fontDataSettings = settings.FontDataSettings;
            var fontFamilyName = fontDataSettings.FontFamilyName ?? txtPreview.Font.Name;
            cbFontFamilies.SelectedItem = fontFamilyDataSource
                .FirstOrDefault(x => x.DisplayValue == fontFamilyName);

            udBaseSize.Value = fontDataSettings.BaseSize;
            txtFirstCharacter.Text = new string(fontDataSettings.FirstCharacter, 1);
            txtFallbackCharacter.Text = new string(fontDataSettings.FallbackCharacter, 1);
            udPageCount.Value = fontDataSettings.PageCount;
            udPadding.Value = fontDataSettings.Padding;
            udCharacterSpacing.Value = (decimal)fontDataSettings.CharacterSpacing;
            udHorizontalScale.Value = (decimal)fontDataSettings.HorizontalScale;
            txtFontFamilyNameOverride.Text = fontDataSettings.FontFamilyNameOverride;

            isRefreshEnabled = true;
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

        private void PopulateFontFamilies()
        {
            fontFamilyDataSource = new List<DataSourceItem<FontFamily>>();
            var fontFamilies = FontFamily.Families.OrderBy(x => x.Name);
            foreach (var fontFamily in fontFamilies)
            {
                fontFamilyDataSource.Add(new DataSourceItem<FontFamily>(fontFamily.Name, fontFamily));
            }

            cbFontFamilies.DisplayMember = nameof(DataSourceItem<FontFamily>.DisplayValue);
            cbFontFamilies.ValueMember = nameof(DataSourceItem<FontFamily>.Value);
            cbFontFamilies.DataSource = fontFamilyDataSource;
        }

        private void RefreshPreview()
        {
            if (!isRefreshEnabled)
            {
                return;
            }

            var fontDataSettings = settings.FontDataSettings;
            var font = new Font(
                fontDataSettings.FontFamilyName,
                fontDataSettings.BaseSize,
                FontStyle.Regular,
                GraphicsUnit.Pixel);
            previewBitmap = generatorService.GeneratePreviewBitmap(settings.FontDataSettings, font, Color.White, Color.Black);
            pnlPreview.Invalidate();
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

        #region Event handlers
        private void OnBaseSizeValueChanged(object sender, EventArgs e)
        {
            settings.FontDataSettings.BaseSize = (int)udBaseSize.Value;
            RefreshPreview();
        }

        private void OnCharacterSpacingValueChanged(object sender, EventArgs e)
        {
            settings.FontDataSettings.CharacterSpacing = (float)udCharacterSpacing.Value;
        }

        private void OnExportClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnExportDirectoryPathClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnFallbackCharacterTextChanged(object sender, EventArgs e)
        {
            var fallbackCharacter = txtFallbackCharacter.TextLength == 0 ? ' ' : txtFallbackCharacter.Text[0];
            settings.FontDataSettings.FallbackCharacter = fallbackCharacter;
        }

        private void OnFirstCharacterTextChanged(object sender, EventArgs e)
        {
            var firstCharacter = txtFirstCharacter.TextLength == 0 ? ' ' : txtFirstCharacter.Text[0];
            if (firstCharacter == settings.FontDataSettings.FirstCharacter)
            {
                return;
            }

            settings.FontDataSettings.FirstCharacter = firstCharacter;
            RefreshPreview();
        }

        private void OnFontFamiliesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFontFamilies.SelectedValue is not FontFamily selectedFontFamily)
            {
                return;
            }

            txtPreview.Font = new Font(selectedFontFamily, txtPreview.Font.Size, txtPreview.Font.Style);

            if (settings == null)
            {
                return;
            }

            settings.FontDataSettings.FontFamilyName = selectedFontFamily.Name;
            RefreshPreview();
        }

        private void OnHorizontalScaleValueChanged(object sender, EventArgs e)
        {
            settings.FontDataSettings.HorizontalScale = (float)udHorizontalScale.Value;
        }

        private void OnPaddingValueChanged(object sender, EventArgs e)
        {
            settings.FontDataSettings.Padding = (int)udPadding.Value;
            RefreshPreview();
        }

        private void OnFontFamilyNameOverrideTextChanged(object sender, EventArgs e)
        {
            var fontfamilyNameOverride = txtFontFamilyNameOverride.Text;
            if (String.IsNullOrEmpty(fontfamilyNameOverride))
            {
                fontfamilyNameOverride = null;
            }

            settings.FontDataSettings.FontFamilyNameOverride = fontfamilyNameOverride;
        }

        private void OnPageCountValueChanged(object sender, EventArgs e)
        {
            settings.FontDataSettings.PageCount = (int)udPageCount.Value;
            RefreshPreview();
        }

        private void OnPaintPreviewPanel(object sender, PaintEventArgs e)
        {
            if (previewBitmap == null)
            {
                return;
            }

            e.Graphics.DrawImage(previewBitmap, Point.Empty);
        }
        #endregion
    }
}
