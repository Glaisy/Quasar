//-----------------------------------------------------------------------
// <copyright file="NativeMessageHandler.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Windows.Forms;

using Quasar.UI.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.Windows.UI
{
    /// <summary>
    /// Represents a native message processing component for UIs.
    /// </summary>
    /// <seealso cref="INativeMessageHandler" />
    [Export(typeof(INativeMessageHandler))]
    [Singleton]
    internal sealed class NativeMessageHandler : INativeMessageHandler
    {
        /// <inheritdoc/>
        public void ProcessMessages()
        {
            Application.DoEvents();
        }
    }
}
