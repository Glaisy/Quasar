//-----------------------------------------------------------------------
// <copyright file="CameraService.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Internals.Services
{
    /// <summary>
    /// Camera service and provider implementation.
    /// </summary>
    [Export]
    [Singleton]
    internal sealed class CameraService
    {
        private readonly List<ICamera> activeCameras = new List<ICamera>();


        /// <summary>
        /// Activates the camera.
        /// </summary>
        /// <param name="camera">The camera.</param>
        public void Activate(ICamera camera)
        {
            activeCameras.Add(camera);
        }

        /// <summary>
        /// Deactivates all active cameras.
        /// </summary>
        public void Clear()
        {
            activeCameras.Clear();
        }

        /// <summary>
        /// Deactivates the camera.
        /// </summary>
        /// <param name="camera">The camera.</param>
        public void Deactive(ICamera camera)
        {
            activeCameras.Remove(camera);
        }

        /// <summary>
        /// Gets the camera enumerator.
        /// </summary>
        public List<ICamera>.Enumerator GetEnumerator()
        {
            return activeCameras.GetEnumerator();
        }
    }
}
