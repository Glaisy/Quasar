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

using System.Runtime.CompilerServices;

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
        /// Executes the render event processing.
        /// </summary>
        /// <param name="context">The context.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessRenderEvent(IRenderingContext context)
        {
            ////if (Visibility != Visibility.Visible ||
            ////    Display != DisplayStyle.Display)
            ////{
            ////    return;
            ////}

            // invoke internal render event handler
            var sprite = new Sprite(textureRepository.FallbackTexture);
            canvas.DrawSprite(sprite, new Vector2(100, 500), new Vector2(320, 180), Color.Yellow);

            ////Canvas.Offset = CanvasPosition;
            ////OnRender(Canvas);
            canvas.Render(context);

            ////// propagate render event through the hierarchy
            ////foreach (var child in children)
            ////{
            ////    child.ProcessRenderEvent();
            ////}
        }
    }
}
