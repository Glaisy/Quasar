//-----------------------------------------------------------------------
// <copyright file="IUIContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using Quasar.UI.VisualElements;

using Space.Core.Collections;

namespace Quasar.UI
{
    /// <summary>
    /// Represents the context object of Quasar's UI system.
    /// </summary>
    public interface IUIContext
    {
        /// <summary>
        /// Gets the visual element list pool.
        /// </summary>
        IPool<List<VisualElement>> VisualElementListPool { get; }


        /// <summary>
        /// Validates the execution context.
        /// </summary>
        void Validate();
    }
}
