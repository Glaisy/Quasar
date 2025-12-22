//-----------------------------------------------------------------------
// <copyright file="IBootstrapper.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar
{
    /// <summary>
    /// Represents the application bootstrapper component.
    /// </summary>
    public interface IBootstrapper
    {
        /// <summary>
        /// Executes the bootstrapping process.
        /// </summary>
        void Execute();
    }
}
