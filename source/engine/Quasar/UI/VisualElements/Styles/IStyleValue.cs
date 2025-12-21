//-----------------------------------------------------------------------
// <copyright file="IStyleValue.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements.Styles
{
    /// <summary>
    /// Represents a generic style property value.
    /// </summary>
    /// <typeparam name="T">The value's type.</typeparam>
    public interface IStyleValue<T>
    {
        /// <summary>
        /// Gets the flag.
        /// </summary>
        StyleFlag Flag { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        T Value { get; }
    }
}
