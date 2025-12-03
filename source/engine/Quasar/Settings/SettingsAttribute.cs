//-----------------------------------------------------------------------
// <copyright file="SettingsAttribute.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Settings
{
    /// <summary>
    /// Settings type marker attribute.
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class SettingsAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsAttribute" /> class.
        /// </summary>
        /// <param name="registeredType">The registered type.</param>
        public SettingsAttribute(Type registeredType)
        {
            RegisteredType = registeredType;
        }


        /// <summary>
        /// Gets the registered settings type.
        /// </summary>
        public Type RegisteredType { get; }
    }
}
