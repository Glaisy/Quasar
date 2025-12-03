//-----------------------------------------------------------------------
// <copyright file="ExecuteBeforeAttribute.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Pipelines
{
    /// <summary>
    /// The marked class are inserted into the execution dependency list before the "executed before" class.
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ExecuteBeforeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteBeforeAttribute" /> class.
        /// </summary>
        /// <param name="executedBefore">The executed before type.</param>
        public ExecuteBeforeAttribute(Type executedBefore)
        {
            ArgumentNullException.ThrowIfNull(executedBefore, nameof(executedBefore));

            ExecutedBefore = executedBefore;
        }


        /// <summary>
        /// The type of class which executed after the marked class.
        /// </summary>
        public readonly Type ExecutedBefore;
    }
}
