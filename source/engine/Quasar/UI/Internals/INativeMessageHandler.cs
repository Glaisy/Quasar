//-----------------------------------------------------------------------
// <copyright file="INativeMessageHandler.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.UI.Internals
{
    /// <summary>
    /// Represents a native message processing component for UIs.
    /// </summary>
    internal interface INativeMessageHandler
    {
        /// <summary>
        /// Processes the event messages.
        /// </summary>
        void ProcessMessages();
    }
}
