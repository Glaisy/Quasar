//-----------------------------------------------------------------------
// <copyright file="ViewBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;

using Quasar.UI.VisualElements;

using Space.Core.Globalization;
using Space.Core.Threading;

namespace Quasar.UI.Mvp
{
    /// <summary>
    /// Abstract base class for UI views.
    /// </summary>
    /// <seealso cref="VisualElement" />
    public abstract class ViewBase : VisualElement
    {
        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static new void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            Dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
            Translator = serviceProvider.GetRequiredService<ITranslator>();
            ValueFormatter = serviceProvider.GetRequiredService<IValueFormatter>();
        }


        /// <summary>
        /// Gets the dispatcher.
        /// </summary>
        protected static IDispatcher Dispatcher { get; private set; }

        /// <summary>
        /// Gets the translator.
        /// </summary>
        protected static ITranslator Translator { get; private set; }

        /// <summary>
        /// Gets the value formatter.
        /// </summary>
        protected static IValueFormatter ValueFormatter { get; private set; }
    }
}
