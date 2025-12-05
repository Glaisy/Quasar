//-----------------------------------------------------------------------
// <copyright file="GLShaderFactory.cs" company="Space Development">
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
using System.IO;
using System.Reflection;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Core.IO;
using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.Graphics.Internals.Factories;
using Quasar.OpenGL.Api;

using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;

namespace Quasar.OpenGL.Graphics.Factories
{
    /// <summary>
    /// OpenGL shader program implementation.
    /// </summary>
    [Export(typeof(IShaderFactory), nameof(GraphicsPlatform.OpenGL))]
    [Singleton]
    internal sealed class GLShaderFactory : IShaderFactory
    {
        private const string BuiltInShaderResourcePath = "Resources/Shaders";
        private const string BuiltInIncludeSubFolderPath = "Includes";
        private const string IncludeFileExtension = ".inc";
        private const string IncludeTokenStart = "#include <";
        private const string IncludeTokenEnd = ">";
        private const string FragmentShaderExtension = ".frag";
        private const string GeometryShaderExtension = ".geom";
        private const string TesselationShaderExtension = ".tess";
        private const string VertexShaderExtension = ".vert";
        private static readonly string[] ShaderExtensions =
        {
            FragmentShaderExtension,
            GeometryShaderExtension,
            TesselationShaderExtension,
            VertexShaderExtension
        };


        private readonly GraphicsResourceDescriptor shaderResourceDescriptor;
        private readonly IResourceProvider builtInShaderResourceProvider;
        private readonly ILogger logger;
        private readonly Dictionary<string, string> includes = new Dictionary<string, string>();


        /// <summary>
        /// Initializes a new instance of the <see cref="GLShaderFactory" /> class.
        /// </summary>
        /// <param name="graphicsDeviceContext">The graphics device context.</param>
        /// <param name="resourceProviderFactory">The resource provider factory.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public GLShaderFactory(
            [FromKeyedServices(GraphicsPlatform.OpenGL)] IGraphicsDeviceContext graphicsDeviceContext,
            IResourceProviderFactory resourceProviderFactory,
            ILoggerFactory loggerFactory)
        {
            shaderResourceDescriptor = new GraphicsResourceDescriptor(
                graphicsDeviceContext.Device,
                GraphicsResourceUsage.Immutable);
            builtInShaderResourceProvider = resourceProviderFactory
                .Create(Assembly.GetExecutingAssembly(), BuiltInShaderResourcePath);

            logger = loggerFactory.Create<GLShaderFactory>();
        }


        /// <inheritdoc/>
        public ComputeShaderBase CreateComputeShader(string id, string source)
        {
            var computeProgramId = 0;
            var shaderHandle = 0;

            try
            {
                computeProgramId = CompileShaderProgram(id, Api.ShaderType.ComputeShader, source);

                // create shader program
                shaderHandle = GL.CreateProgram();
                GL.AttachShader(shaderHandle, computeProgramId);
                GL.LinkProgram(shaderHandle);

                // detach shader before delete
                GL.DetachShader(shaderHandle, computeProgramId);

                // create shader instance.
                return new GLComputeShader(shaderHandle, id, shaderResourceDescriptor);
            }
            catch
            {
                if (shaderHandle > 0)
                {
                    GL.DeleteProgram(shaderHandle);
                }

                throw;
            }
            finally
            {
                if (computeProgramId > 0)
                {
                    GL.DeleteShader(computeProgramId);
                }
            }
        }

        /// <inheritdoc/>
        public ShaderBase CreateShader(string id, in ShaderSource source)
        {
            var vertexProgramId = 0;
            var fragmentProgramId = 0;
            var geometryProgramId = 0;
            var shaderHandle = 0;

            try
            {
                // compile sub-shader programs
                vertexProgramId = CompileShaderProgram(id, Api.ShaderType.VertexShader, source.VertexShader);
                fragmentProgramId = CompileShaderProgram(id, Api.ShaderType.FragmentShader, source.FragmentShader);
                if (!String.IsNullOrEmpty(source.GeometryShader))
                {
                    geometryProgramId = CompileShaderProgram(id, Api.ShaderType.GeometryShader, source.GeometryShader);
                }

                // create shader program
                shaderHandle = GL.CreateProgram();
                GL.AttachShader(shaderHandle, vertexProgramId);
                GL.AttachShader(shaderHandle, fragmentProgramId);
                if (geometryProgramId > 0)
                {
                    GL.AttachShader(shaderHandle, geometryProgramId);
                }

                GL.LinkProgram(shaderHandle);

                // detach shaders before delete
                GL.DetachShader(shaderHandle, vertexProgramId);
                GL.DetachShader(shaderHandle, fragmentProgramId);
                if (geometryProgramId > 0)
                {
                    GL.DetachShader(shaderHandle, geometryProgramId);
                }

                // create shader instance.
                var shader = new GLShader(shaderHandle, id, shaderResourceDescriptor);
                shader.Initialize();
                return shader;
            }
            catch
            {
                if (shaderHandle > 0)
                {
                    GL.DeleteProgram(shaderHandle);
                }

                throw;
            }
            finally
            {
                if (vertexProgramId > 0)
                {
                    GL.DeleteShader(vertexProgramId);
                }

                if (fragmentProgramId > 0)
                {
                    GL.DeleteShader(fragmentProgramId);
                }

                if (geometryProgramId > 0)
                {
                    GL.DeleteShader(geometryProgramId);
                }
            }
        }

