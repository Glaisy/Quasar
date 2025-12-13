namespace Quasar.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gbFontStyleSettings = new System.Windows.Forms.GroupBox();
            txtPreview = new System.Windows.Forms.TextBox();
            lblFontFamily = new System.Windows.Forms.Label();
            cbFontFamilies = new System.Windows.Forms.ComboBox();
            clbFontStyles = new System.Windows.Forms.CheckedListBox();
            gbPreview = new System.Windows.Forms.GroupBox();
            sbVertical = new System.Windows.Forms.VScrollBar();
            pnlPreview = new System.Windows.Forms.Panel();
            gbFontGenerationSettings = new System.Windows.Forms.GroupBox();
            lblFontNameOverride = new System.Windows.Forms.Label();
            txtFontFamilyNameOverride = new System.Windows.Forms.TextBox();
            udHorizontalScale = new System.Windows.Forms.NumericUpDown();
            udCharacterSpacing = new System.Windows.Forms.NumericUpDown();
            udPadding = new System.Windows.Forms.NumericUpDown();
            udPageCount = new System.Windows.Forms.NumericUpDown();
            txtFallbackCharacter = new System.Windows.Forms.TextBox();
            txtFirstCharacter = new System.Windows.Forms.TextBox();
            lblHorizontalScale = new System.Windows.Forms.Label();
            lblCharacterSpacing = new System.Windows.Forms.Label();
            lblPageCount = new System.Windows.Forms.Label();
            lblPadding = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            lblFallbackCharacter = new System.Windows.Forms.Label();
            udBaseSize = new System.Windows.Forms.NumericUpDown();
            lblBaseSize = new System.Windows.Forms.Label();
            gbExportSettings = new System.Windows.Forms.GroupBox();
            btnExport = new System.Windows.Forms.Button();
            btnExportDirectoryPath = new System.Windows.Forms.Button();
            txtExportDirectoryPath = new System.Windows.Forms.TextBox();
            gbFontStyleSettings.SuspendLayout();
            gbPreview.SuspendLayout();
            gbFontGenerationSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)udHorizontalScale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)udCharacterSpacing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)udPadding).BeginInit();
            ((System.ComponentModel.ISupportInitialize)udPageCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)udBaseSize).BeginInit();
            gbExportSettings.SuspendLayout();
            SuspendLayout();
            // 
            // gbFontStyleSettings
            // 
            gbFontStyleSettings.Controls.Add(txtPreview);
            gbFontStyleSettings.Controls.Add(lblFontFamily);
            gbFontStyleSettings.Controls.Add(cbFontFamilies);
            gbFontStyleSettings.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gbFontStyleSettings.Location = new System.Drawing.Point(12, 4);
            gbFontStyleSettings.Margin = new System.Windows.Forms.Padding(4);
            gbFontStyleSettings.Name = "gbFontStyleSettings";
            gbFontStyleSettings.Padding = new System.Windows.Forms.Padding(4);
            gbFontStyleSettings.Size = new System.Drawing.Size(324, 288);
            gbFontStyleSettings.TabIndex = 0;
            gbFontStyleSettings.TabStop = false;
            gbFontStyleSettings.Text = "Font style settings";
            // 
            // txtPreview
            // 
            txtPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtPreview.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtPreview.Location = new System.Drawing.Point(10, 73);
            txtPreview.Multiline = true;
            txtPreview.Name = "txtPreview";
            txtPreview.ReadOnly = true;
            txtPreview.Size = new System.Drawing.Size(307, 208);
            txtPreview.TabIndex = 2;
            txtPreview.Text = "The quick brown fox jumps over the lazy dog";
            // 
            // lblFontFamily
            // 
            lblFontFamily.AutoSize = true;
            lblFontFamily.Location = new System.Drawing.Point(10, 42);
            lblFontFamily.Name = "lblFontFamily";
            lblFontFamily.Size = new System.Drawing.Size(87, 20);
            lblFontFamily.TabIndex = 0;
            lblFontFamily.Text = "Font Family:";
            // 
            // cbFontFamilies
            // 
            cbFontFamilies.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cbFontFamilies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbFontFamilies.FormattingEnabled = true;
            cbFontFamilies.Location = new System.Drawing.Point(103, 39);
            cbFontFamilies.Name = "cbFontFamilies";
            cbFontFamilies.Size = new System.Drawing.Size(214, 28);
            cbFontFamilies.TabIndex = 1;
            cbFontFamilies.SelectedIndexChanged += OnFontFamiliesSelectedIndexChanged;
            // 
            // clbFontStyles
            // 
            clbFontStyles.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            clbFontStyles.CheckOnClick = true;
            clbFontStyles.ColumnWidth = 121;
            clbFontStyles.Items.AddRange(new object[] { "Regular", "Bold", "Italic", "Strikeout" });
            clbFontStyles.Location = new System.Drawing.Point(170, 31);
            clbFontStyles.Name = "clbFontStyles";
            clbFontStyles.Size = new System.Drawing.Size(147, 224);
            clbFontStyles.TabIndex = 11;
            clbFontStyles.TabStop = false;
            // 
            // gbPreview
            // 
            gbPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gbPreview.Controls.Add(sbVertical);
            gbPreview.Controls.Add(pnlPreview);
            gbPreview.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            gbPreview.Location = new System.Drawing.Point(352, 4);
            gbPreview.Margin = new System.Windows.Forms.Padding(4);
            gbPreview.Name = "gbPreview";
            gbPreview.Padding = new System.Windows.Forms.Padding(4);
            gbPreview.Size = new System.Drawing.Size(1055, 744);
            gbPreview.TabIndex = 1;
            gbPreview.TabStop = false;
            gbPreview.Text = "Preview";
            // 
            // sbVertical
            // 
            sbVertical.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            sbVertical.Location = new System.Drawing.Point(1034, 25);
            sbVertical.Name = "sbVertical";
            sbVertical.Size = new System.Drawing.Size(17, 712);
            sbVertical.TabIndex = 1;
            // 
            // pnlPreview
            // 
            pnlPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlPreview.BackColor = System.Drawing.SystemColors.Control;
            pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlPreview.Location = new System.Drawing.Point(7, 25);
            pnlPreview.Name = "pnlPreview";
            pnlPreview.Size = new System.Drawing.Size(1024, 712);
            pnlPreview.TabIndex = 0;
            pnlPreview.Paint += OnPaintPreviewPanel;
            // 
            // gbFontGenerationSettings
            // 
            gbFontGenerationSettings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            gbFontGenerationSettings.Controls.Add(lblFontNameOverride);
            gbFontGenerationSettings.Controls.Add(txtFontFamilyNameOverride);
            gbFontGenerationSettings.Controls.Add(udHorizontalScale);
            gbFontGenerationSettings.Controls.Add(udCharacterSpacing);
            gbFontGenerationSettings.Controls.Add(udPadding);
            gbFontGenerationSettings.Controls.Add(udPageCount);
            gbFontGenerationSettings.Controls.Add(txtFallbackCharacter);
            gbFontGenerationSettings.Controls.Add(txtFirstCharacter);
            gbFontGenerationSettings.Controls.Add(lblHorizontalScale);
            gbFontGenerationSettings.Controls.Add(lblCharacterSpacing);
            gbFontGenerationSettings.Controls.Add(lblPageCount);
            gbFontGenerationSettings.Controls.Add(lblPadding);
            gbFontGenerationSettings.Controls.Add(label2);
            gbFontGenerationSettings.Controls.Add(lblFallbackCharacter);
            gbFontGenerationSettings.Controls.Add(udBaseSize);
            gbFontGenerationSettings.Controls.Add(lblBaseSize);
            gbFontGenerationSettings.Controls.Add(clbFontStyles);
            gbFontGenerationSettings.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            gbFontGenerationSettings.Location = new System.Drawing.Point(12, 307);
            gbFontGenerationSettings.Margin = new System.Windows.Forms.Padding(4);
            gbFontGenerationSettings.Name = "gbFontGenerationSettings";
            gbFontGenerationSettings.Padding = new System.Windows.Forms.Padding(4);
            gbFontGenerationSettings.Size = new System.Drawing.Size(324, 310);
            gbFontGenerationSettings.TabIndex = 2;
            gbFontGenerationSettings.TabStop = false;
            gbFontGenerationSettings.Text = "Font generation settings";
            // 
            // lblFontNameOverride
            // 
            lblFontNameOverride.AutoSize = true;
            lblFontNameOverride.Location = new System.Drawing.Point(10, 272);
            lblFontNameOverride.Name = "lblFontNameOverride";
            lblFontNameOverride.Size = new System.Drawing.Size(82, 20);
            lblFontNameOverride.TabIndex = 17;
            lblFontNameOverride.Text = "Font name:";
            // 
            // txtFontFamilyNameOverride
            // 
            txtFontFamilyNameOverride.Location = new System.Drawing.Point(103, 269);
            txtFontFamilyNameOverride.Name = "txtFontFamilyNameOverride";
            txtFontFamilyNameOverride.Size = new System.Drawing.Size(214, 27);
            txtFontFamilyNameOverride.TabIndex = 12;
            txtFontFamilyNameOverride.TextChanged += OnFontFamilyNameOverrideTextChanged;
            // 
            // udHorizontalScale
            // 
            udHorizontalScale.DecimalPlaces = 1;
            udHorizontalScale.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            udHorizontalScale.Location = new System.Drawing.Point(106, 236);
            udHorizontalScale.Maximum = new decimal(new int[] { 20, 0, 0, 65536 });
            udHorizontalScale.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
            udHorizontalScale.Name = "udHorizontalScale";
            udHorizontalScale.Size = new System.Drawing.Size(49, 27);
            udHorizontalScale.TabIndex = 9;
            udHorizontalScale.Value = new decimal(new int[] { 1, 0, 0, 0 });
            udHorizontalScale.ValueChanged += OnHorizontalScaleValueChanged;
            // 
            // udCharacterSpacing
            // 
            udCharacterSpacing.DecimalPlaces = 1;
            udCharacterSpacing.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            udCharacterSpacing.Location = new System.Drawing.Point(106, 202);
            udCharacterSpacing.Maximum = new decimal(new int[] { 20, 0, 0, 65536 });
            udCharacterSpacing.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
            udCharacterSpacing.Name = "udCharacterSpacing";
            udCharacterSpacing.Size = new System.Drawing.Size(49, 27);
            udCharacterSpacing.TabIndex = 8;
            udCharacterSpacing.Value = new decimal(new int[] { 1, 0, 0, 0 });
            udCharacterSpacing.ValueChanged += OnCharacterSpacingValueChanged;
            // 
            // udPadding
            // 
            udPadding.Location = new System.Drawing.Point(106, 167);
            udPadding.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            udPadding.Name = "udPadding";
            udPadding.Size = new System.Drawing.Size(49, 27);
            udPadding.TabIndex = 7;
            udPadding.ValueChanged += OnPaddingValueChanged;
            // 
            // udPageCount
            // 
            udPageCount.Location = new System.Drawing.Point(106, 132);
            udPageCount.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            udPageCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            udPageCount.Name = "udPageCount";
            udPageCount.Size = new System.Drawing.Size(49, 27);
            udPageCount.TabIndex = 6;
            udPageCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            udPageCount.ValueChanged += OnPageCountValueChanged;
            // 
            // txtFallbackCharacter
            // 
            txtFallbackCharacter.Location = new System.Drawing.Point(106, 98);
            txtFallbackCharacter.MaxLength = 1;
            txtFallbackCharacter.Name = "txtFallbackCharacter";
            txtFallbackCharacter.Size = new System.Drawing.Size(49, 27);
            txtFallbackCharacter.TabIndex = 5;
            txtFallbackCharacter.TextChanged += OnFallbackCharacterTextChanged;
            // 
            // txtFirstCharacter
            // 
            txtFirstCharacter.Location = new System.Drawing.Point(106, 65);
            txtFirstCharacter.MaxLength = 1;
            txtFirstCharacter.Name = "txtFirstCharacter";
            txtFirstCharacter.Size = new System.Drawing.Size(49, 27);
            txtFirstCharacter.TabIndex = 4;
            txtFirstCharacter.TextChanged += OnFirstCharacterTextChanged;
            // 
            // lblHorizontalScale
            // 
            lblHorizontalScale.AutoSize = true;
            lblHorizontalScale.Location = new System.Drawing.Point(10, 238);
            lblHorizontalScale.Name = "lblHorizontalScale";
            lblHorizontalScale.Size = new System.Drawing.Size(77, 20);
            lblHorizontalScale.TabIndex = 15;
            lblHorizontalScale.Text = "Hor. scale:";
            // 
            // lblCharacterSpacing
            // 
            lblCharacterSpacing.AutoSize = true;
            lblCharacterSpacing.Location = new System.Drawing.Point(10, 204);
            lblCharacterSpacing.Name = "lblCharacterSpacing";
            lblCharacterSpacing.Size = new System.Drawing.Size(97, 20);
            lblCharacterSpacing.TabIndex = 0;
            lblCharacterSpacing.Text = "Char spacing:";
            // 
            // lblPageCount
            // 
            lblPageCount.AutoSize = true;
            lblPageCount.Location = new System.Drawing.Point(10, 132);
            lblPageCount.Name = "lblPageCount";
            lblPageCount.Size = new System.Drawing.Size(85, 20);
            lblPageCount.TabIndex = 0;
            lblPageCount.Text = "Page count:";
            // 
            // lblPadding
            // 
            lblPadding.AutoSize = true;
            lblPadding.Location = new System.Drawing.Point(10, 167);
            lblPadding.Name = "lblPadding";
            lblPadding.Size = new System.Drawing.Size(66, 20);
            lblPadding.TabIndex = 0;
            lblPadding.Text = "Padding:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(10, 65);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(71, 20);
            label2.TabIndex = 0;
            label2.Text = "First char:";
            // 
            // lblFallbackCharacter
            // 
            lblFallbackCharacter.AutoSize = true;
            lblFallbackCharacter.Location = new System.Drawing.Point(10, 98);
            lblFallbackCharacter.Name = "lblFallbackCharacter";
            lblFallbackCharacter.Size = new System.Drawing.Size(97, 20);
            lblFallbackCharacter.TabIndex = 0;
            lblFallbackCharacter.Text = "Fallback char:";
            // 
            // udBaseSize
            // 
            udBaseSize.Location = new System.Drawing.Point(106, 31);
            udBaseSize.Name = "udBaseSize";
            udBaseSize.Size = new System.Drawing.Size(49, 27);
            udBaseSize.TabIndex = 3;
            udBaseSize.ValueChanged += OnBaseSizeValueChanged;
            // 
            // lblBaseSize
            // 
            lblBaseSize.AutoSize = true;
            lblBaseSize.Location = new System.Drawing.Point(10, 33);
            lblBaseSize.Name = "lblBaseSize";
            lblBaseSize.Size = new System.Drawing.Size(74, 20);
            lblBaseSize.TabIndex = 0;
            lblBaseSize.Text = "Base Size:";
            // 
            // gbExportSettings
            // 
            gbExportSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            gbExportSettings.Controls.Add(btnExport);
            gbExportSettings.Controls.Add(btnExportDirectoryPath);
            gbExportSettings.Controls.Add(txtExportDirectoryPath);
            gbExportSettings.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            gbExportSettings.Location = new System.Drawing.Point(12, 629);
            gbExportSettings.Margin = new System.Windows.Forms.Padding(4);
            gbExportSettings.Name = "gbExportSettings";
            gbExportSettings.Padding = new System.Windows.Forms.Padding(4);
            gbExportSettings.Size = new System.Drawing.Size(324, 118);
            gbExportSettings.TabIndex = 1;
            gbExportSettings.TabStop = false;
            gbExportSettings.Text = "Export settings";
            // 
            // btnExport
            // 
            btnExport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnExport.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            btnExport.Location = new System.Drawing.Point(10, 69);
            btnExport.Name = "btnExport";
            btnExport.Size = new System.Drawing.Size(307, 32);
            btnExport.TabIndex = 15;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.MouseClick += OnExportClick;
            // 
            // btnExportDirectoryPath
            // 
            btnExportDirectoryPath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnExportDirectoryPath.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            btnExportDirectoryPath.Location = new System.Drawing.Point(282, 27);
            btnExportDirectoryPath.Name = "btnExportDirectoryPath";
            btnExportDirectoryPath.Size = new System.Drawing.Size(35, 27);
            btnExportDirectoryPath.TabIndex = 14;
            btnExportDirectoryPath.Text = "...";
            btnExportDirectoryPath.UseVisualStyleBackColor = true;
            btnExportDirectoryPath.MouseClick += OnExportDirectoryPathClick;
            // 
            // txtExportDirectoryPath
            // 
            txtExportDirectoryPath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtExportDirectoryPath.Location = new System.Drawing.Point(10, 27);
            txtExportDirectoryPath.Name = "txtExportDirectoryPath";
            txtExportDirectoryPath.ReadOnly = true;
            txtExportDirectoryPath.Size = new System.Drawing.Size(270, 27);
            txtExportDirectoryPath.TabIndex = 13;
            txtExportDirectoryPath.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.SystemColors.ControlDark;
            ClientSize = new System.Drawing.Size(1419, 761);
            Controls.Add(gbExportSettings);
            Controls.Add(gbFontGenerationSettings);
            Controls.Add(gbPreview);
            Controls.Add(gbFontStyleSettings);
            Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ForeColor = System.Drawing.SystemColors.ControlText;
            MaximizeBox = false;
            MaximumSize = new System.Drawing.Size(1435, 2000);
            MinimumSize = new System.Drawing.Size(1435, 600);
            Name = "MainForm";
            ShowIcon = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Quasar Font Generator Tool";
            gbFontStyleSettings.ResumeLayout(false);
            gbFontStyleSettings.PerformLayout();
            gbPreview.ResumeLayout(false);
            gbFontGenerationSettings.ResumeLayout(false);
            gbFontGenerationSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)udHorizontalScale).EndInit();
            ((System.ComponentModel.ISupportInitialize)udCharacterSpacing).EndInit();
            ((System.ComponentModel.ISupportInitialize)udPadding).EndInit();
            ((System.ComponentModel.ISupportInitialize)udPageCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)udBaseSize).EndInit();
            gbExportSettings.ResumeLayout(false);
            gbExportSettings.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbFontStyleSettings;
        private System.Windows.Forms.GroupBox gbPreview;
        private System.Windows.Forms.GroupBox gbFontGenerationSettings;
        private System.Windows.Forms.GroupBox gbExportSettings;
        private System.Windows.Forms.VScrollBar sbVertical;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.CheckedListBox clbFontStyles;
        private System.Windows.Forms.Label lblFontFamily;
        private System.Windows.Forms.ComboBox cbFontFamilies;
        private System.Windows.Forms.TextBox txtPreview;
        private System.Windows.Forms.NumericUpDown udBaseSize;
        private System.Windows.Forms.Label lblBaseSize;
        private System.Windows.Forms.Label lblHorizontalScale;
        private System.Windows.Forms.Label lblCharacterSpacing;
        private System.Windows.Forms.Label lblPageCount;
        private System.Windows.Forms.Label lblPadding;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFallbackCharacter;
        private System.Windows.Forms.TextBox txtFirstCharacter;
        private System.Windows.Forms.TextBox txtFallbackCharacter;
        private System.Windows.Forms.NumericUpDown udPageCount;
        private System.Windows.Forms.NumericUpDown udPadding;
        private System.Windows.Forms.NumericUpDown udCharacterSpacing;
        private System.Windows.Forms.NumericUpDown udHorizontalScale;
        private System.Windows.Forms.Label lblFontNameOverride;
        private System.Windows.Forms.TextBox txtFontFamilyNameOverride;
        private System.Windows.Forms.Button btnExportDirectoryPath;
        private System.Windows.Forms.TextBox txtExportDirectoryPath;
        private System.Windows.Forms.Button btnExport;
    }
}