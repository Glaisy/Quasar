//-----------------------------------------------------------------------
// <copyright file="VisualElement.Events.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;
using Quasar.Rendering;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Represents a basic UI visual element - Events.
    /// </summary>
    public partial class VisualElement
    {
        /// <summary>
        /// The render event handler.
        /// </summary>
        /// <param name="context">The context.</param>
        protected virtual void OnRender(IRenderingContext context)
        {
            ////if (Visibility != Visibility.Visible ||
            ////    Display != DisplayStyle.Display)
            ////{
            ////    return;
            ////}

            var sprite = new Sprite(textureRepository.FallbackTexture);
            canvas.DrawSprite(sprite, new Vector2(100, 500), new Vector2(320, 180), Color.Yellow);
            canvas.Render(context);

            ////// propagate render event through the hierarchy
            ////foreach (var child in children)
            ////{
            ////    child.ProcessRenderEvent();
            ////}
        }
    }
}
