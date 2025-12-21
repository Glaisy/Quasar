//-----------------------------------------------------------------------
// <copyright file="IThemeProvider.cs" company="Space Development">
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
    /// Represetns the Quasar theme provider component.
    /// </summary>
    public interface IThemeProvider
    {
        /// <summary>
        /// Gets the current theme.
        /// </summary>
        ITheme CurrentTheme { get; }
    }
}
