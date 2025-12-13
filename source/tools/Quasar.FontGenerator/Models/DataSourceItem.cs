//-----------------------------------------------------------------------
// <copyright file="DataSourceItem.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.FontGenerator.Models
{
    /// <summary>
    /// Internal data source item object for UI item controls.
    /// </summary>
    /// <typeparam name="T">The value data type.</typeparam>
    internal sealed class DataSourceItem<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourceItem{T}"/> class.
        /// </summary>
        /// <param name="displayValue">The display value.</param>
        /// <param name="value">The value.</param>
        public DataSourceItem(string displayValue, T value)
        {
            DisplayValue = displayValue;
            Value = value;
        }

        /// <summary>
        /// Gets the display value.
        /// </summary>
        public string DisplayValue { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; }
    }
}
