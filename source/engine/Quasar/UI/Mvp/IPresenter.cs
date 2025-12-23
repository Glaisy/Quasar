//-----------------------------------------------------------------------
// <copyright file="IPresenter.cs" company="Space Development">
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
    /// Represents a general UI presenter object.
    /// </summary>
    /// <typeparam name="TView">The view type.</typeparam>
    /// <seealso cref="IDisposable" />
    public interface IPresenter<TView> : IDisposable
        where TView : IView
    {
        /// <summary>
        /// Binds the specified view to the presenter.
        /// </summary>
        /// <param name="view">The view.</param>
        void Bind(TView view);
    }
}
