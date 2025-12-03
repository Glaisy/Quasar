//-----------------------------------------------------------------------
// <copyright file="ExecuteAfterAttribute.cs" company="Space Development">
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
    /// The marked class are inserted into the execution dependency list after the "executed after" class.
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ExecuteAfterAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteAfterAttribute" /> class.
        /// </summary>
        /// <param name="executedAfter">The executed after.</param>
        public ExecuteAfterAttribute(Type executedAfter)
        {
            ArgumentNullException.ThrowIfNull(executedAfter, nameof(executedAfter));

            ExecutedAfter = executedAfter;
        }


        /// <summary>
        /// The type of class which executed before the marked class.
        /// </summary>
        public readonly Type ExecutedAfter;
    }
}
