//-----------------------------------------------------------------------
// <copyright file="AssetImporterBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.IO;

using Space.Core.Diagnostics;
using Space.Core.Threading;

namespace Quasar.Assets.Importers.Internals
{
    /// <summary>
    /// Abstract base class for Quasar asset importers.
    /// </summary>
    /// <typeparam name="T">The internal asset data type.</typeparam>
    /// <seealso cref="IAssetImporter" />
    internal abstract class AssetImporterBase<T> : IAssetImporter
    {
        private readonly IDispatcher dispatcher;
        private readonly bool hasDispatchedTask;


        /// <summary>
        /// Initializes a new instance of the <see cref="AssetImporterBase{T}" /> class.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="assetType">Type of the asset.</param>
        /// <param name="hasDispatchedTask">If true dispatched import step should be invoked; otherwise not.</param>
        protected AssetImporterBase(
            IDispatcher dispatcher,
            ILoggerFactory loggerFactory,
            AssetType assetType,
            bool hasDispatchedTask)
        {
            this.dispatcher = dispatcher;
            this.hasDispatchedTask = hasDispatchedTask;

            Type = assetType;
            Logger = loggerFactory.Create(GetType());
        }


        /// <inheritdoc/>
        public AssetType Type { get; }


        /// <inheritdoc/>
        public void BeginImport(Stream stream, string id, string tag, Action<string, bool> onCompleted)
        {
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
            ArgumentNullException.ThrowIfNull(onCompleted, nameof(onCompleted));

            try
            {
                var assetData = OnBeginImport(stream, id, tag);
                var importState = new AssetImportState<T>(id, assetData, onCompleted, tag);

                if (!hasDispatchedTask)
                {
                    onCompleted?.Invoke(id, true);
                    return;
                }

                dispatcher.Dispatch(DispatchedTaskMethod, importState);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);

                onCompleted?.Invoke(id, false);
            }
        }


        /// <summary>
        /// The logger.
        /// </summary>
        protected readonly ILogger Logger;


        /// <summary>
        /// Starts the asset importing process.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The internal asset data.</returns>
        protected abstract T OnBeginImport(Stream stream, string id, string tag);

        /// <summary>
        /// The Finished the asset importing via a dispatched task.
        /// </summary>
        /// <param name="state">The state.</param>
        protected virtual void OnEndImport(in AssetImportState<T> state)
        {
        }


        private void DispatchedTaskMethod(AssetImportState<T> state)
        {
            var succeeded = false;
            try
            {
                OnEndImport(state);
                succeeded = true;
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
            finally
            {
                state.OnCompleted?.Invoke(state.Id, succeeded);
            }
        }
    }
}
