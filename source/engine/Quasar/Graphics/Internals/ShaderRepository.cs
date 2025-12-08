//-----------------------------------------------------------------------
// <copyright file="ShaderRepository.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Quasar.Collections;
using Quasar.Core.IO;
using Quasar.Graphics.Internals.Factories;

using Space.Core.DependencyInjection;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Shader repository implementation.
    /// </summary>
    /// <seealso cref="IShaderRepository" />
    [Export(typeof(IShaderRepository))]
    [Singleton]
    internal sealed class ShaderRepository : TaggedRepositoryBase<string, IShader, ShaderBase>, IShaderRepository
    {
        private readonly IShaderFactory shaderFactory;
        private readonly Dictionary<int, ShaderBase> shadersByHandle = new Dictionary<int, ShaderBase>();


        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderRepository" /> class.
        /// </summary>
        /// <param name="shaderFactory">The shader factory.</param>
        public ShaderRepository(IShaderFactory shaderFactory)
        {
            this.shaderFactory = shaderFactory;
        }


        private ShaderBase fallbackShader;
        /// <inheritdoc/>
        public IShader FallbackShader => fallbackShader;


        /// <inheritdoc/>
        public IShader Create(string id, ShaderSource source, string tag = null)
        {
            ValidateIdentifier(id);
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            ArgumentException.ThrowIfNullOrEmpty(source.VertexShader, nameof(source.VertexShader));
            ArgumentException.ThrowIfNullOrEmpty(source.FragmentShader, nameof(source.FragmentShader));

            ShaderBase shader = null;
            try
            {
                RepositoryLock.EnterWriteLock();

                EnsureIdentifierIsAvailable(id);

                shader = shaderFactory.CreateShader(id, source, tag);
                AddItem(shader);

                return shader;
            }
            catch
            {
                shader?.Dispose();

                throw;
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public ShaderBase GetShader(string id)
        {
            ValidateIdentifier(id);

            try
            {
                RepositoryLock.EnterReadLock();

                return GetItemById(id);
            }
            finally
            {
                RepositoryLock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public ShaderBase GetShader(int handle)
        {
            try
            {
                RepositoryLock.EnterReadLock();

                shadersByHandle.TryGetValue(handle, out var shader);
                return shader;
            }
            finally
            {
                RepositoryLock.ExitReadLock();
            }
        }

        /// <inheritdoc/>
        public void Load(IResourceProvider resourceProvider, string searchPath, string tag)
        {
            ArgumentNullException.ThrowIfNull(resourceProvider, nameof(resourceProvider));
            ArgumentException.ThrowIfNullOrEmpty(tag, nameof(tag));

            try
            {
                RepositoryLock.EnterWriteLock();

                var shadersToAdd = shaderFactory.LoadShaders(resourceProvider, searchPath, tag);
                AddItems(shadersToAdd);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public void LoadBuiltInShaders()
        {
            try
            {
                RepositoryLock.EnterWriteLock();

                var builtInShaders = shaderFactory.LoadBuiltInShaders();
                AddItems(builtInShaders);

                // make sure fallback shader is loaded
                fallbackShader = GetItemById(ShaderConstants.FallbackShaderId);
                if (fallbackShader == null)
                {
                    throw new InvalidOperationException($"Unable to resolve fallback shader '{ShaderConstants.FallbackShaderId}'.");
                }
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }


        /// <inheritdoc/>
        protected override void AddItem(ShaderBase item)
        {
            base.AddItem(item);

            shadersByHandle.Add(item.Handle, item);
        }

        /// <inheritdoc/>
        protected override bool DeleteItem(ShaderBase item)
        {
            var isDeleted = base.DeleteItem(item);
            if (!isDeleted)
            {
                return false;
            }

            shadersByHandle.Remove(item.Handle);
            return true;
        }

        /// <inheritdoc/>
        protected override void ValidateIdentifier(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
        }
    }
}
