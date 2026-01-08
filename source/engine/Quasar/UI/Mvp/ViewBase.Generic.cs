//-----------------------------------------------------------------------
// <copyright file="ViewBase.Generic.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.UI.Mvp
{
    /// <summary>
    /// Abstract base generic class for UI views.
    /// </summary>
    /// <typeparam name="TView">The implemented view type.</typeparam>
    /// <seealso cref="ViewBase" />
    public abstract class ViewBase<TView> : ViewBase
        where TView : class, IView
    {
        private readonly IPresenter<TView> presenter;
        private readonly TView view;


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewBase{T}" /> class.
        /// </summary>
        /// <param name="presenter">The presenter.</param>
        protected ViewBase(IPresenter<TView> presenter)
        {
            this.presenter = presenter;
            view = this as TView
                ?? throw new InvalidOperationException($"The {GetType().FullName} class must implement {typeof(TView).FullName} interface.");

            presenter.Bind(view);
        }


        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            presenter?.Dispose();

            base.Dispose(disposing);
        }
    }
}
