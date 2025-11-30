//-----------------------------------------------------------------------
// <copyright file="SettingsEntry.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Settings.Internals
{
    /// <summary>
    /// Abstract base class for settings entry objects.
    /// </summary>
    internal abstract class SettingsEntry
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        public abstract ISettings Value { get; }


        /// <summary>
        /// Sets the default value.
        /// </summary>
        public abstract void SetDefaultValue();

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        public abstract void SetValue(ISettings newValue);
    }
}
