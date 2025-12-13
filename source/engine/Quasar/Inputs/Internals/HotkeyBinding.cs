//-----------------------------------------------------------------------
// <copyright file="HotkeyBinding.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

using Space.Core;

namespace Quasar.Inputs.Internals
{
    /// <summary>
    /// Represent a keyboard hotkey binding object.
    /// </summary>
    /// <seealso cref="DisposableBase" />
    /// <seealso cref="IIdentifierProvider{Int32}" />
    internal sealed class HotkeyBinding : DisposableBase, IIdentifierProvider<long>
    {
        private readonly Action<HotkeyBinding> disposeAction;


        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyBinding" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="keyCodes">The key codes.</param>
        /// <param name="disposeAction">The dispose action.</param>
        public HotkeyBinding(long id, KeyCode[] keyCodes, Action<HotkeyBinding> disposeAction)
        {
            this.disposeAction = disposeAction;

            Id = id;
            KeyCodes = keyCodes;
        }

        /// <summary>
        /// Internal dispose/finalize event handler.
        /// </summary>
        /// <param name="disposing">Flag indicated the method is invoked by the dispose/finalize method.</param>
        protected override void Dispose(bool disposing)
        {
            Handler = null;

            disposeAction(this);
        }


        /// <summary>
        /// Gets or sets the handler.
        /// </summary>
        public Action Handler { get; set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Gets the key codes.
        /// </summary>
        public KeyCode[] KeyCodes { get; }
    }
}
