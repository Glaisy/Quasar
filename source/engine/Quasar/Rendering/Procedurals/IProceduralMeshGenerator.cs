//-----------------------------------------------------------------------
// <copyright file="IProceduralMeshGenerator.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

namespace Quasar.Rendering.Procedurals
{
    /// <summary>
    /// Procedural mesh generatro interface definition.
    /// </summary>
    public interface IProceduralMeshGenerator
    {
        /// <summary>
        /// Generates a cube mesh data.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="size">The size.</param>
        void GenerateCube(ref IMesh mesh, in Vector3 size);

        /// <summary>
        /// Generates ellipsoid mesh with panoramic UVs into the mesh by the specified radiuses.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="longitudes">The longitudes.</param>
        /// <param name="latitudes">The latitudes.</param>
        /// <param name="radiuses">The radiuses.</param>
        void GenerateEllipsoid(ref IMesh mesh, int longitudes, int latitudes, in Vector3 radiuses);

        /// <summary>
        /// Generates ellipsoid mesh with panoramic UVs into the mesh by the specified radius provider.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="longitudes">The longitudes.</param>
        /// <param name="latitudes">The latitudes.</param>
        /// <param name="radiusProvider">The radius provider.</param>
        void GenerateEllipsoid(ref IMesh mesh, int longitudes, int latitudes, IRadiusProvider radiusProvider);

        /// <summary>
        /// Creates a plane mesh.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="size">The size.</param>
        /// <param name="subdivisions">The number of subdivision per sides.</param>
        void GeneratePlane(ref IMesh mesh, in Vector2 size, int subdivisions);

        /// <summary>
        /// Creates a skybox mesh.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        void GenerateSkybox(ref IMesh mesh);
    }
}
