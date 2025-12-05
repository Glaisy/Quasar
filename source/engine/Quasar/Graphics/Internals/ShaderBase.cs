//-----------------------------------------------------------------------
// <copyright file="ShaderBase.cs" company="Space Development">
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
using System.Linq;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Abstract base class for shader implementations.
    /// </summary>
    /// <seealso cref="CoreShaderBase" />
    /// <seealso cref="IShader" />
    internal abstract class ShaderBase : CoreShaderBase, IShader
    {
        private static readonly List<ShaderProperty> emptyProperties = new List<ShaderProperty>(0);
        private static readonly ShaderProperty emptyProperty = new ShaderProperty(-1, null, ShaderPropertyType.Unknown, 0);


        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderBase" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="descriptor">The graphic resource descriptor.</param>
        protected ShaderBase(string id, in GraphicsResourceDescriptor descriptor)
            : base(ShaderType.Compute, id, descriptor)
        {
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            drawProperties = emptyProperties;
            frameProperties = emptyProperties;
            lightProperties = emptyProperties;
            materialProperties = emptyProperties;
            viewProperties = emptyProperties;
            Properties = emptyProperties;

            base.Dispose(disposing);
        }


        private Dictionary<string, ShaderProperty> propertiesByName;
        /// <summary>
        /// Gets the <see cref="ShaderProperty"/> with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public ShaderProperty this[string name]
        {
            get
            {
                propertiesByName.TryGetValue(name, out var property);
                return property ?? emptyProperty;
            }
        }


        private List<ShaderProperty> drawProperties = emptyProperties;
        /// <summary>
        /// Gets the per draw properties.
        /// </summary>
        public IReadOnlyList<ShaderProperty> DrawProperties => drawProperties;

        private List<ShaderProperty> frameProperties = emptyProperties;
        /// <summary>
        /// Gets the per frame properties.
        /// </summary>
        public IReadOnlyList<ShaderProperty> FrameProperties => frameProperties;

        private List<ShaderProperty> lightProperties = emptyProperties;
        /// <summary>
        /// Gets the light properties.
        /// </summary>
        public IReadOnlyList<ShaderProperty> LightProperties => lightProperties;

        private List<ShaderProperty> materialProperties = emptyProperties;
        /// <summary>
        /// Gets the material properties.
        /// </summary>
        public IReadOnlyList<ShaderProperty> MaterialProperties => materialProperties;

        private List<ShaderProperty> viewProperties = emptyProperties;
        /// <summary>
        /// Gets the per vieww properties.
        /// </summary>
        public IReadOnlyList<ShaderProperty> ViewProperties => viewProperties;


        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(IShader other)
        {
            return base.Equals(other);
        }


        /// <summary>
        /// Initializes internal shader properties.
        /// </summary>
        internal void Initialize()
        {
            // discover all types of shader properties
            propertiesByName = EnumerateProperties();
            CategorizeProperties();
        }

        /// <summary>
        /// Sets the shader property value at index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        internal abstract void SetColor(int index, in Color value);

        /// <summary>
        /// Sets the shader property value at index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        internal abstract void SetCubeMapTexture(int index, ICubeMapTexture value);

        /// <summary>
        /// Sets the shader property value at index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        internal abstract void SetFloat(int index, float value);

        /// <summary>
        /// Sets the shader property value at index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        internal abstract void SetInteger(int index, int value);

        /// <summary>
        /// Sets the shader property value at index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        internal abstract void SetMatrix(int index, in Matrix4 value);

        /// <summary>
        /// Sets the shader property value at index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        internal abstract void SetTexture(int index, ITexture value);

        /// <summary>
        /// Sets the shader property value at index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        internal abstract void SetVector2(int index, in Vector2 value);

        /// <summary>
        /// Sets the shader property value at index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        internal abstract void SetVector3(int index, in Vector3 value);

        /// <summary>
        /// Sets the shader property value at index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        internal abstract void SetVector4(int index, in Vector4 value);


        /// <summary>
        /// Gets the properties by indices.
        /// </summary>
        protected IReadOnlyList<ShaderProperty> Properties { get; private set; } = emptyProperties;


        /// <summary>
        /// Enumerates the declared shader properties.
        /// </summary>
        /// <returns>The dictonary of declared shader properties by name.</returns>
        protected abstract Dictionary<string, ShaderProperty> EnumerateProperties();



        private static void AddProperty(ref List<ShaderProperty> properties, ShaderProperty property)
        {
            if (properties == emptyProperties)
            {
                properties = new List<ShaderProperty>();
            }

            properties.Add(property);
        }

        private void CategorizeProperties()
        {
            if (propertiesByName.Count == 0)
            {
                return;
            }

            // get the index ordered list of proprties
            Properties = propertiesByName.Values.OrderBy(x => x.Index).ToList();

            // categorize built-in properties
            foreach (var property in propertiesByName.Values)
            {
                // per frame properties
                if (ShaderConstants.BuiltInPerFramePropertyNames.Contains(property.Name))
                {
                    AddProperty(ref frameProperties, property);
                    continue;
                }

                // per view properties
                if (ShaderConstants.BuiltInPerViewPropertyNames.Contains(property.Name))
                {
                    AddProperty(ref viewProperties, property);
                    continue;
                }

                // per draw properties
                if (ShaderConstants.BuiltInPerDrawPropertyNames.Contains(property.Name))
                {
                    AddProperty(ref drawProperties, property);
                    continue;
                }

                // light properties
                var lastIndex = property.Name.Length - 1;
                var rawPropertyName = Char.IsDigit(property.Name[lastIndex]) ? property.Name.Substring(0, lastIndex) : property.Name;
                if (ShaderConstants.BuiltInPerLightPropertyNames.Contains(rawPropertyName))
                {
                    AddProperty(ref lightProperties, property);
                    continue;
                }

                // per material properties
                AddProperty(ref materialProperties, property);
            }
        }
    }
}
