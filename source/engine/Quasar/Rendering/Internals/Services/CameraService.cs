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

using System;
using System.Collections.Generic;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Internals.Services
{
    /// <summary>
    /// Camera service and provider implementation.
    /// </summary>
    /// <seealso cref="ICameraProvider" />
    [Export(typeof(ICameraProvider))]
    [Export]
    [Singleton]
    internal class CameraService : ICameraProvider
    {
        private readonly List<ICamera> activeCameras = new List<ICamera>();


        /// <inheritdoc/>
        ICamera ICameraProvider.this[int index] => activeCameras[index];

        /// <inheritdoc/>
        ICamera ICameraProvider.this[string name]
        {
            get
            {
                ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
                return activeCameras.Find(camera => camera.Name == name);
            }
        }

        /// <inheritdoc/>
        int ICameraProvider.Count => activeCameras.Count;

        private ICamera mainCamera;
        /// <inheritdoc/>
        ICamera ICameraProvider.MainCamera => mainCamera;


        /// <inheritdoc/>
        List<ICamera>.Enumerator ICameraProvider.GetEnumerator()
        {
            return activeCameras.GetEnumerator();
        }



        /// <summary>
        /// Activates the camera.
        /// </summary>
        /// <param name="camera">The camera.</param>
        public void Activate(ICamera camera)
        {
            activeCameras.Add(camera);
            mainCamera ??= camera;
        }

        /// <summary>
        /// Deactivates all active cameras.
        /// </summary>
        public void Clear()
        {
            activeCameras.Clear();
            mainCamera = null;
        }

        /// <summary>
        /// Deactivates the camera.
        /// </summary>
        /// <param name="camera">The camera.</param>
        public void Deactive(ICamera camera)
        {
            activeCameras.Remove(camera);

            if (mainCamera == camera)
            {
                mainCamera = activeCameras.Count > 0 ? activeCameras[0] : null;
            }
        }
    }
}
