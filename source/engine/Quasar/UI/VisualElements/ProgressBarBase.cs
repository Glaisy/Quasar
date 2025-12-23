//-----------------------------------------------------------------------
// <copyright file="ProgressBarBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Abstract base class for progress bar implementations.
    /// </summary>
    /// <seealso cref="VisualElement" />
    public abstract class ProgressBarBase : VisualElement
    {
        private float value = 0.0f;
        /// <summary>
        /// Gets or sets the progress value [0...1].
        /// </summary>
        public float Value
        {
            get => value;
            set
            {
                value = Ranges.FloatUnit.Clamp(value);
                if (this.value == value)
                {
                    return;
                }

                this.value = value;
                UpdateProgressIndicator(value);
            }
        }


        /// <inheritdoc/>
        protected override bool IsFocusable => false;


        /// <summary>
        /// Updates the progress indicator by the progress value [0...1].
        /// </summary>
        /// <param name="value">The value.</param>
        protected abstract void UpdateProgressIndicator(float value);
    }
}
