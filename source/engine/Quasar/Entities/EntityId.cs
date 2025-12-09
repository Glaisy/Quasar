//-----------------------------------------------------------------------
// <copyright file="EntityId.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Quasar.Entities
{
    /// <summary>
    /// Represents an identifier for entities (Immutable).
    /// </summary>
    /// <seealso cref="IEquatable{EntityId}" />
    /// <seealso cref="IComparable{EntityId}" />
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct EntityId : IEquatable<EntityId>, IComparable<EntityId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityId"/> struct.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        internal EntityId(int id, int version)
        {
            Id = id;
            Version = version;
        }


        /// <summary>
        /// The empty entity identifier.
        /// </summary>
        public static readonly EntityId Empty = default;

        /// <summary>
        /// The identifier.
        /// </summary>
        public readonly int Id;

        /// <summary>
        /// The version.
        /// </summary>
        public readonly int Version;


        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(in EntityId left, in EntityId right)
        {
            return left.Id == right.Id && left.Version == right.Version;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(in EntityId left, in EntityId right)
        {
            return left.Id != right.Id || left.Version != right.Version;
        }

        /// <inheritdoc/>
        public int CompareTo(EntityId other)
        {
            var result = Id.CompareTo(other.Id);
            if (result != 0)
            {
                return result;
            }

            return Version.CompareTo(other.Version);
        }

        /// <inheritdoc/>
        public bool Equals(EntityId other)
        {
            return this == other;
        }

        /// <inheritdoc/>
        public override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj is not EntityId other)
            {
                return false;
            }

            return this == other;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Version);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[{Id}:{Version}]";
        }
    }
}
