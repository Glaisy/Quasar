//-----------------------------------------------------------------------
// <copyright file="IGraphicsBuffer.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.Graphics
{
    /// <summary>
    /// Graphics buffer interface definition.
    /// </summary>
    /// <seealso cref="IGraphicsResource" />
    public interface IGraphicsBuffer : IGraphicsResource
    {
        /// <summary>
        /// Gets the element count.
        /// </summary>
        int ElementCount { get; }

        /// <summary>
        /// Gets the element size.
        /// </summary>
        int ElementSize { get; }

        /// <summary>
        /// Gets the total number of bytes of the elements in the buffer.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Gets the stride of elements.
        /// </summary>
        int Stride { get; }

        /// <summary>
        /// Gets the buffer type.
        /// </summary>
        GraphicsBufferType Type { get; }


        /// <summary>
        /// Reads the GPU buffer data to a CPU memory span.
        /// </summary>
        /// <typeparam name="T">The span data type.</typeparam>
        /// <param name="data">The span data.</param>
        void GetData<T>(in Span<T> data)
            where T : unmanaged;

        /// <summary>
        /// Reads the GPU buffer data to a CPU memory span from start element with count elements.
        /// </summary>
        /// <typeparam name="T">The span data type.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="start">The start element index.</param>
        /// <param name="count">The element count to read.</param>
        void GetData<T>(in Span<T> data, int start, int count)
            where T : unmanaged;

        /// <summary>
        /// Reads the GPU buffer data to a CPU memory buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="size">The size of buffer in bytes.</param>
        void GetData(IntPtr buffer, int size);

        /// <summary>
        /// Writes the GPU buffer data from a CPU memory buffer.
        /// </summary>
        /// <param name="buffer">The CPU buffer.</param>
        /// <param name="size">The data buffer size in bytes .</param>
        /// <param name="elementSize">Size of the structured elements or zero if unknown.</param>
        /// <param name="stride">The stride.</param>
        void SetData(IntPtr buffer, int size, int elementSize = 0, int stride = 0);

        /// <summary>
        /// Writes the GPU buffer data from the specified data span.
        /// </summary>
        /// <typeparam name="T">The array element data type.</typeparam>
        /// <param name="data">The array data elements.</param>
        void SetData<T>(in Span<T> data)
            where T : unmanaged;

        /// <summary>
        /// Writes the GPU buffer data from the specified span and range.
        /// </summary>
        /// <typeparam name="T">The array element data type.</typeparam>
        /// <param name="data">The array data elements.</param>
        /// <param name="start">The start index for copy from the array.</param>
        /// <param name="count">The count of elements to copy.</param>
        void SetData<T>(in Span<T> data, int start, int count)
            where T : unmanaged;

        /// <summary>
        /// Writes the GPU buffer data from the specified array.
        /// </summary>
        /// <typeparam name="T">The array element data type.</typeparam>
        /// <param name="data">The array data elements.</param>
        void SetData<T>(T[] data)
            where T : unmanaged;

        /// <summary>
        /// Writes the GPU buffer data from the specified array and range.
        /// </summary>
        /// <typeparam name="T">The array element data type.</typeparam>
        /// <param name="data">The array data elements.</param>
        /// <param name="start">The start index for copy from the array.</param>
        /// <param name="count">The count of elements to copy.</param>
        void SetData<T>(T[] data, int start, int count)
            where T : unmanaged;
    }
}
