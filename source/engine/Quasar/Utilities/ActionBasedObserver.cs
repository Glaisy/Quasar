//-----------------------------------------------------------------------
// <copyright file="ActionBasedObserver.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Utilities
{
    /// <summary>
    /// Action based observer implementation.
    /// </summary>
    /// <typeparam name="T">The observed data type.</typeparam>
    /// <seealso cref="IObserver{T}" />
    public sealed class ActionBasedObserver<T> : IObserver<T>
    {
        private readonly Action onCompleted;
        private readonly Action<T> onNext;
        private readonly Action<Exception> onError;


        /// <summary>
        /// Initializes a new instance of the <see cref="ActionBasedObserver{T}"/> class.
        /// </summary>
        /// <param name="onNext">The next data event handler action.</param>
        /// <param name="onError">The error event handler action.</param>
        /// <param name="onCompleted">The completed event handler action.</param>
        public ActionBasedObserver(Action<T> onNext, Action<Exception> onError = null, Action onCompleted = null)
        {
            this.onNext = onNext;
            this.onError = onError;
            this.onCompleted = onCompleted;
        }


        /// <inheritdoc/>
        public void OnCompleted()
        {
            onCompleted?.Invoke();
        }

        /// <inheritdoc/>
        public void OnError(Exception error)
        {
            onError?.Invoke(error);
        }

        /// <inheritdoc/>
        public void OnNext(T value)
        {
            onNext(value);
        }
    }
}
