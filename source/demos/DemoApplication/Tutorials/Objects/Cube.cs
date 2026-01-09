//-----------------------------------------------------------------------
// <copyright file="Cube.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar;
using Quasar.Graphics;
using Quasar.Rendering;
using Quasar.Rendering.Procedurals;

namespace DemoApplication.Tutorials.Objects
{
    /// <summary>
    /// Cube test object.
    /// </summary>
    /// <seealso cref="RenderModel" />
    internal class Cube : RenderModel
    {
        private Vector3 rotationSpeed;


        /// <summary>
        /// Creates the specified position.
        /// </summary>
        /// <param name="proceduralMeshGenerator">The procedural mesh generator.</param>
        /// <param name="position">The position.</param>
        /// <param name="size">The size.</param>
        /// <param name="rotationSpeed">The rotation speed.</param>
        /// <param name="material">The material.</param>
        /// <returns>
        /// The cube instance.
        /// </returns>
        public static Cube Create(
            IProceduralMeshGenerator proceduralMeshGenerator,
            in Vector3 position,
            in Vector3 size,
            in Vector3 rotationSpeed,
            Material material)
        {
            IMesh mesh = null;
            proceduralMeshGenerator.GenerateCube(ref mesh, size);

            var cube = new Cube
            {
                Material = material,
                rotationSpeed = rotationSpeed
            };
            cube.SetMesh(mesh, false);


            cube.Transform.LocalPosition = position;

            return cube;
        }

        /// <summary>
        /// Updates the specified delta time.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public void Update(float deltaTime)
        {
            var rotation = Quaternion.AngleAxis(rotationSpeed.X * deltaTime, Vector3.PositiveX) *
                Quaternion.AngleAxis(rotationSpeed.Y * deltaTime, Vector3.PositiveY) *
                Quaternion.AngleAxis(rotationSpeed.Z * deltaTime, Vector3.PositiveZ);

            Transform.LocalRotation *= rotation;
        }
    }
}
