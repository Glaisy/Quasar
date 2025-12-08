//-----------------------------------------------------------------------
// <copyright file="IQuasarContext.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Core.IO;

using Space.Core;

namespace Quasar
{
    /// <summary>
    /// Represents the context information for the Quasar engine.
    /// </summary>
    public interface IQuasarContext
    {
        /// <summary>
        /// Gets the environment information.
        /// </summary>
        IEnvironmentInformation EnvironmentInformation { get; }

        /// <summary>
        /// Gets the resource provider for the built-in engine resources.
        /// </summary>
        IResourceProvider ResourceProvider { get; }
    }
}
