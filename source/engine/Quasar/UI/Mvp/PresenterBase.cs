//-----------------------------------------------------------------------
// <copyright file="PresenterBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core;

namespace Quasar.UI.Mvp
{
    /// <summary>
    /// Abstract base class for UI presenter implementations.
    /// </summary>
    /// <typeparam name="TView">The view type.</typeparam>
    /// <seealso cref="DisposableBase" />
    public abstract class PresenterBase<TView> : DisposableBase, IPresenter<TView>
        where TView : IView
    {
        /// <inheritdoc/>
        protected override sealed void Dispose(bool disposing)
        {
            OnUnload();
            View = default;
        }


        /// <inheritdoc/>
        void IPresenter<TView>.Bind(TView view)
        {
            Assertion.ThrowIfNull(view, nameof(view));
            Assertion.ThrowIfNotEqual(View, null, "View is already bound.");

            View = view;
            OnLoad();
        }


        /// <summary>
        /// Gets the view.
        /// </summary>
        protected TView View { get; private set; }


        /// <summary>
        /// Load event handler.
        /// Invoked when to view is bound to the presenter.
        /// </summary>
        protected abstract void OnLoad();

        /// <summary>
        /// Unload event handler.
        /// Invoked when the presenter is being destroyed.
        /// </summary>
        protected virtual void OnUnload()
        {
        }
    }
}
