//-----------------------------------------------------------------------
// <copyright file="PreviewPanel.cs" company="Space Development">
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
using System.Windows.Forms;

namespace Quasar.FontGenerator.UI
{
    /// <summary>
    /// Custom panel to display font preview image.
    /// </summary>
    /// <seealso cref="Panel" />
    public sealed class PreviewPanel : Panel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewPanel"/> class.
        /// </summary>
        public PreviewPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
        }


        private Bitmap previewBitmap;
        /// <summary>
        /// Gets or sets the preview bitmap.
        /// </summary>
        public Bitmap PreviewBitmap
        {
            get => previewBitmap;
            set
            {
                if (previewBitmap == value)
                {
                    return;
                }

                previewBitmap?.Dispose();
                previewBitmap = value;
                Invalidate();
            }
        }

        private int offset;
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public int Offset
        {
            get => offset;
            set
            {
                if (offset == value)
                {
                    return;
                }

                offset = value;
                Invalidate();
            }
        }

        /// <inheritdoc/>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (previewBitmap == null)
            {
                return;
            }

            var height = Math.Min(ClientRectangle.Height, previewBitmap.Height);
            var width = Math.Min(ClientRectangle.Width, previewBitmap.Width);
            var size = new Size(width, height);
            var srcRect = new Rectangle(new Point(0, offset), size);
            var destRect = new Rectangle(Point.Empty, size);
            e.Graphics.DrawImage(previewBitmap, destRect, srcRect, GraphicsUnit.Pixel);
        }
    }
}
