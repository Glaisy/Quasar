//-----------------------------------------------------------------------
// <copyright file="ButtonBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Quasar.Inputs;
using Quasar.UI.VisualElements.Styles;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Abstract base class for button visual elements.
    /// </summary>
    /// <seealso cref="VisualElement" />
    public abstract class ButtonBase : VisualElement
    {
        private bool leftButtonIsDown;


        /// <summary>
        /// Gets or sets the click event handler.
        /// </summary>
        public Action<VisualElement> Click { get; set; }


        /// <inheritdoc/>
        protected override PseudoClass GetPseudoClass()
        {
            if (IsEnabled)
            {
                if (PointerOver)
                {
                    return leftButtonIsDown ? PseudoClass.Active : PseudoClass.Hover;
                }

                return PseudoClass.Default;
            }

            return PseudoClass.Disabled;
        }

        /// <inheritdoc/>
        protected override void OnPointerButtonDown(in PointerButtonEventArgs args)
        {
            if (args.Button != PointerButton.Left)
            {
                return;
            }

            leftButtonIsDown = true;
            Invalidate(InvalidationFlags.PseudoClass);
        }

        /// <inheritdoc/>
        protected override void OnPointerButtonUp(in PointerButtonEventArgs args)
        {
            if (args.Button != PointerButton.Left)
            {
                return;
            }

            leftButtonIsDown = false;
            Invalidate(InvalidationFlags.PseudoClass);
        }


        /// <inheritdoc/>
        protected override void OnPointerClick(in PointerButtonEventArgs args)
        {
            if (args.Button != PointerButton.Left || Click == null)
            {
                return;
            }

            Click(this);
        }

        /// <inheritdoc/>
        protected override void OnPointerLeave()
        {
            leftButtonIsDown = false;
        }
    }
}
