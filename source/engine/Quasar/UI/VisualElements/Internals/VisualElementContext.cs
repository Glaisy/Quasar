//-----------------------------------------------------------------------
// <copyright file="VisualElementContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using Quasar.UI.VisualElements.Layouts;
using Quasar.UI.VisualElements.Styles;
using Quasar.UI.VisualElements.Styles.Internals;

using Space.Core.Collections;
using Space.Core.DependencyInjection;

namespace Quasar.UI.VisualElements.Internals
{
    /// <summary>
    /// Represents the context information for visual elements.
    /// </summary>
    [Export]
    [Singleton]
    internal sealed class VisualElementContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualElementContext" /> class.
        /// </summary>
        /// <param name="styleBuilder">The style builder.</param>
        /// <param name="poolFactory">The pool factory.</param>
        public VisualElementContext(
            IStyleBuilder styleBuilder,
            IPoolFactory poolFactory)
        {
            // layouts
            GridColumnPool = poolFactory.Create(false, () => new GridColumn(), x => x.Clear());
            GridColumnListPool = poolFactory.Create(false, () => new List<GridColumn>(), ClearGridColumns);
            GridRowPool = poolFactory.Create(false, () => new GridRow(), x => x.Clear());
            GridRowListPool = poolFactory.Create(false, () => new List<GridRow>(), ClearGridRows);

            // styles
            StylePool = poolFactory.Create(
                false,
                () =>
                {
                    var style = new Style();
                    styleBuilder.Copy(style, Style.Template);
                    return style;
                },
                style =>
                {
                    style.Selector = null;
                    styleBuilder.Copy(style, Style.Template);
                });

            // visual elements
            VisualElementListPool = poolFactory.Create(false, () => new List<VisualElement>(), x => x.Clear());
        }


        /// <summary>
        /// Gets the grid column pool.
        /// </summary>
        public IPool<GridColumn> GridColumnPool { get; }

        /// <summary>
        /// Gets the grid column pool.
        /// </summary>
        public IPool<List<GridColumn>> GridColumnListPool { get; }

        /// <summary>
        /// Gets the grid row pool.
        /// </summary>
        public IPool<GridRow> GridRowPool { get; }

        /// <summary>
        /// Gets the grid row list pool.
        /// </summary>
        public IPool<List<GridRow>> GridRowListPool { get; }

        /// <summary>
        /// Gets the style pool.
        /// </summary>
        public IPool<Style> StylePool { get; }

        /// <summary>
        /// Gets the visual element list pool.
        /// </summary>
        public IPool<List<VisualElement>> VisualElementListPool { get; }


        private void ClearGridColumns(List<GridColumn> gridColumns)
        {
            foreach (var gridColumn in gridColumns)
            {
                GridColumnPool.Release(gridColumn);
            }

            gridColumns.Clear();
        }

        private void ClearGridRows(List<GridRow> gridRows)
        {
            foreach (var gridRow in gridRows)
            {
                GridRowPool.Release(gridRow);
            }

            gridRows.Clear();
        }
    }
}
