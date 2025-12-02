//-----------------------------------------------------------------------
// <copyright file="PixelFormatDescriptor.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace Quasar.Windows.Interop.Gdi32
{
    /// <summary>
    /// Pixel format descriptor structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct PixelFormatDescriptor
    {
        /// <summary>
        /// The size.
        /// </summary>
        public ushort Size;

        /// <summary>
        /// The version.
        /// </summary>
        public ushort Version;

        /// <summary>
        /// The flags.
        /// </summary>
        public PixelFormatFlags Flags;

        /// <summary>
        /// The pixel type.
        /// </summary>
        public PixelType PixelType;

        /// <summary>
        /// The color bits.
        /// </summary>
        public byte ColorBits;

        /// <summary>
        /// The red bits.
        /// </summary>
        public byte RedBits;

        /// <summary>
        /// The red shift.
        /// </summary>
        public byte RedShift;

        /// <summary>
        /// The green bits.
        /// </summary>
        public byte GreenBits;

        /// <summary>
        /// The green shift.
        /// </summary>
        public byte GreenShift;

        /// <summary>
        /// The blue bits.
        /// </summary>
        public byte BlueBits;

        /// <summary>
        /// The blue shift.
        /// </summary>
        public byte BlueShift;

        /// <summary>
        /// The alpha bits.
        /// </summary>
        public byte AlphaBits;

        /// <summary>
        /// The alpha shift.
        /// </summary>
        public byte AlphaShift;

        /// <summary>
        /// The accumulator bits.
        /// </summary>
        public byte AccumulatorBits;

        /// <summary>
        /// The accumulator red bits.
        /// </summary>
        public byte AccumulatorRedBits;

        /// <summary>
        /// The accumulator green bits.
        /// </summary>
        public byte AccumGreenBits;

        /// <summary>
        /// The accumulator blue bits.
        /// </summary>
        public byte AccumulatorBlueBits;

        /// <summary>
        /// The accumulator alpha bits.
        /// </summary>
        public byte AccumulatorAlphaBits;

        /// <summary>
        /// The depth bits.
        /// </summary>
        public byte DepthBits;

        /// <summary>
        /// The stencil bits.
        /// </summary>
        public byte StencilBits;

        /// <summary>
        /// The aux buffers.
        /// </summary>
        public byte AuxBuffers;

        /// <summary>
        /// The layer type.
        /// </summary>
        public LayerType LayerType;

        /// <summary>
        /// The reserved.
        /// </summary>
        public byte Reserved;

        /// <summary>
        /// The layer mask.
        /// </summary>
        public uint LayerMask;

        /// <summary>
        /// The visible mask.
        /// </summary>
        public uint VisibleMask;

        /// <summary>
        /// The damage mask.
        /// </summary>
        public uint DamageMask;


        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Init()
        {
            Size = (ushort)Marshal.SizeOf<PixelFormatDescriptor>();
        }
    }
}
