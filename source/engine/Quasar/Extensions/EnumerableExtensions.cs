//-----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Space Development">
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

using Quasar.Collections;

namespace Quasar.Extensions
{
    /// <summary>
    /// Enumerable extension methods.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Gets the dependency ordered list of data objects associated to the dependency nodes (instance).
        /// </summary>
        /// <typeparam name="T">The data object type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="throwErrorOnCycles">if set to <c>true</c> [throw error on cycles].</param>
        public static List<T> Sort<T>(this IEnumerable<DependencyNode<T>> instance, bool throwErrorOnCycles)
        {
            var sortedList = new List<T>();
            var visitedDependencyNodes = new HashSet<DependencyNode<T>>();
            foreach (var dependencyNode in instance)
            {
                VisitDependencyNode(dependencyNode, visitedDependencyNodes, sortedList, throwErrorOnCycles);
            }

            return sortedList;
        }


        private static void VisitDependencyNode<T>(
            DependencyNode<T> dependencyNode,
            HashSet<DependencyNode<T>> visitedDependencyNodes,
            ICollection<T> sortedItems,
            bool throwErrorOnCycles)
        {
            if (!visitedDependencyNodes.Contains(dependencyNode))
            {
                visitedDependencyNodes.Add(dependencyNode);

                foreach (var dependencyNodes in dependencyNode.DependencyNodes)
                {
                    VisitDependencyNode(dependencyNodes, visitedDependencyNodes, sortedItems, throwErrorOnCycles);
                }

                sortedItems.Add(dependencyNode.Data);
            }
            else
            {
                if (throwErrorOnCycles &&
                    !sortedItems.Contains(dependencyNode.Data))
                {
                    throw new ArgumentException($"Circular dependency found at node {dependencyNode}.");
                }
            }
        }
    }
}