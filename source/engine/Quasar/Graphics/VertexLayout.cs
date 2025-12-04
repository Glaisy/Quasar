//-----------------------------------------------------------------------
// <copyright file="VertexLayout.cs" company="Space Development">
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
using System.Linq;
using System.Runtime.InteropServices;

namespace Quasar.Graphics
{
    /// <summary>
    /// Represents a descriptor object for Vertex layouts.
    /// </summary>
    public sealed class VertexLayout
    {
        private bool isReadonly;


        /// <summary>
        /// Initializes a new instance of the <see cref="VertexLayout"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public VertexLayout(string name)
        {
            Name = name;
        }


        private readonly List<VertexElement> elements = new List<VertexElement>();
        /// <summary>
        /// Gets the elements.
        /// </summary>
        public IReadOnlyList<VertexElement> Elements => elements;

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the vertex stride (size in bytes).
        /// </summary>
        public int Stride { get; private set; }

        /// <summary>
        /// All valid vertex semantic names.
        /// </summary>
        public static readonly HashSet<string> ValidVertexSemanticNames = new HashSet<string>
        {
            VertexSemanticNames.BiTangent,
            VertexSemanticNames.Color,
            VertexSemanticNames.Normal,
            VertexSemanticNames.Position,
            VertexSemanticNames.PositionTransformed,
            VertexSemanticNames.Tangent,
            VertexSemanticNames.UV
        };


        /// <summary>
        /// Adds a vertex element to the layout.
        /// </summary>
        /// <typeparam name="T">The vertex element data type.</typeparam>
        /// <param name="semanticName">The vertex element's semantic name.</param>
        /// <param name="semanticIndex">The vertex element semantic's index.</param>
        /// <returns>
        /// The layout instance for method chaining.
        /// </returns>
        public VertexLayout AddElement<T>(string semanticName, int semanticIndex = 0)
            where T : struct
        {
            if (isReadonly)
            {
                throw new InvalidOperationException($"The vertex layout '{Name}' is read only.");
            }

            if (!ValidVertexSemanticNames.Contains(semanticName))
            {
                throw new ArgumentOutOfRangeException($"Invalid vertex element semantic name: '{semanticName}'.");
            }

            ArgumentOutOfRangeException.ThrowIfNegative(semanticIndex, nameof(semanticIndex));

            var isDuplicate = elements.Any(x => x.SemanticName == semanticName && x.SemanticIndex == semanticIndex);
            if (isDuplicate)
            {
                throw new ArgumentOutOfRangeException($"Duplicate vertex element semantic: '{semanticName}{semanticIndex}'.");
            }

            // add element and update stride
            var elementSize = Marshal.SizeOf<T>();
            elements.Add(new VertexElement(semanticName, semanticIndex, elementSize, Stride));
            Stride += elementSize;

            return this;
        }

        /// <summary>
        /// Marks the layout as read only.
        /// </summary>
        /// <returns>The layout instance for method chaining.</returns>
        public VertexLayout AsReadOnly()
        {
            isReadonly = true;
            return this;
        }

        /// <summary>
        /// Finds  vertex element by semantic name and index.
        /// </summary>
        /// <param name="semanticName">The semantic name.</param>
        /// <param name="semanticIndex">The semantic index.</param>
        /// <returns>The vertex element.</returns>
        public VertexElement FindElement(string semanticName, int semanticIndex = 0)
        {
            foreach (var element in elements)
            {
                if (element.SemanticName == semanticName &&
                    element.SemanticIndex == semanticIndex)
                {
                    return element;
                }
            }

            throw new ArgumentOutOfRangeException($"The vertex layout does not have an element with the following semantic: '{semanticName}{semanticIndex}'.");
        }
    }
}
