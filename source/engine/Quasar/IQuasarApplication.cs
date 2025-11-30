//-----------------------------------------------------------------------
// <copyright file="IQuasarApplication.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar
{
    /// <summary>
    /// Represents a Quasar application.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public interface IQuasarApplication : IDisposable
    {
        /// <summary>
        /// Runs the execution loop.
        /// </summary>
        void Run();
    }
}
