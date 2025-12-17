//-----------------------------------------------------------------------
// <copyright file="TransformBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar
{
    /// <summary>
    /// Abstract base class for 3D space transformation objects.
    /// </summary>
    /// <seealso cref="INameProvider" />
    /// <seealso cref="InvalidatableBase" />
    public abstract partial class TransformBase : InvalidatableBase, INameProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransformBase" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected TransformBase(string name = null)
        {
            Name = name;
        }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
