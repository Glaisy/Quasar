//-----------------------------------------------------------------------
// <copyright file="IUIEventProcessor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Rendering;

namespace Quasar.UI.Internals
{
    /// <summary>
    /// Represent a UI component to process general events.
    /// </summary>
    internal interface IUIEventProcessor
    {
        /// <summary>
        /// Processes the event when UI rendering happens.
        /// </summary>
        /// <param name="context">The context.</param>
        void ProcessRenderEvent(IRenderingContext context);

        /// <summary>
        /// Processes the event when UI update happens.
        /// </summary>
        void ProcessUpdateEvent();
    }
}
