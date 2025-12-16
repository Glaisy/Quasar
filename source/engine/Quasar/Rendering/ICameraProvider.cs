//-----------------------------------------------------------------------
// <copyright file="ICameraProvider.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Quasar.Rendering
{
    /// <summary>
    /// Represents a provider component to access active cameras.
    /// </summary>
    public interface ICameraProvider
    {
        /// <summary>
        /// Gets the camera by the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        ICamera this[int index] { get; }

        /// <summary>
        /// Gets the camera by the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        ICamera this[string name] { get; }


        /// <summary>
        /// Gets tne number of active cameras.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the main camera (index = 0).
        /// </summary>
        ICamera MainCamera { get; }


        /// <summary>
        /// Gets the enumerator for the active cameras.
        /// </summary>
        List<ICamera>.Enumerator GetEnumerator();
    }
}
