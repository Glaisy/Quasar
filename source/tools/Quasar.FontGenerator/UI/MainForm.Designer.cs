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
            gbPreview = new System.Windows.Forms.GroupBox();
            sbVertical = new System.Windows.Forms.VScrollBar();
            pnlPreview = new System.Windows.Forms.Panel();
            gbFontGenerationSettings = new System.Windows.Forms.GroupBox();
            gbExportSettings = new System.Windows.Forms.GroupBox();
            gbPreview.SuspendLayout();
            SuspendLayout();
            // 
            // gbFontStyleSettings
            // 
            gbFontStyleSettings.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gbFontStyleSettings.Location = new System.Drawing.Point(12, 4);
            gbFontStyleSettings.Margin = new System.Windows.Forms.Padding(4);
            gbFontStyleSettings.Name = "gbFontStyleSettings";
            gbFontStyleSettings.Padding = new System.Windows.Forms.Padding(4);
            gbFontStyleSettings.Size = new System.Drawing.Size(324, 179);
            gbFontStyleSettings.TabIndex = 0;
            gbFontStyleSettings.TabStop = false;
            gbFontStyleSettings.Text = "Font style settings";
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
            pnlPreview.Paint += PaintPreviewPanel;
            // 
            // gbFontGenerationSettings
            // 
            gbFontGenerationSettings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            gbFontGenerationSettings.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            gbFontGenerationSettings.Location = new System.Drawing.Point(12, 191);
            gbFontGenerationSettings.Margin = new System.Windows.Forms.Padding(4);
            gbFontGenerationSettings.Name = "gbFontGenerationSettings";
            gbFontGenerationSettings.Padding = new System.Windows.Forms.Padding(4);
            gbFontGenerationSettings.Size = new System.Drawing.Size(324, 390);
            gbFontGenerationSettings.TabIndex = 2;
            gbFontGenerationSettings.TabStop = false;
            gbFontGenerationSettings.Text = "Font generation settings";
            // 
            // gbExportSettings
            // 
            gbExportSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            gbExportSettings.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            gbExportSettings.Location = new System.Drawing.Point(12, 589);
            gbExportSettings.Margin = new System.Windows.Forms.Padding(4);
            gbExportSettings.Name = "gbExportSettings";
            gbExportSettings.Padding = new System.Windows.Forms.Padding(4);
            gbExportSettings.Size = new System.Drawing.Size(324, 158);
            gbExportSettings.TabIndex = 1;
            gbExportSettings.TabStop = false;
            gbExportSettings.Text = "Export settings";
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
            gbPreview.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbFontStyleSettings;
        private System.Windows.Forms.GroupBox gbPreview;
        private System.Windows.Forms.GroupBox gbFontGenerationSettings;
        private System.Windows.Forms.GroupBox gbExportSettings;
        private System.Windows.Forms.VScrollBar sbVertical;
        private System.Windows.Forms.Panel pnlPreview;
    }
}