        /// <inheritdoc/>
        public List<ShaderBase> LoadBuiltInShaders()
        {
            return LoadShaders(builtInShaderResourceProvider, null);
        }

        /// <inheritdoc/>
        public List<ShaderBase> LoadShaders(IResourceProvider resourceProvider, string resourceDirectoryPath)
        {
            // load include files
            var includeFilePath = resourceProvider.PathResolver.Resolve(BuiltInIncludeSubFolderPath, resourceDirectoryPath);
            LoadIncludes(resourceProvider, includeFilePath);

            // load shader sources
            var shaderSources = LoadShaderSources(resourceProvider, resourceDirectoryPath);

            // create and add shaders
            var shaders = new List<ShaderBase>();
            foreach (var pair in shaderSources)
            {
                var shaderSource = pair.Value;

                ShaderBase shader = null;
                try
                {
                    shader = CreateShader(pair.Key, shaderSource);
                    shaders.Add(shader);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Unable to load create shader '{pair.Key}'. Skipped.");

                    shader?.Dispose();
                }
            }

            return shaders;
        }


        private static int CompileShaderProgram(string id, Api.ShaderType shaderType, string sourceCode)
        {
            var handle = 0;
            try
            {
                handle = GL.CreateShader(shaderType);
                GL.ShaderSource(handle, sourceCode);
                GL.CompileShader(handle);
                if (GL.GetShaderParameter(handle, ShaderParameter.CompileStatus) == 0)
                {
                    var errorMessage = GL.GetShaderInformationLog(handle);
                    throw new OpenGLShaderException(id, shaderType, errorMessage);
                }
            }
            catch
            {
                // delete shader on error
                if (handle > 0)
                {
                    GL.DeleteShader(handle);
                }

                throw;
            }

            return handle;
        }

        private void LoadIncludes(IResourceProvider resourceProvider, string includeBasePath)
        {
            // load include assets
            var includeAssetNames = resourceProvider.EnumerateResources(includeBasePath, true, IncludeFileExtension);
            var includeNameIndex = resourceProvider.BasePath.Length + includeBasePath.Length + 2;
            foreach (var includeAssetName in includeAssetNames)
            {
                var includeName = includeAssetName.Substring(includeNameIndex);

                using (var reader = new StreamReader(resourceProvider.GetResourceStream(includeAssetName)))
                {
                    var includeAsset = reader.ReadToEnd();
                    includes.Add(includeName, includeAsset);
                }
            }
        }

        private Dictionary<string, ShaderSource> LoadShaderSources(IResourceProvider resourceProvider, string sourceBasePath)
        {
            var shaderSources = new Dictionary<string, ShaderSource>();
            var shaderSourcePrefix = resourceProvider.GetAbsolutePath(sourceBasePath);
            var shaderNameIndex = shaderSourcePrefix.Length + 1;

            var resourceNames = resourceProvider.EnumerateResources(sourceBasePath, true, ShaderExtensions);
            var included = new HashSet<string>();
            foreach (var resourceName in resourceNames)
            {
                // get shader name and extension
                var extension = resourceProvider.GetResourceExtension(resourceName);
                var length = resourceName.Length - extension.Length - shaderNameIndex;
                var shaderName = resourceName.Substring(shaderNameIndex, length);

                // get or create shader source object
                if (!shaderSources.TryGetValue(shaderName, out var shaderSource))
                {
                    shaderSource = new ShaderSource();
                    shaderSources[shaderName] = shaderSource;
                }

                // load source from resource stream into the shader source.
                using (var reader = new StreamReader(resourceProvider.GetResourceStream(resourceName)))
                {
                    var source = reader.ReadToEnd();

                    switch (extension)
                    {
                        case FragmentShaderExtension:
                            shaderSource.FragmentShader = ParseShaderSource(shaderName, source, included);
                            break;
                        case GeometryShaderExtension:
                            shaderSource.GeometryShader = ParseShaderSource(shaderName, source, included);
                            break;
                        case TesselationShaderExtension:
                            shaderSource.TesselationShader = ParseShaderSource(shaderName, source, included);
                            break;
                        case VertexShaderExtension:
                            shaderSource.VertexShader = ParseShaderSource(shaderName, source, included);
                            break;
                        default:
                            throw new InvalidOperationException($"Unknown shader source extension: '{extension}'.");
                    }
                }
            }

            return shaderSources;
        }

        private string ParseShaderSource(string shaderName, string shaderSource, HashSet<string> included)
        {
            var sb = new StringBuilder();
            included.Clear();
            using (var reader = new StringReader(shaderSource))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim(' ');

                    // add line if no include token.
                    if (!line.StartsWith(IncludeTokenStart) ||
                       !line.EndsWith(IncludeTokenEnd))
                    {
                        sb.AppendLine(line);
                        continue;
                    }

                    // extract include name
                    var length = line.Length - IncludeTokenStart.Length - IncludeTokenEnd.Length;
                    var includeName = line.Substring(IncludeTokenStart.Length, length);

                    // already included?
                    if (included.Contains(includeName))
                    {
                        continue;
                    }

                    // resolve include
                    if (!includes.TryGetValue(includeName, out var resolvedInclude))
                    {
                        throw new OpenGLShaderException($"Unable to resolve include '{includeName}' for '{shaderName}' shader.");
                    }

                    sb.Append(resolvedInclude);

                    // mark as included
                    included.Add(includeName);
                }
            }

            return sb.ToString();
        }
    }
}
