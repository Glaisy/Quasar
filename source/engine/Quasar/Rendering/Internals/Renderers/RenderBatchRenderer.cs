//-----------------------------------------------------------------------
// <copyright file="RenderBatchRenderer.cs" company="Space Development">
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
using Quasar.Graphics.Internals;
using Quasar.Pipelines;

using Space.Core.DependencyInjection;

namespace Quasar.Rendering.Internals.Renderers
{
    /// <summary>
    /// Render batch renderer implementation.
    /// </summary>
    [Export]
    internal sealed class RenderBatchRenderer
    {
        private readonly ITimeProvider timeProvider;
        private ShaderBase shader;


        /// <summary>
        /// Initializes a new instance of the <see cref="RenderBatchRenderer" /> class.
        /// </summary>
        /// <param name="timeProvider">The time provider.</param>
        public RenderBatchRenderer(ITimeProvider timeProvider)
        {
            this.timeProvider = timeProvider;
        }


        /// <summary>
        /// Activates the renderer by the specified shader and rendering view.
        /// </summary>
        /// <param name="renderingView">The rendering view.</param>
        /// <param name="shader">The shader.</param>
        public void Activate(in RenderingView renderingView, ShaderBase shader)
        {
            this.shader = shader;

            // activate shader
            shader.Activate();

            // initialize general shader properties
            foreach (var shaderProperty in shader.FrameProperties)
            {
                switch (shaderProperty.Name)
                {
                    case ShaderConstants.DeltaTime:
                        shader.SetFloat(shaderProperty.Index, timeProvider.DeltaTime);
                        break;
                    case ShaderConstants.Time:
                        shader.SetFloat(shaderProperty.Index, timeProvider.Time);
                        break;
                }
            }

            foreach (var shaderProperty in shader.ViewProperties)
            {
                var camera = renderingView.Camera;
                switch (shaderProperty.Name)
                {
                    case ShaderConstants.ProjectionMatrix:
                        shader.SetMatrix(shaderProperty.Index, camera.ProjectionMatrix);
                        break;
                    case ShaderConstants.ViewMatrix:
                        shader.SetMatrix(shaderProperty.Index, camera.ViewMatrix);
                        break;
                    case ShaderConstants.ViewProjectionMatrix:
                        shader.SetMatrix(shaderProperty.Index, camera.ViewProjectionMatrix);
                        break;
                    case ShaderConstants.CameraPositionWorldSpace:
                        shader.SetVector3(shaderProperty.Index, camera.Transform.Position);
                        break;
                }
            }

            ////if (renderView.LightSourceProvider.Count > 0)
            ////{
            ////    // TODO: support more light sources
            ////    var lightSource = renderView.LightSourceProvider.MainLigntSource;
            ////    var shadowMap = renderView.ShadowMap;
            ////    foreach (var shaderProperty in shader.LightProperties)
            ////    {
            ////        switch (shaderProperty.Name)
            ////        {
            ////            case ShaderConstants.LightColor:
            ////                shader.SetColor(shaderProperty.Index, lightSource.EffectiveColor);
            ////                break;
            ////            case ShaderConstants.LightSourceType:
            ////                shader.SetInteger(shaderProperty.Index, (int)lightSource.Type);
            ////                break;
            ////            case ShaderConstants.LightDirectionWorldSpace:
            ////                shader.SetVector3(shaderProperty.Index, lightSource.Transform.NegativeZ);
            ////                break;
            ////            case ShaderConstants.LightPositionWorldSpace:
            ////                shader.SetVector3(shaderProperty.Index, lightSource.Transform.Position);
            ////                break;
            ////            case ShaderConstants.ShadowBias:
            ////                shader.SetFloat(shaderProperty.Index, shadowMap.Settings.Bias);
            ////                break;
            ////            case ShaderConstants.ShadowMap:
            ////                shader.SetTexture(shaderProperty.Index, shadowMap.FrameBuffer.DepthTexture);
            ////                break;
            ////            case ShaderConstants.ShadowStrength:
            ////                shader.SetFloat(shaderProperty.Index, shadowMap.Settings.Strength);
            ////                break;
            ////            case ShaderConstants.ShadowTexelSize:
            ////                var texelSize = shadowMap.Settings.SoftEdges ?
            ////                    1.0f / shadowMap.Settings.Resolution : 0.0f;
            ////                shader.SetVector2(shaderProperty.Index, new Vector2(texelSize));
            ////                break;
            ////        }
            ////    }
            ////}
        }

        /// <summary>
        /// Deactivates the renderer.
        /// </summary>
        public void Deactivate()
        {
            shader.Deactivate();
            shader = null;
        }

        /// <summary>
        /// Renders the models of the render batch.
        /// </summary>
        /// <param name="context">The rendering context.</param>
        /// <param name="renderingView">The rendering view.</param>
        /// <param name="renderBatch">The render batch.</param>
        public void Render(IRenderingContext context, in RenderingView renderingView, RenderBatch renderBatch)
        {
            var materialTimestamp = Int32.MinValue;
            if (renderBatch.DoubleSidedModels.Count > 0)
            {
                context.CommandProcessor.SetBackfaceCulling(false);
                RenderModels(context, renderingView, renderBatch.DoubleSidedModels, ref materialTimestamp);
            }

            if (renderBatch.Models.Count > 0)
            {
                context.CommandProcessor.SetBackfaceCulling(true);
                RenderModels(context, renderingView, renderBatch.Models, ref materialTimestamp);
            }
        }


        private void RenderModels(
            IRenderingContext context,
            in RenderingView renderingView,
            HashSet<RenderModel> renderModels,
            ref int materialTimestamp)
        {
            foreach (var renderModel in renderModels)
            {
                // update material properties in shader
                if (materialTimestamp != renderModel.Material.Timestamp)
                {
                    renderModel.Material.TransferToShader();
                    materialTimestamp = renderModel.Material.Timestamp;
                }

                // update draw properties
                if (shader.DrawProperties.Count > 0)
                {
                    ////var shadowMap = renderingView.ShadowMap;
                    var camera = renderingView.Camera;
                    foreach (var shaderProperty in shader.DrawProperties)
                    {
                        switch (shaderProperty.Name)
                        {
                            ////case ShaderConstants.LightModelViewProjectionMatrix:
                            ////    Matrix4 lightModelViewProjectionMatrix = default;
                            ////    Matrix4.Multiply(renderModel.ModelMatrix, renderView.ShadowMap.ViewProjectionMatrix, ref lightModelViewProjectionMatrix);
                            ////    shader.SetMatrix(shaderProperty.Index, lightModelViewProjectionMatrix);
                            ////    break;

                            case ShaderConstants.ModelMatrix:
                                shader.SetMatrix(shaderProperty.Index, renderModel.ModelMatrix);
                                break;
                            case ShaderConstants.ModelViewMatrix:
                                Matrix4 modelViewMatrix = default;
                                Matrix4.Multiply(renderModel.ModelMatrix, camera.ViewMatrix, ref modelViewMatrix);
                                shader.SetMatrix(shaderProperty.Index, modelViewMatrix);
                                break;
                            case ShaderConstants.ModelViewProjectionMatrix:
                                Matrix4 modelViewProjectionMatrix = default;
                                Matrix4.Multiply(renderModel.ModelMatrix, camera.ViewProjectionMatrix, ref modelViewProjectionMatrix);
                                shader.SetMatrix(shaderProperty.Index, modelViewProjectionMatrix);
                                break;
                        }
                    }
                }

                // draw mesh
                context.CommandProcessor.DrawMesh(renderModel.Mesh);
            }
        }
    }
}
