//-----------------------------------------------------------------------
// <copyright file="ObjectsModelsAndSkyboxTutorialView.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core.DependencyInjection;

namespace DemoApplication.Tutorials.Objects
{
    /// <summary>
    /// Tutorial which demonstrates loading models, creating rendered object and skybox.
    /// </summary>
    /// <seealso cref="TutorialViewBase" />
    [Export]
    internal sealed class ObjectsModelsAndSkyboxTutorialView : TutorialViewBase
    {
        /// <inheritdoc/>
        public override string Title => "Objects and models";
    }
}
