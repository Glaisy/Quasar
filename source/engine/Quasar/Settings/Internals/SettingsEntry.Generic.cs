//-----------------------------------------------------------------------
// <copyright file="SettingsEntry.Generic.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Space.Core.Settings;
using Space.Core.Utilities;

namespace Quasar.Settings.Internals
{
    /// <summary>
    /// Generic settings entry object implementations.
    /// </summary>
    /// <typeparam name="T">The settings object type.</typeparam>
    /// <seealso cref="SettingsEntry" />
    internal sealed class SettingsEntry<T> : SettingsEntry
        where T : ISettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsEntry{T}"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        public SettingsEntry(T initialValue)
        {
            value = initialValue;
        }


        /// <summary>
        /// The changed event observable.
        /// </summary>
        public readonly Observable<T> Changed = new Observable<T>();

        private readonly T value;
        /// <inheritdoc/>
        public override ISettings Value => value;



        /// <inheritdoc/>
        public override void SetDefaultValue()
        {
            value.SetDefaults();
        }

        /// <inheritdoc/>
        public override void SetValue(ISettings newValue)
        {
            value.Copy(newValue);
            Changed.Push(value);
        }
    }
}
