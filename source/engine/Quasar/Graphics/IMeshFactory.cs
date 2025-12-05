//-----------------------------------------------------------------------
// <copyright file="IMeshFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents an internal factory object for mesh creation.
    /// </summary>
    public interface IMeshFactory
    {
        /// <summary>
        /// Creates a new mesh instance by the specified parameters and data.
        /// </summary>
        /// <param name="primitiveType">Ty graphics primitive type.</param>
        /// <param name="vertexLayout">The vertex layout.</param>
        /// <param name="isIndexed">The indexed mesh flag.</param>
        /// <param name="name">The name.</param>
        /// <param name="usage">The usage.</param>
        /// <returns>
        /// The created mesh instance.
        /// </returns>
        IMesh Create(
            PrimitiveType primitiveType,
            VertexLayout vertexLayout,
            bool isIndexed,
            string name = null,
            GraphicsResourceUsage usage = GraphicsResourceUsage.Immutable);
    }
}
