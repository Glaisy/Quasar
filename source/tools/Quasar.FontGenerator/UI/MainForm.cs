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
        private FontStyle previewFontStyle;
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
            PopulateFontStyles();
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

            udBaseSize.Value = fontDataSettings.BaseSize;
            txtFirstCharacter.Text = new string(fontDataSettings.FirstCharacter, 1);
            txtFallbackCharacter.Text = new string(fontDataSettings.FallbackCharacter, 1);
            udPageCount.Value = fontDataSettings.PageCount;
            udPadding.Value = fontDataSettings.Padding;
            udCharacterSpacing.Value = (decimal)fontDataSettings.CharacterSpacing;
            udHorizontalOffset.Value = (decimal)fontDataSettings.HorizontalOffset;
            udHorizontalScale.Value = (decimal)fontDataSettings.HorizontalScale;
            txtFontFamilyNameOverride.Text = fontDataSettings.FontFamilyNameOverride;
            udVerticalOffset.Value = (decimal)fontDataSettings.VerticalOffset;

            var fontFamilyName = fontDataSettings.FontFamilyName ?? txtPreview.Font.Name;
            cbFontFamilies.SelectedItem = fontFamilyDataSource
                .FirstOrDefault(x => x.DisplayValue == fontFamilyName);

            if (fontDataSettings.GeneratedStyles.Count == 0)
            {
                clbFontStyles.SetItemChecked(0, true);
            }
            else
            {
                clbFontStyles.SetItemChecked(0, true);
                foreach (var fontStyle in fontDataSettings.GeneratedStyles)
                {
                    clbFontStyles.SetItemChecked((int)fontStyle, true);
                }
            }

            string exportDirectoryPath;
            string exportFileName;
            if (String.IsNullOrEmpty(settings.ExportFilePath))
            {
                exportDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
                exportFileName = null;
            }
            else
            {
                exportDirectoryPath = Path.GetDirectoryName(settings.ExportFilePath);
                exportFileName = Path.GetFileName(settings.ExportFilePath);
                if (!Path.IsPathRooted(exportDirectoryPath))
                {
                    exportDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, exportDirectoryPath);
                }

                if (!Directory.Exists(exportDirectoryPath))
                {
                    Directory.CreateDirectory(exportDirectoryPath);
                }
            }

            exportDialog.InitialDirectory = exportDirectoryPath;
            if (!String.IsNullOrEmpty(exportFileName))
            {
                txtExportFilePath.Text = Path.Combine(exportDirectoryPath, exportFileName);
                txtExportFilePath.SelectionStart = txtExportFilePath.Text.Length;
                txtExportFilePath.SelectionLength = 0;
                txtExportFilePath.ScrollToCaret();
                btnExport.Enabled = true;
            }
            else
            {
                txtExportFilePath.Text = null;
                btnExport.Enabled = false;
            }

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

        private void PopulateFontStyles()
        {
            var fontStylesCount = (int)(FontStyle.Regular | FontStyle.Bold | FontStyle.Italic) + 1;
            for (var i = 0; i < fontStylesCount; i++)
            {
                var fontStyle = (FontStyle)i;
                var item = new DataSourceItem<FontStyle>(fontStyle.ToString(), fontStyle);
                clbFontStyles.Items.Add(item, false);
            }
        }

        private void RefreshPreview()
        {
            if (!isRefreshEnabled)
            {
                return;
            }

            previewBitmap = generatorService.GeneratePreviewBitmap(settings.FontDataSettings, previewFontStyle, Color.White, Color.Black);

            UpdateScrollBarLimits();
            pnlPreview.PreviewBitmap = previewBitmap;
        }

        private void UpdateScrollBarLimits()
        {
            isRefreshEnabled = false;

            var maximum = 0;
            if (previewBitmap != null)
            {
                maximum = Math.Max(0, previewBitmap.Height - pnlPreview.ClientSize.Height + sbVertical.LargeChange);
            }

            sbVertical.Value = Math.Min(maximum, sbVertical.Value);
            sbVertical.Maximum = maximum;
            pnlPreview.Offset = sbVertical.Value;

            isRefreshEnabled = true;
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
            var successed = generatorService.ExportFont(settings.FontDataSettings, settings.ExportFilePath);
            if (successed)
            {
                MessageBox.Show(this, "The font file is successfully exported.", null, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "The font file export has failed.", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnExportDirectoryPathClick(object sender, MouseEventArgs e)
        {
            if (exportDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            settings.ExportFilePath = exportDialog.FileName;
            txtExportFilePath.Text = exportDialog.FileName;
            txtExportFilePath.SelectionStart = txtExportFilePath.Text.Length;
            txtExportFilePath.SelectionLength = 0;
            txtExportFilePath.ScrollToCaret();
            btnExport.Enabled = true;
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

        private void OnFontStylesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (clbFontStyles.SelectedItem is not DataSourceItem<FontStyle> selectedItem ||
                selectedItem.Value == previewFontStyle)
            {
                return;
            }

            previewFontStyle = selectedItem.Value;
            RefreshPreview();
        }

        private void OnFontStylesItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!isRefreshEnabled)
            {
                return;
            }

            if (e.Index == 0 && e.NewValue != CheckState.Checked)
            {
                e.NewValue = CheckState.Checked;
                return;
            }

            var generatedStyles = settings.FontDataSettings.GeneratedStyles;
            generatedStyles.Clear();
            for (var i = 0; i < clbFontStyles.Items.Count; i++)
            {
                if (i == e.Index)
                {
                    if (e.NewValue == CheckState.Checked)
                    {
                        generatedStyles.Add((Graphics.FontStyle)i);
                        continue;
                    }
                }

                if (clbFontStyles.GetItemChecked(i))
                {
                    generatedStyles.Add((Graphics.FontStyle)i);
                }
            }
        }

        private void OnHorizontalOffsetValueChanged(object sender, EventArgs e)
        {
            settings.FontDataSettings.HorizontalOffset = (int)udHorizontalOffset.Value;
            RefreshPreview();
        }

        private void OnHorizontalScaleValueChanged(object sender, EventArgs e)
        {
            settings.FontDataSettings.HorizontalScale = (float)udHorizontalScale.Value;
            RefreshPreview();
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

        private void OnPreviewSizeChanged(object sender, EventArgs e)
        {
            UpdateScrollBarLimits();
        }

        private void OnVerticalOffsetValueChanged(object sender, EventArgs e)
        {
            settings.FontDataSettings.VerticalOffset = (int)udVerticalOffset.Value;
            RefreshPreview();
        }

        private void OnVerticalScrollbarValueChanged(object sender, EventArgs e)
        {
            if (!isRefreshEnabled)
            {
                return;
            }

            pnlPreview.Offset = sbVertical.Value;
        }
        #endregion
    }
}
