//-----------------------------------------------------------------------
// <copyright file="ClearFrameRenderingPipelineStage.cs" company="Space Development">
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
using Quasar.Graphics;
using Quasar.Pipelines;
using Quasar.Rendering.Internals.Renderers;
using Quasar.Rendering.Internals.Services;
using Quasar.UI;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Pipelines
{
    /// <summary>
    /// Render pipeline's clear stage implementation.
    /// </summary>
    /// <seealso cref="RenderingPipelineStageBase" />
    [Export(typeof(RenderingPipelineStageBase), nameof(ClearFrameRenderingPipelineStage))]
    [ExecuteAfter(typeof(InitializeRenderingPipelineStage))]
    public sealed class ClearFrameRenderingPipelineStage : RenderingPipelineStageBase
    {
        private readonly IApplicationWindow applicationWindow;
        private readonly CameraService cameraService;
        private readonly SkyboxRenderer skyboxRenderer;
        private readonly HashSet<int> clearedFrameBuffers = new HashSet<int>();


        /// <summary>
        /// Initializes a new instance of the <see cref="ClearFrameRenderingPipelineStage" /> class.
        /// </summary>
        /// <param name="applicationWindow">The application window.</param>
        /// <param name="cameraService">The camera service.</param>
        /// <param name="skyboxRenderer">The skybox renderer.</param>
        internal ClearFrameRenderingPipelineStage(
            IApplicationWindow applicationWindow,
            CameraService cameraService,
            SkyboxRenderer skyboxRenderer)
        {
            this.applicationWindow = applicationWindow;
            this.cameraService = cameraService;
            this.skyboxRenderer = skyboxRenderer;
        }


        /// <inheritdoc/>
        protected override void OnApplySettings(IRenderingSettings renderingSettings)
        {
            applicationWindow.FullscreenMode = renderingSettings.FullScreenMode;
        }

        /// <inheritdoc/>
        protected override void OnExecute()
        {
            foreach (var camera in cameraService)
            {
                if (camera.ClearType == CameraClearType.None ||
                    clearedFrameBuffers.Contains(camera.FrameBuffer.Handle))
                {
                    continue;
                }

                switch (camera.ClearType)
                {
                    case CameraClearType.Skybox:
                        camera.FrameBuffer.ClearDepthBuffer();
                        skyboxRenderer.Render(Context, camera);
                        break;

                    case CameraClearType.Depth:
                        camera.FrameBuffer.ClearDepthBuffer();
                        break;

                    case CameraClearType.SolidColor:
                        camera.FrameBuffer.Clear(camera.ClearColor, true);
                        break;

                    default:
                        throw new NotSupportedException($"Camera clear type: {camera.ClearType}.");
                }

                clearedFrameBuffers.Add(camera.FrameBuffer.Handle);
            }

            if (!clearedFrameBuffers.Contains(Context.PrimaryFrameBuffer.Handle))
            {
                Context.PrimaryFrameBuffer.Clear(Color.Black, true);
            }

            clearedFrameBuffers.Clear();
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            skyboxRenderer.Initialize();
        }

        /// <inheritdoc/>
        protected override void OnSizeChanged(in Size size)
        {
            Context.CommandProcessor.SetViewport(Point.Empty, size);
        }
    }
}
