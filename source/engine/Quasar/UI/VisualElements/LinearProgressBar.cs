//-----------------------------------------------------------------------
// <copyright file="LinearProgressBar.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Basic linear progress bar implementation.
    /// </summary>
    /// <seealso cref="ProgressBarBase" />
    public class LinearProgressBar : ProgressBarBase
    {
        /// <summary>
        /// Custom class selector constants.
        /// </summary>
        public static class ClassSelectors
        {
            /// <summary>
            /// The indicator's class selector.
            /// </summary>
            public static readonly string Indicator = TagSelectors.LinearProgressBar + "-indicator";
        }


        private readonly VisualElement indicator;


        /// <summary>
        /// Initializes a new instance of the <see cref="LinearProgressBar"/> class.
        /// </summary>
        public LinearProgressBar()
        {
            LayoutType = Layouts.LayoutType.Grid;
            AddColumn(0.0f, Layouts.GridLengthType.Star);
            AddColumn(1.0f, Layouts.GridLengthType.Star);
            indicator = new VisualElement();
            indicator.AddToClassList(ClassSelectors.Indicator);
            Add(indicator);
        }


        /// <inheritdoc/>
        protected override string TagSelector => TagSelectors.LinearProgressBar;


        /// <inheritdoc/>
        protected override void UpdateProgressIndicator(float value)
        {
            SetColumn(0, value, Layouts.GridLengthType.Star);
            SetColumn(1, 1.0f - value, Layouts.GridLengthType.Star);
        }
    }
}
