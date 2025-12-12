//-----------------------------------------------------------------------
// <copyright file="ALEnums.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.OpenAL.Api
{
    /// <summary>
    /// Buffer format enumeration.
    /// </summary>
    internal enum BufferFormat
    {
        /// <summary>
        /// The mono, 8 bits format.
        /// </summary>
        Mono8Bits = 0x1100,

        /// <summary>
        /// The mono, 16 bits format.
        /// </summary>
        Mono16Bits = 0x1101,

        /// <summary>
        /// The stereo, 8 bits format.
        /// </summary>
        Stereo8Bits = 0x1102,

        /// <summary>
        /// The stereo, 16 bits format.
        /// </summary>
        Stereo16Bits = 0x1103
    }

    /// <summary>
    /// Distance model enumeration.
    /// </summary>
    internal enum DistanceModel
    {
        /// <summary>
        /// The no distance model.
        /// </summary>
        None = 0,

        /// <summary>
        /// The inverse model.
        /// </summary>
        Inverse = 0xD001,

        /// <summary>
        /// The inverse clamped model.
        /// </summary>
        InverseClamped = 0xD002,

        /// <summary>
        /// The linear model.
        /// </summary>
        Linear = 0xD003,

        /// <summary>
        /// The linear clamped model.
        /// </summary>
        LinearClamped = 0xD004,

        /// <summary>
        /// The exponential model.
        /// </summary>
        Exponential = 0xD005,

        /// <summary>
        /// The exponential clamped model.
        /// </summary>
        ExponentialClamped = 0xD006
    }

    /// <summary>
    /// Extension names.
    /// </summary>
    internal enum ExtensionNames
    {
        /// <summary>
        /// The ALC enumeration extension.
        /// </summary>
        ALC_ENUMERATION_EXT,

        /// <summary>
        /// The ALC enumerate all extension.
        /// </summary>
        ALC_ENUMERATE_ALL_EXT = 1
    }

    /// <summary>
    /// Integer type enumeration.
    /// </summary>
    internal enum IntegerType
    {
        /// <summary>
        /// The major version.
        /// </summary>
        MajorVersion = 0x1000,

        /// <summary>
        /// The minor version.
        /// </summary>
        MinorVersion = 0x1001,

        /// <summary>
        /// The attributes size.
        /// </summary>
        AttributesSize = 0x1002,

        /// <summary>
        /// All attributes.
        /// </summary>
        AllAttributes = 0x1003
    }

    /// <summary>
    /// Listener property type enumeration.
    /// </summary>
    internal enum ListenerProperty
    {
        /// <summary>
        /// The gain property.
        /// </summary>
        Gain = 0x100A,

        /// <summary>
        /// The orientation property.
        /// </summary>
        Orientation = 0x100F,

        /// <summary>
        /// The position property.
        /// </summary>
        Position = 0x1004,

        /// <summary>
        /// The velocity property.
        /// </summary>
        Velocity = 0x1006
    }


    /// <summary>
    /// AL String type enumeration.
    /// </summary>
    internal enum StringType
    {
        /// <summary>
        /// The extensions.
        /// </summary>
        Extensions = 0xB004,

        /// <summary>
        /// The renderer.
        /// </summary>
        Renderer = 0xB003,

        /// <summary>
        /// The vendor.
        /// </summary>
        Vendor = 0xB001,

        /// <summary>
        /// The version.
        /// </summary>
        Version = 0xB002,
    }

    /// <summary>
    /// ALC String type enumeration.
    /// </summary>
    internal enum StringTypeExt
    {
        /// <summary>
        /// The default device specifier.
        /// </summary>
        DefaultDeviceSpecifier = 0x1004,

        /// <summary>
        /// The device specifier.
        /// </summary>
        DeviceSpecifier = 0x1005,

        /// <summary>
        /// The capture device specifier.
        /// </summary>
        CaptureDeviceSpecifier = 0x310,

        /// <summary>
        /// The default capture device specifier.
        /// </summary>
        DefaultCaptureDeviceSpecifier = 0x311,

        /// <summary>
        /// The extensions.
        /// </summary>
        Extensions = 0x1006,

        /// <summary>
        /// All devices.
        /// </summary>
        AllDeviceSpecifiers = 0x1013
    }

    /// <summary>
    /// Source property type enumeration.
    /// </summary>
    internal enum SourceProperty
    {
        /// <summary>
        /// The buffer property.
        /// </summary>
        Buffer = 0x1009,

        /// <summary>
        /// The direction property.
        /// </summary>
        Direction = 0x1005,

        /// <summary>
        /// The gain property.
        /// </summary>
        Gain = 0x100A,

        /// <summary>
        /// The looping property.
        /// </summary>
        Looping = 0x1007,

        /// <summary>
        /// The maximum distance.
        /// </summary>
        MaximumDistance = 0x1023,

        /// <summary>
        /// The maximum gain property.
        /// </summary>
        MaximumGain = 0x100E,

        /// <summary>
        /// The minimum gain property.
        /// </summary>
        MinimumGain = 0x100D,

        /// <summary>
        /// The pitch property.
        /// </summary>
        Pitch = 0x1003,

        /// <summary>
        /// The position property.
        /// </summary>
        Position = 0x1004,

        /// <summary>
        /// The reference distance.
        /// </summary>
        ReferenceDistance = 0x1020,

        /// <summary>
        /// The rolll-off factor.
        /// </summary>
        RollOfFactor = 0x1021,

        /// <summary>
        /// The velocity property.
        /// </summary>
        Velocity = 0x1006
    }
}
