//-----------------------------------------------------------------------
// <copyright file="ShaderConstants.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace Quasar.Graphics
{
    /// <summary>
    /// Shader related constant definitions.
    /// </summary>
    public static class ShaderConstants
    {
        /// <summary>
        /// The built-in per frame shader property names.
        /// </summary>
        public static readonly HashSet<string> BuiltInPerFramePropertyNames =
        [
            DeltaTime,
            Time
        ];

        /// <summary>
        /// The built-in per light shader property names.
        /// </summary>
        public static readonly HashSet<string> BuiltInPerLightPropertyNames =
        [
            LightColor,
            LightDirectionWorldSpace,
            LightPositionWorldSpace,
            LightSourceType,
            ShadowBias,
            ShadowMap,
            ShadowStrength,
            ShadowTexelSize
        ];

        /// <summary>
        /// The built-in per view shader property names.
        /// </summary>
        public static readonly HashSet<string> BuiltInPerViewPropertyNames =
        [
            ProjectionMatrix,
            ViewMatrix,
            ViewProjectionMatrix,
            CameraPositionWorldSpace,
        ];

        /// <summary>
        /// The built-in per draw shader property names.
        /// </summary>
        public static readonly HashSet<string> BuiltInPerDrawPropertyNames =
        [
            LightModelViewProjectionMatrix,
            ModelMatrix,
            ModelViewMatrix,
            ModelViewProjectionMatrix
        ];

        /// <summary>
        /// The world space camera position property name.
        /// </summary>
        public const string CameraPositionWorldSpace = "CameraPositionWS";

        /// <summary>
        /// The color property name suffix.
        /// </summary>
        public const string ColorPropertyNameSuffix = "Color";

        /// <summary>
        /// The delta time property name.
        /// </summary>
        public const string DeltaTime = "DeltaTime";

        /// <summary>
        /// The fallback shader's identifier.
        /// </summary>
        public const string FallbackShaderId = "Fallback";

        /// <summary>
        /// The light color property name.
        /// </summary>
        public const string LightColor = "LightColor";

        /// <summary>
        /// The world space light direction property name.
        /// </summary>
        public const string LightDirectionWorldSpace = "LightDirectionWS";

        /// <summary>
        /// The light space model view projection matrix name.
        /// </summary>
        public const string LightModelViewProjectionMatrix = "LightModelViewProjectionMatrix";

        /// <summary>
        /// The world space light position property name.
        /// </summary>
        public const string LightPositionWorldSpace = "LightPositionWS";

        /// <summary>
        /// The light source type property name.
        /// </summary>
        public const string LightSourceType = "LightSourceType";

        /// <summary>
        /// The model matrix name.
        /// </summary>
        public const string ModelMatrix = "ModelMatrix";

        /// <summary>
        /// The model view matrix name.
        /// </summary>
        public const string ModelViewMatrix = "ModelViewMatrix";

        /// <summary>
        /// The model view projection matrix name.
        /// </summary>
        public const string ModelViewProjectionMatrix = "ModelViewProjectionMatrix";

        /// <summary>
        /// The normal map property name suffix.
        /// </summary>
        public const string NormalMapPropertyNameSuffix = "NormalMap";

        /// <summary>
        /// The projection matrix name.
        /// </summary>
        public const string ProjectionMatrix = "ProjectionMatrix";

        /// <summary>
        /// The shadow bias property name.
        /// </summary>
        public const string ShadowBias = "ShadowBias";

        /// <summary>
        /// The shadow map property name.
        /// </summary>
        public const string ShadowMap = "ShadowMap";

        /// <summary>
        /// The shadow strength property name.
        /// </summary>
        public const string ShadowStrength = "ShadowStrength";

        /// <summary>
        /// The shadow texel property name.
        /// </summary>
        public const string ShadowTexelSize = "ShadowTexelSize";

        /// <summary>
        /// The time property name.
        /// </summary>
        public const string Time = "Time";

        /// <summary>
        /// The view matrix name.
        /// </summary>
        public const string ViewMatrix = "ViewMatrix";

        /// <summary>
        /// The view projection matrix name.
        /// </summary>
        public const string ViewProjectionMatrix = "ViewProjectionMatrix";

        /// <summary>
        /// The view rotation projection (skybox) matrix name.
        /// </summary>
        public const string ViewRotationProjectionMatrix = "ViewRotationProjectionMatrix";
    }
}
