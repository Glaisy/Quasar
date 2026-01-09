//-----------------------------------------------------------------------
// <copyright file="LightSourceService.cs" company="Space Development">
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
    /// Light source service implementation.
    /// </summary>
    [Export]
    [Singleton]
    internal class LightSourceService
    {
        private readonly List<ILightSource> activeLightSources = new List<ILightSource>();


        /// <summary>
        /// Gets the <see cref="ILightSource" /> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public ILightSource this[int index] => activeLightSources[index];


        /// <summary>
        /// Gets tne number of active light sources.
        /// </summary>
        public int Count => activeLightSources.Count;


        /// <summary>
        /// Gets the enumerator for the active light sources.
        /// </summary>
        public List<ILightSource>.Enumerator GetEnumerator()
        {
            return activeLightSources.GetEnumerator();
        }


        /// <summary>
        /// Adds the light source.
        /// </summary>
        /// <param name="lightSource">The light source.</param>
        public void Add(ILightSource lightSource)
        {
            activeLightSources.Add(lightSource);
        }

        /// <summary>
        /// Removes all active light sources.
        /// </summary>
        public void Clear()
        {
            activeLightSources.Clear();
        }

        /// <summary>
        /// Remove the light source.
        /// </summary>
        /// <param name="lightSource">The light source.</param>
        public void Remove(ILightSource lightSource)
        {
            activeLightSources.Remove(lightSource);
        }
    }
}
