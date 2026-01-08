//-----------------------------------------------------------------------
// <copyright file="TemplatedVisualElementBase.cs" company="Space Development">
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
    /// Abstract base class for templated visual elements.
    /// </summary>
    /// <seealso cref="VisualElement" />
    public abstract class TemplatedVisualElementBase : VisualElement
    {
        /// <summary>
        /// Executes the template loaded event processing.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ProcessTemplateLoadedEvent()
        {
            OnTemplateLoaded();
        }


        /// <summary>
        /// The template loaded event handler.
        /// </summary>
        protected virtual void OnTemplateLoaded()
        {
        }
    }
}
