//-----------------------------------------------------------------------
// <copyright file="TutorialTitlePanel.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.UI.Templates;
using Quasar.UI.VisualElements;

namespace DemoApplication.Tutorials
{
    /// <summary>
    /// Demo application's title panel for tutorials.
    /// </summary>
    /// <seealso cref="VisualElement" />
    [UITemplate("TitorialTitlePanel")]
    internal sealed class TutorialTitlePanel : VisualElement
    {
        /// <summary>
        /// Gets the back button.
        /// </summary>
        public Button BackButton { get; private set; }

        /// <summary>
        /// Gets the title label.
        /// </summary>
        public Label TitleLabel { get; private set; }


        /// <inheritdoc/>
        protected override void OnLoad()
        {
            BackButton = Q<Button>("backButton");
            TitleLabel = Q<Label>("titleLabel");
        }
    }
}
