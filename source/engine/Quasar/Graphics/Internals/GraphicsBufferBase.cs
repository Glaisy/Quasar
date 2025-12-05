//-----------------------------------------------------------------------
// <copyright file="GraphicsBufferBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace Quasar.Graphics.Internals
{
    /// <summary>
    /// Abstract base class for graphics buffers.
    /// </summary>
    /// <seealso cref="GraphicsResourceBase" />
    /// <seealso cref="IGraphicsBuffer" />
    internal abstract class GraphicsBufferBase : GraphicsResourceBase, IGraphicsBuffer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsBufferBase" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="descriptor">The graphics resource descriptor.</param>
        protected GraphicsBufferBase(GraphicsBufferType type, in GraphicsResourceDescriptor descriptor)
            : base(descriptor)
        {
            Type = type;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            Type = GraphicsBufferType.Uninitialized;
            ElementCount = ElementSize = Size = 0;
        }


        /// <inheritdoc/>
        public int ElementCount { get; private set; }

        /// <inheritdoc/>
        public int ElementSize { get; private set; }

        /// <inheritdoc/>
        public int Size { get; private set; }

        /// <inheritdoc/>
        public int Stride { get; private set; }

        /// <inheritdoc/>
        public GraphicsBufferType Type { get; private set; }


        /// <inheritdoc/>
        public unsafe void GetData<T>(in Span<T> data)
            where T : unmanaged
        {
            var size = Marshal.SizeOf<T>() * data.Length;

            fixed (T* pointer = &data[0])
            {
                ReadData(new IntPtr(pointer), size);
            }
        }

        /// <inheritdoc/>
        public unsafe void GetData<T>(in Span<T> data, int start, int count)
            where T : unmanaged
        {
            ArgumentOutOfRangeException.ThrowIfNegative(start, nameof(start));
            ArgumentOutOfRangeException.ThrowIfNegative(count, nameof(count));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(start + count, data.Length, nameof(start) + "+" + nameof(count));

            if (count == 0)
            {
                return;
            }

            var size = Marshal.SizeOf<T>() * count;
            fixed (T* pointer = &data[start])
            {
                ReadData(new IntPtr(pointer), size);
            }
        }

        /// <inheritdoc/>
        public void GetData(IntPtr buffer, int size)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(buffer == IntPtr.Zero, true, nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfNegative(size, nameof(size));

            if (size == 0)
            {
                return;
            }

            ReadData(buffer, size);
        }

        /// <inheritdoc/>
        public void SetData(IntPtr buffer, int size, int elementSize = 0, int stride = 0)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(buffer == IntPtr.Zero, true, nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfNegative(size, nameof(size));
            ArgumentOutOfRangeException.ThrowIfNegative(elementSize, nameof(elementSize));
            ArgumentOutOfRangeException.ThrowIfNegative(stride, nameof(stride));

            Size = size;
            if (elementSize > 0)
            {
                ElementSize = elementSize;
                ElementCount = size / elementSize;
                Stride = Math.Max(elementSize, stride);
            }
            else
            {
                ElementCount = size;
                ElementSize = Stride = 1;
            }

            WriteData(buffer, size);
        }

        /// <inheritdoc/>
        public unsafe void SetData<T>(in Span<T> data)
            where T : unmanaged
        {
            ElementCount = data.Length;
            Stride = ElementSize = Marshal.SizeOf<T>();
            Size = ElementSize * data.Length;

            fixed (T* pointer = &data[0])
            {
                WriteData(new IntPtr(pointer), Size);
            }
        }

        /// <inheritdoc/>
        public unsafe void SetData<T>(in Span<T> data, int start, int count)
            where T : unmanaged
        {
            ArgumentOutOfRangeException.ThrowIfNegative(start, nameof(start));
            ArgumentOutOfRangeException.ThrowIfNegative(count, nameof(count));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(start + count, data.Length, nameof(start) + "+" + nameof(count));

            ElementCount = count;
            Stride = ElementSize = Marshal.SizeOf<T>();
            Size = ElementSize * count;

            fixed (T* pointer = &data[start])
            {
                WriteData(new IntPtr(pointer), Size);
            }
        }

        /// <inheritdoc/>
        public unsafe void SetData<T>(T[] data)
            where T : unmanaged
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));

            ElementCount = data.Length;
            Stride = ElementSize = Marshal.SizeOf<T>();
            Size = ElementSize * data.Length;

            fixed (T* pointer = &data[0])
            {
                WriteData(new IntPtr(pointer), Size);
            }
        }

        /// <inheritdoc/>
        public unsafe void SetData<T>(T[] data, int start, int count)
            where T : unmanaged
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));
            ArgumentOutOfRangeException.ThrowIfNegative(start, nameof(start));
            ArgumentOutOfRangeException.ThrowIfNegative(count, nameof(count));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(start + count, data.Length, nameof(start) + "+" + nameof(count));

            ElementCount = count;
            Stride = ElementSize = Marshal.SizeOf<T>();
            Size = ElementSize * count;

            fixed (T* pointer = &data[start])
            {
                WriteData(new IntPtr(pointer), Size);
            }
        }


        /// <summary>
        /// Reads the data from GPU to CPU.
        /// </summary>
        /// <param name="buffer">The CPU memory buffer.</param>
        /// <param name="size">The buffer size.</param>
        protected abstract void ReadData(IntPtr buffer, int size);

        /// <summary>
        /// Writes the data from CPU buffer to GPU.
        /// </summary>
        /// <param name="buffer">The CPU memory buffer.</param>
        /// <param name="size">The buffer size.</param>
        protected abstract void WriteData(IntPtr buffer, int size);
    }
}
