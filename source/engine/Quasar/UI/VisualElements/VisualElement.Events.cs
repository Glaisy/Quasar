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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessRenderEvent()
        {
            ////if (Visibility != Visibility.Visible ||
            ////    Display != DisplayStyle.Display)
            ////{
            ////    return;
            ////}

            // invoke internal render event handler
            ////Canvas.Offset = CanvasPosition;
            ////OnRender(Canvas);

            ////// propagate render event through the hierarchy
            ////foreach (var child in children)
            ////{
            ////    child.ProcessRenderEvent();
            ////}
        }
    }
}
