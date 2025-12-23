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
        /// <param name="view">The view.</param>
        protected ViewBase(IPresenter<TView> presenter, TView view)
        {
            this.presenter = presenter;
            this.view = view;
        }


        /// <inheritdoc/>
        protected override void OnLoad()
        {
            presenter.Bind(view);
        }

        /// <inheritdoc/>
        protected override void OnUnload()
        {
            presenter.Dispose();
        }
    }
}
