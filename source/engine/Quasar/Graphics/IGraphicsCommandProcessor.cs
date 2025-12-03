//-----------------------------------------------------------------------
// <copyright file="IGraphicsCommandProcessor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Graphics
{
    /// <summary>
    /// Graphics command processor interface definition.
    /// </summary>
    public interface IGraphicsCommandProcessor
    {
        /// <summary>
        /// Checks if there is any graphics errors.
        /// </summary>
        void CheckErrors();

        /// <summary>
        /// Resets the state of the processsor.
        /// </summary>
        void ResetState();

        /// <summary>
        /// Sets the viewport.
        /// </summary>
        /// <param name="position">The position of the lower left corner.</param>
        /// <param name="size">The size.</param>
        void SetViewport(in Point position, in Size size);

        /// <summary>
        /// Sets the V-Sync mode.
        /// </summary>
        /// <param name="mode">The V-Sync mode.</param>
        void SetVSyncMode(VSyncMode mode);
    }
}
