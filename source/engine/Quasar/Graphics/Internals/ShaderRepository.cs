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
using Space.Core.Diagnostics;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Shader repository implementation.
    /// </summary>
    /// <seealso cref="IShaderRepository" />
    [Export(typeof(IShaderRepository))]
    [Singleton]
    internal sealed class ShaderRepository : RepositoryBase<string, IShader, ShaderBase>, IShaderRepository
    {
        private readonly IShaderFactory shaderFactory;
        private readonly Dictionary<int, ShaderBase> shadersByHandle = new Dictionary<int, ShaderBase>();
        private readonly List<ShaderBase> selectedShaders = new List<ShaderBase>();
        private readonly ILogger logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderRepository" /> class.
        /// </summary>
        /// <param name="shaderFactory">The shader factory.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public ShaderRepository(
            IShaderFactory shaderFactory,
            ILoggerFactory loggerFactory)
        {
            this.shaderFactory = shaderFactory;

            logger = loggerFactory.Create<ShaderRepository>();
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

                shader = shaderFactory.CreateShader(id, source);
                shader.Tag = tag;
                AddItem(id, shader);

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
        public void DeleteByTag(string tag)
        {
            ArgumentException.ThrowIfNullOrEmpty(tag, nameof(tag));

            try
            {
                RepositoryLock.EnterWriteLock();

                FindItems(shader => shader.Tag == tag, selectedShaders);
                if (selectedShaders.Count == 0)
                {
                    return;
                }

                foreach (var shader in selectedShaders)
                {
                    DeleteShader(shader);
                }
            }
            finally
            {
                selectedShaders.Clear();
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

                return GetItem(id);
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
        public void LoadBuiltInShaders()
        {
            try
            {
                RepositoryLock.EnterWriteLock();


                // loads built-in shaders
                var loadedShaders = shaderFactory.LoadBuiltInShaders();
                AddShaders(loadedShaders, null);

                // make sure fallback shader is loaded
                fallbackShader = GetItem(ShaderConstants.FallbackShaderKey);
                if (fallbackShader == null)
                {
                    throw new InvalidOperationException($"Unable to resolve fallback shader '{ShaderConstants.FallbackShaderKey}'.");
                }
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }

        /// <inheritdoc/>
        public void Load(IResourceProvider resourceProvider, string resourceDirectoryPath, string tag = null)
        {
            ArgumentNullException.ThrowIfNull(resourceProvider, nameof(resourceProvider));

            try
            {
                RepositoryLock.EnterWriteLock();

                var loadedShaders = shaderFactory.LoadShaders(resourceProvider, resourceDirectoryPath);
                AddShaders(loadedShaders, tag);
            }
            finally
            {
                RepositoryLock.ExitWriteLock();
            }
        }


        /// <inheritdoc/>
        protected override void AddItem(string id, ShaderBase item)
        {
            base.AddItem(id, item);

            shadersByHandle.Add(item.Handle, item);
        }

        /// <inheritdoc/>
        protected override void ValidateIdentifier(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));
        }


        private void AddShaders(List<ShaderBase> shaders, string tag)
        {
            foreach (var shader in shaders)
            {
                try
                {
                    shader.Tag = tag;
                    AddItem(shader.Id, shader);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Unable to add shader: '{shader.Id}'. Skipped.");

                    shader.Dispose();
                }
            }
        }

        private void DeleteShader(ShaderBase shader)
        {
            DeleteItem(shader.Id);
            shadersByHandle.Remove(shader.Handle);
            shader.Dispose();
        }
    }
}
