//-----------------------------------------------------------------------
// <copyright file="VisualElement.InvalidationFlags.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Represents a basic UI visual element - InvalidationFlags.
    /// </summary>
    public partial class VisualElement
    {
        /// <summary>
        /// The invalidation flags constants.
        /// </summary>
        protected static class InvalidationFlags
        {
            /// <summary>
            /// The no invalidation flags.
            /// </summary>
            public const int None = 0;

            /// <summary>
            /// The styles invalidation flag.
            /// </summary>
            public const int Styles = 1;

            /// <summary>
            /// The layout invalidation flag.
            /// </summary>
            public const int Layout = 2;

            /// <summary>
            /// The preferred size invalidation flag.
            /// </summary>
            public const int PreferredSize = 4;

            /// <summary>
            /// The pseudo class invalidation flag.
            /// </summary>
            public const int PseudoClass = 8;

            /// <summary>
            /// The content alignment invalidation flag.
            /// </summary>
            public const int ContentAlignment = 16;

            /// <summary>
            /// The canvas invalidation flag.
            /// </summary>
            public const int Canvas = 32;

            /// <summary>
            /// All invalidation flags.
            /// </summary>
            public const int All = Styles | Layout | PreferredSize | PseudoClass | ContentAlignment | Canvas;
        }
    }
}
