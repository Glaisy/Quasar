//-----------------------------------------------------------------------
// <copyright file="DevMode.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Runtime.InteropServices;

using Quasar.Graphics;

namespace Quasar.Windows.Interop.User32
{
    /// <summary>
    /// Win32 data structure which contains information about a device mode.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Pack = 1)]
    internal unsafe struct DevMode
    {
        /// <summary>
        /// The device name.
        /// </summary>
        [FieldOffset(0)]
        public fixed byte DeviceName[32];

        /// <summary>
        /// The specification version.
        /// </summary>
        [FieldOffset(32)]
        public short SpecificationVersion;

        /// <summary>
        /// The driver version.
        /// </summary>
        [FieldOffset(34)]
        public short DriverVersion;

        /// <summary>
        /// The size of this structure in bytes.
        /// </summary>
        [FieldOffset(36)]
        public ushort Size;

        /// <summary>
        /// The driver extra bytes.
        /// </summary>
        [FieldOffset(38)]
        public short DriverExtra;

        /// <summary>
        /// The fields.
        /// </summary>
        [FieldOffset(40)]
        public int Fields;

        /// <summary>
        /// The orientation.
        /// </summary>
        [FieldOffset(44)]
        public short Orientation;

        /// <summary>
        /// The paper size.
        /// </summary>
        [FieldOffset(46)]
        public short PaperSize;

        /// <summary>
        /// The paper length.
        /// </summary>
        [FieldOffset(48)]
        public short PaperLength;

        /// <summary>
        /// The paper width.
        /// </summary>
        [FieldOffset(50)]
        public short PaperWidth;

        /// <summary>
        /// The scale.
        /// </summary>
        [FieldOffset(52)]
        public short Scale;

        /// <summary>
        /// The copies.
        /// </summary>
        [FieldOffset(54)]
        public short Copies;

        /// <summary>
        /// The default source.
        /// </summary>
        [FieldOffset(56)]
        public short DefaultSource;

        /// <summary>
        /// The print quality.
        /// </summary>
        [FieldOffset(58)]
        public short PrintQuality;

        /// <summary>
        /// The position.
        /// </summary>
        [FieldOffset(44)]
        public Point Position;

        /// <summary>
        /// The display orientation.
        /// </summary>
        [FieldOffset(52)]
        public int DisplayOrientation;

        /// <summary>
        /// The display fixed output.
        /// </summary>
        [FieldOffset(56)]
        public int DisplayFixedOutput;

        /// <summary>
        /// The color.
        /// </summary>
        [FieldOffset(60)]
        public short Color;

        /// <summary>
        /// The duplex.
        /// </summary>
        [FieldOffset(62)]
        public short Duplex;

        /// <summary>
        /// The y resolution.
        /// </summary>
        [FieldOffset(64)]
        public short YResolution;

        /// <summary>
        /// The tt option.
        /// </summary>
        [FieldOffset(66)]
        public short TTOption;

        /// <summary>
        /// The collate.
        /// </summary>
        [FieldOffset(68)]
        public short Collate;

        /// <summary>
        /// The form name.
        /// </summary>
        [FieldOffset(70)]
        public fixed byte FormName[32];

        /// <summary>
        /// The log pixels.
        /// </summary>
        [FieldOffset(102)]
        public short LogPixels;

        /// <summary>
        /// The bits per pel.
        /// </summary>
        [FieldOffset(104)]
        public int BitsPerPel;

        /// <summary>
        /// The pels width.
        /// </summary>
        [FieldOffset(108)]
        public int PelsWidth;

        /// <summary>
        /// The pels height.
        /// </summary>
        [FieldOffset(112)]
        public int PelsHeight;

        /// <summary>
        /// The display flags.
        /// </summary>
        [FieldOffset(116)]
        public int DisplayFlags;

        /// <summary>
        /// The reserved space.
        /// </summary>
        [FieldOffset(120)]
        public int Reserved;

        /// <summary>
        /// The display frequency.
        /// </summary>
        [FieldOffset(120)]
        public int DisplayFrequency;


        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Init()
        {
            // 124
            Size = (ushort)Marshal.SizeOf<DevMode>();
        }
    }
}
