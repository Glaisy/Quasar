//-----------------------------------------------------------------------
// <copyright file="DependencyNode.cs" company="Space Development">
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

namespace Quasar.Collections
{
    /// <summary>
    /// Dependency node object to describe dependency relations between data objects.
    /// </summary>
    /// <typeparam name="T">The dependency data type.</typeparam>
    public sealed class DependencyNode<T>
    {
        private static readonly List<DependencyNode<T>> emptyDependencyNodes = new List<DependencyNode<T>>(0);


        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyNode{T}" /> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public DependencyNode(T data)
        {
            Data = data;
        }


        /// <summary>
        /// Gets the data associated with the dependency node object.
        /// </summary>
        public T Data { get; }


        private List<DependencyNode<T>> dependencyNodes = emptyDependencyNodes;
        /// <summary>
        /// Gets the dependency nodes for this node.
        /// </summary>
        public IReadOnlyList<DependencyNode<T>> DependencyNodes => dependencyNodes;


        /// <summary>
        /// Adds the dependency node.
        /// </summary>
        /// <param name="dependencyNode">The dependency node.</param>
        public void AddDependencyNode(DependencyNode<T> dependencyNode)
        {
            ArgumentNullException.ThrowIfNull(dependencyNode, nameof(dependencyNode));

            if (dependencyNodes == emptyDependencyNodes)
            {
                dependencyNodes = new List<DependencyNode<T>>();
            }

            dependencyNodes.Add(dependencyNode);
        }
    }
}
