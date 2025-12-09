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
            sbHorizontal = new System.Windows.Forms.HScrollBar();
            sbVertical = new System.Windows.Forms.VScrollBar();
            pnlPreview = new System.Windows.Forms.Panel();
            gbFontGenerationSettings = new System.Windows.Forms.GroupBox();
            gbExportSettings = new System.Windows.Forms.GroupBox();
            gbPreview.SuspendLayout();
            SuspendLayout();
            // 
            // gbFontStyleSettings
            // 
            gbFontStyleSettings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            gbFontStyleSettings.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gbFontStyleSettings.Location = new System.Drawing.Point(12, 4);
            gbFontStyleSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            gbFontStyleSettings.Name = "gbFontStyleSettings";
            gbFontStyleSettings.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            gbFontStyleSettings.Size = new System.Drawing.Size(324, 159);
            gbFontStyleSettings.TabIndex = 0;
            gbFontStyleSettings.TabStop = false;
            gbFontStyleSettings.Text = "Font style settings";
            // 
            // gbPreview
            // 
            gbPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            gbPreview.Controls.Add(sbHorizontal);
            gbPreview.Controls.Add(sbVertical);
            gbPreview.Controls.Add(pnlPreview);
            gbPreview.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            gbPreview.Location = new System.Drawing.Point(352, 4);
            gbPreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            gbPreview.Name = "gbPreview";
            gbPreview.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            gbPreview.Size = new System.Drawing.Size(900, 724);
            gbPreview.TabIndex = 1;
            gbPreview.TabStop = false;
            gbPreview.Text = "Preview";
            // 
            // sbHorizontal
            // 
            sbHorizontal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            sbHorizontal.Location = new System.Drawing.Point(7, 703);
            sbHorizontal.Name = "sbHorizontal";
            sbHorizontal.Size = new System.Drawing.Size(869, 17);
            sbHorizontal.TabIndex = 2;
            // 
            // sbVertical
            // 
            sbVertical.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            sbVertical.Location = new System.Drawing.Point(879, 25);
            sbVertical.Name = "sbVertical";
            sbVertical.Size = new System.Drawing.Size(17, 675);
            sbVertical.TabIndex = 1;
            // 
            // pnlPreview
            // 
            pnlPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlPreview.BackColor = System.Drawing.SystemColors.Control;
            pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlPreview.Location = new System.Drawing.Point(7, 25);
            pnlPreview.Name = "pnlPreview";
            pnlPreview.Size = new System.Drawing.Size(869, 675);
            pnlPreview.TabIndex = 0;
            // 
            // gbFontGenerationSettings
            // 
            gbFontGenerationSettings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            gbFontGenerationSettings.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            gbFontGenerationSettings.Location = new System.Drawing.Point(12, 202);
            gbFontGenerationSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            gbFontGenerationSettings.Name = "gbFontGenerationSettings";
            gbFontGenerationSettings.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            gbFontGenerationSettings.Size = new System.Drawing.Size(324, 359);
            gbFontGenerationSettings.TabIndex = 2;
            gbFontGenerationSettings.TabStop = false;
            gbFontGenerationSettings.Text = "Font generation settings";
            // 
            // gbExportSettings
            // 
            gbExportSettings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            gbExportSettings.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            gbExportSettings.Location = new System.Drawing.Point(12, 600);
            gbExportSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            gbExportSettings.Name = "gbExportSettings";
            gbExportSettings.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            gbExportSettings.Size = new System.Drawing.Size(324, 127);
            gbExportSettings.TabIndex = 1;
            gbExportSettings.TabStop = false;
            gbExportSettings.Text = "Export settings";
            // 
            // MainForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.SystemColors.ControlDark;
            ClientSize = new System.Drawing.Size(1264, 741);
            Controls.Add(gbExportSettings);
            Controls.Add(gbFontGenerationSettings);
            Controls.Add(gbPreview);
            Controls.Add(gbFontStyleSettings);
            Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ForeColor = System.Drawing.SystemColors.ControlText;
            Name = "MainForm";
            ShowIcon = false;
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
        private System.Windows.Forms.HScrollBar sbHorizontal;
        private System.Windows.Forms.VScrollBar sbVertical;
        private System.Windows.Forms.Panel pnlPreview;
    }
}