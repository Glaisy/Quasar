//-----------------------------------------------------------------------
// <copyright file="ProceduralMeshGenerator.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Graphics;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Procedurals.Internals
{
    /// <summary>
    /// Procedural mesh generator implementation.
    /// </summary>
    /// <seealso cref="IProceduralMeshGenerator" />
    [Export(typeof(IProceduralMeshGenerator))]
    [Singleton]
    internal sealed class ProceduralMeshGenerator : IProceduralMeshGenerator
    {
        private readonly PlaneMeshGenerator planeMeshGenerator;
        private readonly CubeMeshGenerator cubeMeshGenerator;
        private readonly EllipsoidMeshGenerator ellipsoidMeshGenerator;


        /// <summary>
        /// Initializes a new instance of the <see cref="ProceduralMeshGenerator" /> class.
        /// </summary>
        /// <param name="planeMeshGenerator">The plane mesh generator.</param>
        /// <param name="cubeMeshGenerator">The cube mesh generator.</param>
        /// <param name="ellipsoidMeshGenerator">The ellipsoid mesh generator.</param>
        public ProceduralMeshGenerator(
            PlaneMeshGenerator planeMeshGenerator,
            CubeMeshGenerator cubeMeshGenerator,
            EllipsoidMeshGenerator ellipsoidMeshGenerator)
        {
            this.planeMeshGenerator = planeMeshGenerator;
            this.cubeMeshGenerator = cubeMeshGenerator;
            this.ellipsoidMeshGenerator = ellipsoidMeshGenerator;
        }


        /// <summary>
        /// Generates a cube mesh data.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="size">The size.</param>
        public void GenerateCube(ref IMesh mesh, in Vector3 size)
        {
            cubeMeshGenerator.Generate(ref mesh, size);
        }

        /// <summary>
        /// Creates a plane mesh.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="size">The size.</param>
        /// <param name="subdivisions">The number of subdivision per sides.</param>
        public void GeneratePlane(ref IMesh mesh, in Vector2 size, int subdivisions)
        {
            planeMeshGenerator.Generate(ref mesh, size, subdivisions);
        }

        /// <summary>
        /// Creates a skybox mesh.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        public void GenerateSkybox(ref IMesh mesh)
        {
            cubeMeshGenerator.GenerateSkybox(ref mesh);
        }

        /// <summary>
        /// Generates ellipsoid mesh with panoramic UVs into the mesh by the specified radiuses.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="longitudes">The longitudes.</param>
        /// <param name="latitudes">The latitudes.</param>
        /// <param name="radiuses">The radiuses.</param>
        public void GenerateEllipsoid(ref IMesh mesh, int longitudes, int latitudes, in Vector3 radiuses)
        {
            ellipsoidMeshGenerator.Generate(ref mesh, longitudes, latitudes, radiuses);
        }

        /// <summary>
        /// Generates ellipsoid mesh with panoramic UVs into the mesh by the specified radius provider.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <param name="longitudes">The longitudes.</param>
        /// <param name="latitudes">The latitudes.</param>
        /// <param name="radiusProvider">The radius provider.</param>
        public void GenerateEllipsoid(ref IMesh mesh, int longitudes, int latitudes, IRadiusProvider radiusProvider)
        {
            ellipsoidMeshGenerator.Generate(ref mesh, longitudes, latitudes, radiusProvider);
        }
    }
}
