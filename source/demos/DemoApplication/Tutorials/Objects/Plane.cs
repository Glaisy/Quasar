//-----------------------------------------------------------------------
// <copyright file="Plane.cs" company="Space Development">
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
    /// Plane test object.
    /// </summary>
    /// <seealso cref="RenderModel" />
    internal sealed class Plane : RenderModel
    {
        /// <summary>
        /// Creates a plane object by the specified parameters.
        /// </summary>
        /// <param name="proceduralMeshGenerator">The procedural mesh generator.</param>
        /// <param name="position">The position.</param>
        /// <param name="normal">The normal.</param>
        /// <param name="size">The size.</param>
        /// <param name="color">The color.</param>
        /// <param name="subdivisions">The subdivisions.</param>
        /// <returns>
        /// The plane instance.
        /// </returns>
        public static Plane Create(
            IProceduralMeshGenerator proceduralMeshGenerator,
            in Vector3 position,
            Vector3 normal,
            Vector2 size,
            Color color,
            int subdivisions = 8)
        {
            IMesh mesh = null;
            proceduralMeshGenerator.GeneratePlane(ref mesh, size, subdivisions);
            var material = new Material("Wireframe");
            material.SetColor("FillColor", color);
            material.SetColor("LineColor", Color.Black);
            material.SetFloat("Thickness", 1.5f);
            ////var material = new Material("Diffuse");
            ////material.SetColor("DiffuseColor", Color.LerpUnclamped(Color.Yellow, Color.White, 0.75f));

            var plane = new Plane
            {
                DoubleSided = true,
                Material = material,
            };
            plane.SetMesh(mesh, false);

            plane.Transform.LocalPosition = position;
            plane.Transform.LocalRotation = Quaternion.FromToRotation(Vector3.PositiveY, normal);
            return plane;
        }
    }
}
