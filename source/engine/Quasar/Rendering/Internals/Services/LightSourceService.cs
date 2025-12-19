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

////using System;
////using System.Collections.Generic;

////using Quasar.Core.Composition;

////namespace Quasar.Rendering.Internals.Services
////{
////    /// <summary>
////    /// Light source service implementation.
////    /// </summary>
////    /// <seealso cref="ILightSourceProvider" />
////    [Export(typeof(ILightSourceProvider))]
////    [Export]
////    [Shared]
////    internal class LightSourceService : ILightSourceProvider
////    {
////        private readonly List<ILightSource> activeLightSources = new List<ILightSource>();


////        /// <summary>
////        /// Gets the light source by the specified index.
////        /// </summary>
////        /// <param name="index">The index.</param>
////        ILightSource ILightSourceProvider.this[int index] => activeLightSources[index];

////        /// <summary>
////        /// Gets the <see cref="ILightSource"/> with the specified name.
////        /// </summary>
////        /// <value>
////        /// The <see cref="ILightSource"/>.
////        /// </value>
////        /// <param name="name">The name.</param>
////        ILightSource ILightSourceProvider.this[string name]
////        {
////            get
////            {
////                ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

////                foreach (var lightSource in activeLightSources)
////                {
////                    if (lightSource.Name == name)
////                    {
////                        return lightSource;
////                    }
////                }

////                return null;
////            }
////        }


////        /// <summary>
////        /// Gets tne number of active light sources.
////        /// </summary>
////        int ILightSourceProvider.Count => activeLightSources.Count;

////        private ILightSource mainLightSource;
////        /// <summary>
////        /// Gets the maing light source.
////        /// </summary>
////        ILightSource ILightSourceProvider.MainLigntSource => mainLightSource;


////        /// <summary>
////        /// Gets the enumerator for the active light sources.
////        /// </summary>
////        List<ILightSource>.Enumerator ILightSourceProvider.GetEnumerator()
////        {
////            return activeLightSources.GetEnumerator();
////        }


////        /// <summary>
////        /// Adds the light source.
////        /// </summary>
////        /// <param name="lightSource">The light source.</param>
////        public void Add(ILightSource lightSource)
////        {
////            activeLightSources.Add(lightSource);
////            mainLightSource ??= lightSource;
////        }

////        /// <summary>
////        /// Removes all active light sources.
////        /// </summary>
////        public void Clear()
////        {
////            activeLightSources.Clear();
////            mainLightSource = null;
////        }

////        /// <summary>
////        /// Remove the light source.
////        /// </summary>
////        /// <param name="lightSource">The light source.</param>
////        public void Remove(ILightSource lightSource)
////        {
////            activeLightSources.Remove(lightSource);
////            if (mainLightSource == lightSource)
////            {
////                mainLightSource = activeLightSources.Count > 0 ? activeLightSources[0] : null;
////            }
////        }
////    }
////}
