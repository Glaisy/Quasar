//-----------------------------------------------------------------------
// <copyright file="NativeWindowFactory.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Windows.Forms;

using Quasar.Graphics;
using Quasar.Inputs.Internals;
using Quasar.UI;
using Quasar.UI.Internals;

using Space.Core.DependencyInjection;

namespace Quasar.Windows.UI
{
    /// <summary>
    /// Represents a native window factory component for UIs.
    /// </summary>
    /// <seealso cref="INativeWindowFactory" />
    [Export(typeof(INativeWindowFactory))]
    [Singleton]
    internal sealed class NativeWindowFactory : INativeWindowFactory
    {
        private readonly InputMapper inputMapper;
        private readonly IInputEventProcessor inputEventProcessor;


        /// <summary>
        /// Initializes a new instance of the <see cref="NativeWindowFactory"/> class.
        /// </summary>
        /// <param name="inputMapper">The input mapper.</param>
        /// <param name="inputEventProcessor">The input event processor.</param>
        public NativeWindowFactory(
            InputMapper inputMapper,
            IInputEventProcessor inputEventProcessor)
        {
            this.inputMapper = inputMapper;
            this.inputEventProcessor = inputEventProcessor;
        }


        /// <inheritdoc/>
        public IApplicationWindow CreateApplicationWindow(
            ApplicationWindowType applicationWindowType,
            string title,
            float screenRatio)
        {
            var screenSize = Screen.PrimaryScreen.Bounds.Size;

            var size = new Size((int)(screenSize.Width * screenRatio), (int)(screenSize.Height * screenRatio));
            return new ApplicationWindow(inputMapper, inputEventProcessor, applicationWindowType, title, size);
        }
    }
}
