//-----------------------------------------------------------------------
// <copyright file="GLEnums.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;

namespace Quasar.OpenGL.Api
{
    /// <summary>
    /// Active uniform type enumeration.
    /// </summary>
    internal enum ActiveUniformType
    {
        /// <summary>
        /// Original was GL_INT = 0x1404
        /// </summary>
        Int = 5124,

        /// <summary>
        /// Original was GL_UNSIGNED_INT = 0x1405
        /// </summary>
        UnsignedInt = 5125,

        /// <summary>
        /// Original was GL_FLOAT = 0x1406
        /// </summary>
        Float = 5126,

        /// <summary>
        /// Original was GL_DOUBLE = 0x140A
        /// </summary>
        Double = 5130,

        /// <summary>
        /// Original was GL_FLOAT_VEC2 = 0x8B50
        /// </summary>
        FloatVec2 = 35664,

        /// <summary>
        /// Original was GL_FLOAT_VEC3 = 0x8B51
        /// </summary>
        FloatVec3 = 35665,

        /// <summary>
        /// Original was GL_FLOAT_VEC4 = 0x8B52
        /// </summary>
        FloatVec4 = 35666,

        /// <summary>
        /// Original was GL_INT_VEC2 = 0x8B53
        /// </summary>
        IntVec2 = 35667,

        /// <summary>
        /// Original was GL_INT_VEC3 = 0x8B54
        /// </summary>
        IntVec3 = 35668,

        /// <summary>
        /// Original was GL_INT_VEC4 = 0x8B55
        /// </summary>
        IntVec4 = 35669,

        /// <summary>
        /// Original was GL_BOOL = 0x8B56
        /// </summary>
        Bool = 35670,

        /// <summary>
        /// Original was GL_BOOL_VEC2 = 0x8B57
        /// </summary>
        BoolVec2 = 35671,

        /// <summary>
        /// Original was GL_BOOL_VEC3 = 0x8B58
        /// </summary>
        BoolVec3 = 35672,

        /// <summary>
        /// Original was GL_BOOL_VEC4 = 0x8B59
        /// </summary>
        BoolVec4 = 35673,

        /// <summary>
        /// Original was GL_FLOAT_MAT2 = 0x8B5A
        /// </summary>
        FloatMat2 = 35674,

        /// <summary>
        /// Original was GL_FLOAT_MAT3 = 0x8B5B
        /// </summary>
        FloatMat3 = 35675,

        /// <summary>
        /// Original was GL_FLOAT_MAT4 = 0x8B5C
        /// </summary>
        FloatMat4 = 35676,

        /// <summary>
        /// Original was GL_SAMPLER_1D = 0x8B5D
        /// </summary>
        Sampler1D = 35677,

        /// <summary>
        /// Original was GL_SAMPLER_2D = 0x8B5E
        /// </summary>
        Sampler2D = 35678,

        /// <summary>
        /// Original was GL_SAMPLER_3D = 0x8B5F
        /// </summary>
        Sampler3D = 35679,

        /// <summary>
        /// Original was GL_SAMPLER_CUBE = 0x8B60
        /// </summary>
        SamplerCube = 35680,

        /// <summary>
        /// Original was GL_SAMPLER_1D_SHADOW = 0x8B61
        /// </summary>
        Sampler1DShadow = 35681,

        /// <summary>
        /// Original was GL_SAMPLER_2D_SHADOW = 0x8B62
        /// </summary>
        Sampler2DShadow = 35682,

        /// <summary>
        /// Original was GL_SAMPLER_2D_RECT = 0x8B63
        /// </summary>
        Sampler2DRect = 35683,

        /// <summary>
        /// Original was GL_SAMPLER_2D_RECT_SHADOW = 0x8B64
        /// </summary>
        Sampler2DRectShadow = 35684,

        /// <summary>
        /// Original was GL_FLOAT_MAT2x3 = 0x8B65
        /// </summary>
        FloatMat2x3 = 35685,

        /// <summary>
        /// Original was GL_FLOAT_MAT2x4 = 0x8B66
        /// </summary>
        FloatMat2x4 = 35686,

        /// <summary>
        /// Original was GL_FLOAT_MAT3x2 = 0x8B67
        /// </summary>
        FloatMat3x2 = 35687,

        /// <summary>
        /// Original was GL_FLOAT_MAT3x4 = 0x8B68
        /// </summary>
        FloatMat3x4 = 35688,

        /// <summary>
        /// Original was GL_FLOAT_MAT4x2 = 0x8B69
        /// </summary>
        FloatMat4x2 = 35689,

        /// <summary>
        /// Original was GL_FLOAT_MAT4x3 = 0x8B6A
        /// </summary>
        FloatMat4x3 = 35690,

        /// <summary>
        /// Original was GL_SAMPLER_1D_ARRAY = 0x8DC0
        /// </summary>
        Sampler1DArray = 36288,

        /// <summary>
        /// Original was GL_SAMPLER_2D_ARRAY = 0x8DC1
        /// </summary>
        Sampler2DArray = 36289,

        /// <summary>
        /// Original was GL_SAMPLER_BUFFER = 0x8DC2
        /// </summary>
        SamplerBuffer = 36290,

        /// <summary>
        /// Original was GL_SAMPLER_1D_ARRAY_SHADOW = 0x8DC3
        /// </summary>
        Sampler1DArrayShadow = 36291,

        /// <summary>
        /// Original was GL_SAMPLER_2D_ARRAY_SHADOW = 0x8DC4
        /// </summary>
        Sampler2DArrayShadow = 36292,

        /// <summary>
        /// Original was GL_SAMPLER_CUBE_SHADOW = 0x8DC5
        /// </summary>
        SamplerCubeShadow = 36293,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_VEC2 = 0x8DC6
        /// </summary>
        UnsignedIntVec2 = 36294,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_VEC3 = 0x8DC7
        /// </summary>
        UnsignedIntVec3 = 36295,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_VEC4 = 0x8DC8
        /// </summary>
        UnsignedIntVec4 = 36296,

        /// <summary>
        /// Original was GL_INT_SAMPLER_1D = 0x8DC9
        /// </summary>
        IntSampler1D = 36297,

        /// <summary>
        /// Original was GL_INT_SAMPLER_2D = 0x8DCA
        /// </summary>
        IntSampler2D = 36298,

        /// <summary>
        /// Original was GL_INT_SAMPLER_3D = 0x8DCB
        /// </summary>
        IntSampler3D = 36299,

        /// <summary>
        /// Original was GL_INT_SAMPLER_CUBE = 0x8DCC
        /// </summary>
        IntSamplerCube = 36300,

        /// <summary>
        /// Original was GL_INT_SAMPLER_2D_RECT = 0x8DCD
        /// </summary>
        IntSampler2DRect = 36301,

        /// <summary>
        /// Original was GL_INT_SAMPLER_1D_ARRAY = 0x8DCE
        /// </summary>
        IntSampler1DArray = 36302,

        /// <summary>
        /// Original was GL_INT_SAMPLER_2D_ARRAY = 0x8DCF
        /// </summary>
        IntSampler2DArray = 36303,

        /// <summary>
        /// Original was GL_INT_SAMPLER_BUFFER = 0x8DD0
        /// </summary>
        IntSamplerBuffer = 36304,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_SAMPLER_1D = 0x8DD1
        /// </summary>
        UnsignedIntSampler1D = 36305,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_SAMPLER_2D = 0x8DD2
        /// </summary>
        UnsignedIntSampler2D = 36306,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_SAMPLER_3D = 0x8DD3
        /// </summary>
        UnsignedIntSampler3D = 36307,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_SAMPLER_CUBE = 0x8DD4
        /// </summary>
        UnsignedIntSamplerCube = 36308,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_SAMPLER_2D_RECT = 0x8DD5
        /// </summary>
        UnsignedIntSampler2DRect = 36309,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_SAMPLER_1D_ARRAY = 0x8DD6
        /// </summary>
        UnsignedIntSampler1DArray = 36310,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_SAMPLER_2D_ARRAY = 0x8DD7
        /// </summary>
        UnsignedIntSampler2DArray = 36311,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_SAMPLER_BUFFER = 0x8DD8
        /// </summary>
        UnsignedIntSamplerBuffer = 36312,

        /// <summary>
        /// Original was GL_DOUBLE_VEC2 = 0x8FFC
        /// </summary>
        DoubleVec2 = 36860,

        /// <summary>
        /// Original was GL_DOUBLE_VEC3 = 0x8FFD
        /// </summary>
        DoubleVec3 = 36861,

        /// <summary>
        /// Original was GL_DOUBLE_VEC4 = 0x8FFE
        /// </summary>
        DoubleVec4 = 36862,

        /// <summary>
        /// Original was GL_SAMPLER_CUBE_MAP_ARRAY = 0x900C
        /// </summary>
        SamplerCubeMapArray = 36876,

        /// <summary>
        /// Original was GL_SAMPLER_CUBE_MAP_ARRAY_SHADOW = 0x900D
        /// </summary>
        SamplerCubeMapArrayShadow = 36877,

        /// <summary>
        /// Original was GL_INT_SAMPLER_CUBE_MAP_ARRAY = 0x900E
        /// </summary>
        IntSamplerCubeMapArray = 36878,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY = 0x900F
        /// </summary>
        UnsignedIntSamplerCubeMapArray = 36879,

        /// <summary>
        /// Original was GL_IMAGE_1D = 0x904C
        /// </summary>
        Image1D = 36940,

        /// <summary>
        /// Original was GL_IMAGE_2D = 0x904D
        /// </summary>
        Image2D = 36941,

        /// <summary>
        /// Original was GL_IMAGE_3D = 0x904E
        /// </summary>
        Image3D = 36942,

        /// <summary>
        /// Original was GL_IMAGE_2D_RECT = 0x904F
        /// </summary>
        Image2DRect = 36943,

        /// <summary>
        /// Original was GL_IMAGE_CUBE = 0x9050
        /// </summary>
        ImageCube = 36944,

        /// <summary>
        /// Original was GL_IMAGE_BUFFER = 0x9051
        /// </summary>
        ImageBuffer = 36945,

        /// <summary>
        /// Original was GL_IMAGE_1D_ARRAY = 0x9052
        /// </summary>
        Image1DArray = 36946,

        /// <summary>
        /// Original was GL_IMAGE_2D_ARRAY = 0x9053
        /// </summary>
        Image2DArray = 36947,

        /// <summary>
        /// Original was GL_IMAGE_CUBE_MAP_ARRAY = 0x9054
        /// </summary>
        ImageCubeMapArray = 36948,

        /// <summary>
        /// Original was GL_IMAGE_2D_MULTISAMPLE = 0x9055
        /// </summary>
        Image2DMultisample = 36949,

        /// <summary>
        /// Original was GL_IMAGE_2D_MULTISAMPLE_ARRAY = 0x9056
        /// </summary>
        Image2DMultisampleArray = 36950,

        /// <summary>
        /// Original was GL_INT_IMAGE_1D = 0x9057
        /// </summary>
        IntImage1D = 36951,

        /// <summary>
        /// Original was GL_INT_IMAGE_2D = 0x9058
        /// </summary>
        IntImage2D = 36952,

        /// <summary>
        /// Original was GL_INT_IMAGE_3D = 0x9059
        /// </summary>
        IntImage3D = 36953,

        /// <summary>
        /// Original was GL_INT_IMAGE_2D_RECT = 0x905A
        /// </summary>
        IntImage2DRect = 36954,

        /// <summary>
        /// Original was GL_INT_IMAGE_CUBE = 0x905B
        /// </summary>
        IntImageCube = 36955,

        /// <summary>
        /// Original was GL_INT_IMAGE_BUFFER = 0x905C
        /// </summary>
        IntImageBuffer = 36956,

        /// <summary>
        /// Original was GL_INT_IMAGE_1D_ARRAY = 0x905D
        /// </summary>
        IntImage1DArray = 36957,

        /// <summary>
        /// Original was GL_INT_IMAGE_2D_ARRAY = 0x905E
        /// </summary>
        IntImage2DArray = 36958,

        /// <summary>
        /// Original was GL_INT_IMAGE_CUBE_MAP_ARRAY = 0x905F
        /// </summary>
        IntImageCubeMapArray = 36959,

        /// <summary>
        /// Original was GL_INT_IMAGE_2D_MULTISAMPLE = 0x9060
        /// </summary>
        IntImage2DMultisample = 36960,

        /// <summary>
        /// Original was GL_INT_IMAGE_2D_MULTISAMPLE_ARRAY = 0x9061
        /// </summary>
        IntImage2DMultisampleArray = 36961,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_IMAGE_1D = 0x9062
        /// </summary>
        UnsignedIntImage1D = 36962,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_IMAGE_2D = 0x9063
        /// </summary>
        UnsignedIntImage2D = 36963,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_IMAGE_3D = 0x9064
        /// </summary>
        UnsignedIntImage3D = 36964,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_IMAGE_2D_RECT = 0x9065
        /// </summary>
        UnsignedIntImage2DRect = 36965,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_IMAGE_CUBE = 0x9066
        /// </summary>
        UnsignedIntImageCube = 36966,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_IMAGE_BUFFER = 0x9067
        /// </summary>
        UnsignedIntImageBuffer = 36967,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_IMAGE_1D_ARRAY = 0x9068
        /// </summary>
        UnsignedIntImage1DArray = 36968,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_IMAGE_2D_ARRAY = 0x9069
        /// </summary>
        UnsignedIntImage2DArray = 36969,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_IMAGE_CUBE_MAP_ARRAY = 0x906A
        /// </summary>
        UnsignedIntImageCubeMapArray = 36970,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE = 0x906B
        /// </summary>
        UnsignedIntImage2DMultisample = 36971,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_IMAGE_2D_MULTISAMPLE_ARRAY = 0x906C
        /// </summary>
        UnsignedIntImage2DMultisampleArray = 36972,

        /// <summary>
        /// Original was GL_SAMPLER_2D_MULTISAMPLE = 0x9108
        /// </summary>
        Sampler2DMultisample = 37128,

        /// <summary>
        /// Original was GL_INT_SAMPLER_2D_MULTISAMPLE = 0x9109
        /// </summary>
        IntSampler2DMultisample = 37129,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE = 0x910A
        /// </summary>
        UnsignedIntSampler2DMultisample = 37130,

        /// <summary>
        /// Original was GL_SAMPLER_2D_MULTISAMPLE_ARRAY = 0x910B
        /// </summary>
        Sampler2DMultisampleArray = 37131,

        /// <summary>
        /// Original was GL_INT_SAMPLER_2D_MULTISAMPLE_ARRAY = 0x910C
        /// </summary>
        IntSampler2DMultisampleArray = 37132,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_SAMPLER_2D_MULTISAMPLE_ARRAY = 0x910D
        /// </summary>
        UnsignedIntSampler2DMultisampleArray = 37133,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_ATOMIC_COUNTER = 0x92DB
        /// </summary>
        UnsignedIntAtomicCounter = 37595
    }

    /// <summary>
    /// Draw begin mode enumeration.
    /// </summary>
    internal enum BeginMode
    {
        /// <summary>
        /// Original was GL_POINTS = 0x0000
        /// </summary>
        Points = 0x0000,

        /// <summary>
        /// Original was GL_LINES = 0x0001
        /// </summary>
        Lines = 0x0001,

        /// <summary>
        /// Original was GL_LINE_LOOP = 0x0002
        /// </summary>
        LineLoop = 0x0002,

        /// <summary>
        /// Original was GL_LINE_STRIP = 0x0003
        /// </summary>
        LineStrip = 0x0003,

        /// <summary>
        /// Original was GL_TRIANGLES = 0x0004
        /// </summary>
        Triangles = 0x0004,

        /// <summary>
        /// Original was GL_TRIANGLE_STRIP = 0x0005
        /// </summary>
        TriangleStrip = 0x0005,

        /// <summary>
        /// Original was GL_TRIANGLE_FAN = 0x0006
        /// </summary>
        TriangleFan = 0x0006,

        /// <summary>
        /// Original was GL_QUADS = 0x0007
        /// </summary>
        Quads = 0x0007,

        /// <summary>
        /// Original was GL_QUAD_STRIP = 0x0008
        /// </summary>
        QuadStrip = 0x0008,

        /// <summary>
        /// Original was GL_POLYGON = 0x0009
        /// </summary>
        Polygon = 0x0009,

        /// <summary>
        /// Original was GL_LINES_ADJACENCY = 0xA
        /// </summary>
        LinesAdjacency = 0xA,

        /// <summary>
        /// Original was GL_LINE_STRIP_ADJACENCY = 0xB
        /// </summary>
        LineStripAdjacency = 0xB,

        /// <summary>
        /// Original was GL_TRIANGLES_ADJACENCY = 0xC
        /// </summary>
        TrianglesAdjacency = 0xC,

        /// <summary>
        /// Original was GL_TRIANGLE_STRIP_ADJACENCY = 0xD
        /// </summary>
        TriangleStripAdjacency = 0xD,

        /// <summary>
        /// Original was GL_PATCHES = 0x000E
        /// </summary>
        Patches = 0x000E
    }

    /// <summary>
    /// Blend factor enumeration.
    /// </summary>
    internal enum BlendingFactor
    {
        /// <summary>
        /// Original was GL_ZERO = 0
        /// </summary>
        Zero = 0,

        /// <summary>
        /// Original was GL_ONE = 1
        /// </summary>
        One = 1,

        /// <summary>
        /// Original was GL_SRC_COLOR = 0x0300
        /// </summary>
        SrcColor = 0x0300,

        /// <summary>
        /// Original was GL_ONE_MINUS_SRC_COLOR = 0x0301
        /// </summary>
        OneMinusSrcColor = 0x0301,

        /// <summary>
        /// Original was GL_SRC_ALPHA = 0x0302
        /// </summary>
        SrcAlpha = 0x0302,

        /// <summary>
        /// Original was GL_ONE_MINUS_SRC_ALPHA = 0x0303
        /// </summary>
        OneMinusSrcAlpha = 0x0303,

        /// <summary>
        /// Original was GL_DST_ALPHA = 0x0304
        /// </summary>
        DstAlpha = 0x0304,

        /// <summary>
        /// Original was GL_ONE_MINUS_DST_ALPHA = 0x0305
        /// </summary>
        OneMinusDstAlpha = 0x0305,

        /// <summary>
        /// Original was GL_DST_COLOR = 0x0306
        /// </summary>
        DstColor = 0x0306,

        /// <summary>
        /// Original was GL_ONE_MINUS_DST_COLOR = 0x0307
        /// </summary>
        OneMinusDstColor = 0x0307,

        /// <summary>
        /// Original was GL_SRC_ALPHA_SATURATE = 0x0308
        /// </summary>
        SrcAlphaSaturate = 0x0308,

        /// <summary>
        /// Original was GL_CONSTANT_COLOR = 0x8001
        /// </summary>
        ConstantColor = 0x8001,

        /// <summary>
        /// Original was GL_ONE_MINUS_CONSTANT_COLOR = 0x8002
        /// </summary>
        OneMinusConstantColor = 0x8002,

        /// <summary>
        /// Original was GL_CONSTANT_ALPHA = 0x8003
        /// </summary>
        ConstantAlpha = 0x8003,

        /// <summary>
        /// Original was GL_ONE_MINUS_CONSTANT_ALPHA = 0x8004
        /// </summary>
        OneMinusConstantAlpha = 0x8004,

        /// <summary>
        /// Original was GL_SRC1_ALPHA = 0x8589
        /// </summary>
        Src1Alpha = 0x8589,

        /// <summary>
        /// Original was GL_SRC1_COLOR = 0x88F9
        /// </summary>
        Src1Color = 0x88F9
    }

    /// <summary>
    /// Buffer clear mask enumeration.
    /// </summary>
    [Flags]
    internal enum BufferClearMask : uint
    {
        /// <summary>
        /// The none bit (GL_NONE = 0).
        /// </summary>
        None = 0,

        /// <summary>
        /// The depth buffer bit (GL_DEPTH_BUFFER_BIT = 0x0100)
        /// </summary>
        DepthBuffer = 0x0100,

        /// <summary>
        /// The accumulation buffer bit (GL_ACCUM_BUFFER_BIT = 0x0200)
        /// </summary>
        AccumulationBuffer = 0x0200,

        /// <summary>
        /// The stencil buffer bit (GL_STENCIL_BUFFER_BIT = 0x0400)
        /// </summary>
        StencilBuffer = 0x0400,

        /// <summary>
        /// The stencil buffer bit (GL_COLOR_BUFFER_BIT = 0x4000)
        /// </summary>
        ColorBuffer = 0x4000,

        /// <summary>
        /// The coverage buffer bit (GL_COVERAGE_BUFFER_BIT_NV = 0x8000)
        /// </summary>
        CoverageBuffer = 0x8000
    }

    /// <summary>
    /// Buffer target enumeration.
    /// </summary>
    internal enum BufferTarget
    {
        /// <summary>
        /// Original was GL_ARRAY_BUFFER = 0x8892
        /// </summary>
        ArrayBuffer = 0x8892,

        /// <summary>
        /// Original was GL_ELEMENT_ARRAY_BUFFER = 0x8893
        /// </summary>
        ElementArrayBuffer = 0x8893,

        /// <summary>
        /// Original was GL_PIXEL_PACK_BUFFER = 0x88EB
        /// </summary>
        PixelPackBuffer = 0x88EB,

        /// <summary>
        /// Original was GL_PIXEL_UNPACK_BUFFER = 0x88EC
        /// </summary>
        PixelUnpackBuffer = 0x88EC,

        /// <summary>
        /// Original was GL_UNIFORM_BUFFER = 0x8A11
        /// </summary>
        UniformBuffer = 0x8A11,

        /// <summary>
        /// Original was GL_TEXTURE_BUFFER = 0x8C2A
        /// </summary>
        TextureBuffer = 0x8C2A,

        /// <summary>
        /// Original was GL_TRANSFORM_FEEDBACK_BUFFER = 0x8C8E
        /// </summary>
        TransformFeedbackBuffer = 0x8C8E,

        /// <summary>
        /// Original was GL_COPY_READ_BUFFER = 0x8F36
        /// </summary>
        CopyReadBuffer = 0x8F36,

        /// <summary>
        /// Original was GL_COPY_WRITE_BUFFER = 0x8F37
        /// </summary>
        CopyWriteBuffer = 0x8F37,

        /// <summary>
        /// Original was GL_DRAW_INDIRECT_BUFFER = 0x8F3F
        /// </summary>
        DrawIndirectBuffer = 0x8F3F,

        /// <summary>
        /// Original was GL_SHADER_STORAGE_BUFFER = 0x90D2
        /// </summary>
        ShaderStorageBuffer = 0x90D2,

        /// <summary>
        /// Original was GL_DISPATCH_INDIRECT_BUFFER = 0x90EE
        /// </summary>
        DispatchIndirectBuffer = 0x90EE,

        /// <summary>
        /// Original was GL_QUERY_BUFFER = 0x9192
        /// </summary>
        QueryBuffer = 0x9192,

        /// <summary>
        /// Original was GL_ATOMIC_COUNTER_BUFFER = 0x92C0
        /// </summary>
        AtomicCounterBuffer = 0x92C0
    }

    /// <summary>
    /// Buffer usage hint enumeration.
    /// </summary>
    internal enum BufferUsageHint
    {
        /// <summary>
        /// Original was GL_STREAM_DRAW = 0x88E0
        /// </summary>
        StreamDraw = 0x88E0,

        /// <summary>
        /// Original was GL_STREAM_READ = 0x88E1
        /// </summary>
        StreamRead = 0x88E1,

        /// <summary>
        /// Original was GL_STREAM_COPY = 0x88E2
        /// </summary>
        StreamCopy = 0x88E2,

        /// <summary>
        /// Original was GL_STATIC_DRAW = 0x88E4
        /// </summary>
        StaticDraw = 0x88E4,

        /// <summary>
        /// Original was GL_STATIC_READ = 0x88E5
        /// </summary>
        StaticRead = 0x88E5,

        /// <summary>
        /// Original was GL_STATIC_COPY = 0x88E6
        /// </summary>
        StaticCopy = 0x88E6,

        /// <summary>
        /// Original was GL_DYNAMIC_DRAW = 0x88E8
        /// </summary>
        DynamicDraw = 0x88E8,

        /// <summary>
        /// Original was GL_DYNAMIC_READ = 0x88E9
        /// </summary>
        DynamicRead = 0x88E9,

        /// <summary>
        /// Original was GL_DYNAMIC_COPY = 0x88EA
        /// </summary>
        DynamicCopy = 0x88EA
    }

    /// <summary>
    /// The OpenGL capability enumeration.
    /// </summary>
    internal enum Capability
    {
        /// <summary>
        /// Original was GL_LINE_SMOOTH = 0x0B20
        /// </summary>
        LineSmooth = 0x0B20,

        /// <summary>
        /// Original was GL_POLYGON_SMOOTH = 0x0B41
        /// </summary>
        PolygonSmooth = 0x0B41,

        /// <summary>
        /// Original was GL_CULL_FACE = 0x0B44
        /// </summary>
        CullFace = 0x0B44,

        /// <summary>
        /// Original was GL_DEPTH_TEST = 0x0B71
        /// </summary>
        DepthTest = 0x0B71,

        /// <summary>
        /// Original was GL_STENCIL_TEST = 0x0B90
        /// </summary>
        StencilTest = 0x0B90,

        /// <summary>
        /// Original was GL_DITHER = 0x0BD0
        /// </summary>
        Dither = 0x0BD0,

        /// <summary>
        /// Original was GL_BLEND = 0x0BE2
        /// </summary>
        Blend = 0x0BE2,

        /// <summary>
        /// Original was GL_COLOR_LOGIC_OP = 0x0BF2
        /// </summary>
        ColorLogicOp = 0x0BF2,

        /// <summary>
        /// Original was GL_SCISSOR_TEST = 0x0C11
        /// </summary>
        ScissorTest = 0x0C11,

        /// <summary>
        /// Original was GL_TEXTURE_1D = 0x0DE0
        /// </summary>
        Texture1D = 0x0DE0,

        /// <summary>
        /// Original was GL_TEXTURE_2D = 0x0DE1
        /// </summary>
        Texture2D = 0x0DE1,

        /// <summary>
        /// Original was GL_POLYGON_OFFSET_POINT = 0x2A01
        /// </summary>
        PolygonOffsetPoint = 0x2A01,

        /// <summary>
        /// Original was GL_POLYGON_OFFSET_LINE = 0x2A02
        /// </summary>
        PolygonOffsetLine = 0x2A02,

        /// <summary>
        /// Original was GL_CLIP_DISTANCE0 = 0x3000
        /// </summary>
        ClipDistance0 = 0x3000,

        /// <summary>
        /// Original was GL_CLIP_PLANE0 = 0x3000
        /// </summary>
        ClipPlane0 = 0x3000,

        /// <summary>
        /// Original was GL_CLIP_DISTANCE1 = 0x3001
        /// </summary>
        ClipDistance1 = 0x3001,

        /// <summary>
        /// Original was GL_CLIP_PLANE1 = 0x3001
        /// </summary>
        ClipPlane1 = 0x3001,

        /// <summary>
        /// Original was GL_CLIP_DISTANCE2 = 0x3002
        /// </summary>
        ClipDistance2 = 0x3002,

        /// <summary>
        /// Original was GL_CLIP_PLANE2 = 0x3002
        /// </summary>
        ClipPlane2 = 0x3002,

        /// <summary>
        /// Original was GL_CLIP_DISTANCE3 = 0x3003
        /// </summary>
        ClipDistance3 = 0x3003,

        /// <summary>
        /// Original was GL_CLIP_PLANE3 = 0x3003
        /// </summary>
        ClipPlane3 = 0x3003,

        /// <summary>
        /// Original was GL_CLIP_DISTANCE4 = 0x3004
        /// </summary>
        ClipDistance4 = 0x3004,

        /// <summary>
        /// Original was GL_CLIP_PLANE4 = 0x3004
        /// </summary>
        ClipPlane4 = 0x3004,

        /// <summary>
        /// Original was GL_CLIP_DISTANCE5 = 0x3005
        /// </summary>
        ClipDistance5 = 0x3005,

        /// <summary>
        /// Original was GL_CLIP_PLANE5 = 0x3005
        /// </summary>
        ClipPlane5 = 0x3005,

        /// <summary>
        /// Original was GL_CLIP_DISTANCE6 = 0x3006
        /// </summary>
        ClipDistance6 = 0x3006,

        /// <summary>
        /// Original was GL_CLIP_DISTANCE7 = 0x3007
        /// </summary>
        ClipDistance7 = 0x3007,

        /// <summary>
        /// Original was GL_CONVOLUTION_1D = 0x8010
        /// </summary>
        Convolution1D = 0x8010,

        /// <summary>
        /// Original was GL_CONVOLUTION_1D_EXT = 0x8010
        /// </summary>
        Convolution1DExt = 0x8010,

        /// <summary>
        /// Original was GL_CONVOLUTION_2D = 0x8011
        /// </summary>
        Convolution2D = 0x8011,

        /// <summary>
        /// Original was GL_CONVOLUTION_2D_EXT = 0x8011
        /// </summary>
        Convolution2DExt = 0x8011,

        /// <summary>
        /// Original was GL_SEPARABLE_2D = 0x8012
        /// </summary>
        Separable2D = 0x8012,

        /// <summary>
        /// Original was GL_SEPARABLE_2D_EXT = 0x8012
        /// </summary>
        Separable2DExt = 0x8012,

        /// <summary>
        /// Original was GL_HISTOGRAM = 0x8024
        /// </summary>
        Histogram = 0x8024,

        /// <summary>
        /// Original was GL_HISTOGRAM_EXT = 0x8024
        /// </summary>
        HistogramExt = 0x8024,

        /// <summary>
        /// Original was GL_MINMAX_EXT = 0x802E
        /// </summary>
        MinmaxExt = 0x802E,

        /// <summary>
        /// Original was GL_POLYGON_OFFSET_FILL = 0x8037
        /// </summary>
        PolygonOffsetFill = 0x8037,

        /// <summary>
        /// Original was GL_RESCALE_NORMAL = 0x803A
        /// </summary>
        RescaleNormal = 0x803A,

        /// <summary>
        /// Original was GL_RESCALE_NORMAL_EXT = 0x803A
        /// </summary>
        RescaleNormalExt = 0x803A,

        /// <summary>
        /// Original was GL_TEXTURE_3D_EXT = 0x806F
        /// </summary>
        Texture3DExt = 0x806F,

        /// <summary>
        /// Original was GL_INTERLACE_SGIX = 0x8094
        /// </summary>
        InterlaceSgix = 0x8094,

        /// <summary>
        /// Original was GL_MULTISAMPLE = 0x809D
        /// </summary>
        Multisample = 0x809D,

        /// <summary>
        /// Original was GL_MULTISAMPLE_SGIS = 0x809D
        /// </summary>
        MultisampleSgis = 0x809D,

        /// <summary>
        /// Original was GL_SAMPLE_ALPHA_TO_COVERAGE = 0x809E
        /// </summary>
        SampleAlphaToCoverage = 0x809E,

        /// <summary>
        /// Original was GL_SAMPLE_ALPHA_TO_MASK_SGIS = 0x809E
        /// </summary>
        SampleAlphaToMaskSgis = 0x809E,

        /// <summary>
        /// Original was GL_SAMPLE_ALPHA_TO_ONE = 0x809F
        /// </summary>
        SampleAlphaToOne = 0x809F,

        /// <summary>
        /// Original was GL_SAMPLE_ALPHA_TO_ONE_SGIS = 0x809F
        /// </summary>
        SampleAlphaToOneSgis = 0x809F,

        /// <summary>
        /// Original was GL_SAMPLE_COVERAGE = 0x80A0
        /// </summary>
        SampleCoverage = 0x80A0,

        /// <summary>
        /// Original was GL_SAMPLE_MASK_SGIS = 0x80A0
        /// </summary>
        SampleMaskSgis = 0x80A0,

        /// <summary>
        /// Original was GL_TEXTURE_COLOR_TABLE_SGI = 0x80BC
        /// </summary>
        TextureColorTableSgi = 0x80BC,

        /// <summary>
        /// Original was GL_COLOR_TABLE = 0x80D0
        /// </summary>
        ColorTable = 0x80D0,

        /// <summary>
        /// Original was GL_COLOR_TABLE_SGI = 0x80D0
        /// </summary>
        ColorTableSgi = 0x80D0,

        /// <summary>
        /// Original was GL_POST_CONVOLUTION_COLOR_TABLE = 0x80D1
        /// </summary>
        PostConvolutionColorTable = 0x80D1,

        /// <summary>
        /// Original was GL_POST_CONVOLUTION_COLOR_TABLE_SGI = 0x80D1
        /// </summary>
        PostConvolutionColorTableSgi = 0x80D1,

        /// <summary>
        /// Original was GL_POST_COLOR_MATRIX_COLOR_TABLE = 0x80D2
        /// </summary>
        PostColorMatrixColorTable = 0x80D2,

        /// <summary>
        /// Original was GL_POST_COLOR_MATRIX_COLOR_TABLE_SGI = 0x80D2
        /// </summary>
        PostColorMatrixColorTableSgi = 0x80D2,

        /// <summary>
        /// Original was GL_TEXTURE_4D_SGIS = 0x8134
        /// </summary>
        Texture4DSgis = 0x8134,

        /// <summary>
        /// Original was GL_PIXEL_TEX_GEN_SGIX = 0x8139
        /// </summary>
        PixelTexGenSgix = 0x8139,

        /// <summary>
        /// Original was GL_SPRITE_SGIX = 0x8148
        /// </summary>
        SpriteSgix = 0x8148,

        /// <summary>
        /// Original was GL_REFERENCE_PLANE_SGIX = 0x817D
        /// </summary>
        ReferencePlaneSgix = 0x817D,

        /// <summary>
        /// Original was GL_IR_INSTRUMENT1_SGIX = 0x817F
        /// </summary>
        IrInstrument1Sgix = 0x817F,

        /// <summary>
        /// Original was GL_CALLIGRAPHIC_FRAGMENT_SGIX = 0x8183
        /// </summary>
        CalligraphicFragmentSgix = 0x8183,

        /// <summary>
        /// Original was GL_FRAMEZOOM_SGIX = 0x818B
        /// </summary>
        FramezoomSgix = 0x818B,

        /// <summary>
        /// Original was GL_FOG_OFFSET_SGIX = 0x8198
        /// </summary>
        FogOffsetSgix = 0x8198,

        /// <summary>
        /// Original was GL_SHARED_TEXTURE_PALETTE_EXT = 0x81FB
        /// </summary>
        SharedTexturePaletteExt = 0x81FB,

        /// <summary>
        /// Original was GL_DEBUG_OUTPUT_SYNCHRONOUS = 0x8242
        /// </summary>
        DebugOutputSynchronous = 0x8242,

        /// <summary>
        /// Original was GL_ASYNC_HISTOGRAM_SGIX = 0x832C
        /// </summary>
        AsyncHistogramSgix = 0x832C,

        /// <summary>
        /// Original was GL_PIXEL_TEXTURE_SGIS = 0x8353
        /// </summary>
        PixelTextureSgis = 0x8353,

        /// <summary>
        /// Original was GL_ASYNC_TEX_IMAGE_SGIX = 0x835C
        /// </summary>
        AsyncTexImageSgix = 0x835C,

        /// <summary>
        /// Original was GL_ASYNC_DRAW_PIXELS_SGIX = 0x835D
        /// </summary>
        AsyncDrawPixelsSgix = 0x835D,

        /// <summary>
        /// Original was GL_ASYNC_READ_PIXELS_SGIX = 0x835E
        /// </summary>
        AsyncReadPixelsSgix = 0x835E,

        /// <summary>
        /// Original was GL_FRAGMENT_LIGHTING_SGIX = 0x8400
        /// </summary>
        FragmentLightingSgix = 0x8400,

        /// <summary>
        /// Original was GL_FRAGMENT_COLOR_MATERIAL_SGIX = 0x8401
        /// </summary>
        FragmentColorMaterialSgix = 0x8401,

        /// <summary>
        /// Original was GL_FRAGMENT_LIGHT0_SGIX = 0x840C
        /// </summary>
        FragmentLight0Sgix = 0x840C,

        /// <summary>
        /// Original was GL_FRAGMENT_LIGHT1_SGIX = 0x840D
        /// </summary>
        FragmentLight1Sgix = 0x840D,

        /// <summary>
        /// Original was GL_FRAGMENT_LIGHT2_SGIX = 0x840E
        /// </summary>
        FragmentLight2Sgix = 0x840E,

        /// <summary>
        /// Original was GL_FRAGMENT_LIGHT3_SGIX = 0x840F
        /// </summary>
        FragmentLight3Sgix = 0x840F,

        /// <summary>
        /// Original was GL_FRAGMENT_LIGHT4_SGIX = 0x8410
        /// </summary>
        FragmentLight4Sgix = 0x8410,

        /// <summary>
        /// Original was GL_FRAGMENT_LIGHT5_SGIX = 0x8411
        /// </summary>
        FragmentLight5Sgix = 0x8411,

        /// <summary>
        /// Original was GL_FRAGMENT_LIGHT6_SGIX = 0x8412
        /// </summary>
        FragmentLight6Sgix = 0x8412,

        /// <summary>
        /// Original was GL_FRAGMENT_LIGHT7_SGIX = 0x8413
        /// </summary>
        FragmentLight7Sgix = 0x8413,

        /// <summary>
        /// Original was GL_FOG_COORD_ARRAY = 0x8457
        /// </summary>
        FogCoordArray = 0x8457,

        /// <summary>
        /// Original was GL_COLOR_SUM = 0x8458
        /// </summary>
        ColorSum = 0x8458,

        /// <summary>
        /// Original was GL_SECONDARY_COLOR_ARRAY = 0x845E
        /// </summary>
        SecondaryColorArray = 0x845E,

        /// <summary>
        /// Original was GL_TEXTURE_RECTANGLE = 0x84F5
        /// </summary>
        TextureRectangle = 0x84F5,

        /// <summary>
        /// Original was GL_TEXTURE_CUBE_MAP = 0x8513
        /// </summary>
        TextureCubeMap = 0x8513,

        /// <summary>
        /// Original was GL_PROGRAM_POINT_SIZE = 0x8642
        /// </summary>
        ProgramPointSize = 0x8642,

        /// <summary>
        /// Original was GL_VERTEX_PROGRAM_POINT_SIZE = 0x8642
        /// </summary>
        VertexProgramPointSize = 0x8642,

        /// <summary>
        /// Original was GL_VERTEX_PROGRAM_TWO_SIDE = 0x8643
        /// </summary>
        VertexProgramTwoSide = 0x8643,

        /// <summary>
        /// Original was GL_DEPTH_CLAMP = 0x864F
        /// </summary>
        DepthClamp = 0x864F,

        /// <summary>
        /// Original was GL_TEXTURE_CUBE_MAP_SEAMLESS = 0x884F
        /// </summary>
        TextureCubeMapSeamless = 0x884F,

        /// <summary>
        /// Original was GL_POINT_SPRITE = 0x8861
        /// </summary>
        PointSprite = 0x8861,

        /// <summary>
        /// Original was GL_SAMPLE_SHADING = 0x8C36
        /// </summary>
        SampleShading = 0x8C36,

        /// <summary>
        /// Original was GL_RASTERIZER_DISCARD = 0x8C89
        /// </summary>
        RasterizerDiscard = 0x8C89,

        /// <summary>
        /// Original was GL_PRIMITIVE_RESTART_FIXED_INDEX = 0x8D69
        /// </summary>
        PrimitiveRestartFixedIndex = 0x8D69,

        /// <summary>
        /// Original was GL_FRAMEBUFFER_SRGB = 0x8DB9
        /// </summary>
        FramebufferSrgb = 0x8DB9,

        /// <summary>
        /// Original was GL_SAMPLE_MASK = 0x8E51
        /// </summary>
        SampleMask = 0x8E51,

        /// <summary>
        /// Original was GL_PRIMITIVE_RESTART = 0x8F9D
        /// </summary>
        PrimitiveRestart = 0x8F9D,

        /// <summary>
        /// Original was GL_DEBUG_OUTPUT = 0x92E0
        /// </summary>
        DebugOutput = 0x92E0
    }

    /// <summary>
    /// Render context attribute enumeration.
    /// </summary>
    internal enum ContextAttributes
    {
        /// <summary>
        /// The debug bit. (WGL_CONTEXT_DEBUG_BIT_ARB = 0x0001)
        /// </summary>
        Debug = 0x0001,

        /// <summary>
        /// The forward compatible bit. (WGL_CONTEXT_FORWARD_COMPATIBLE_BIT_ARB = 0x0002)
        /// </summary>
        ForwardCompatible = 0x0002,

        /// <summary>
        /// The major version (WGL_CONTEXT_MAJOR_VERSION_ARB = 0x2091)
        /// </summary>
        MajorVersion = 0x2091,

        /// <summary>
        /// The major version (WGL_CONTEXT_MINOR_VERSION_ARB = 0x2092)
        /// </summary>
        MinorVersion = 0x2092,

        /// <summary>
        /// The context layer plane ARB. (WGL_CONTEXT_LAYER_PLANE_ARB = 0x2093)
        /// </summary>
        ContextLayerPlaneARB = 0x2093,

        /// <summary>
        /// The context flags ARB (WGL_CONTEXT_FLAGS_ARB = 0x2094)
        /// </summary>
        ContextFlagsARB = 0x2094,

        /// <summary>
        /// The invalid version ARB. (ERROR_INVALID_VERSION_ARB = 0x2095)
        /// </summary>
        InvalidVersionARB = 0x2095,

        /// <summary>
        /// The invalid profle ARB. (ERROR_INVALID_PROFILE_ARB = 0x2096)
        /// </summary>
        InvalidProfleARB = 0x2096,

        /// <summary>
        /// The context profile mask ARB. (WGL_CONTEXT_PROFILE_MASK_ARB = 0x9126)
        /// </summary>
        ContextProfileMaskARB = 0x9126
    }

    /// <summary>
    /// Cull face enumeration.
    /// </summary>
    internal enum CullFaceMode
    {
        /// <summary>
        ///  Original was GL_FRONT = 0x0404
        /// </summary>
        Front = 0x0404,

        /// <summary>
        /// Original was GL_BACK = 0x0405
        /// </summary>
        Back = 0x0405,

        /// <summary>
        /// Original was GL_FRONT_AND_BACK = 0x0408
        /// </summary>
        FrontAndBack = 0x0408
    }

    /// <summary>
    /// Depth function enumeration.
    /// </summary>
    internal enum DepthFunction
    {
        /// <summary>
        /// Original was GL_NEVER = 0x0200
        /// </summary>
        Never = 0x0200,

        /// <summary>
        /// Original was GL_LESS = 0x0201
        /// </summary>
        Less = 0x0201,

        /// <summary>
        /// Original was GL_EQUAL = 0x0202
        /// </summary>
        Equal = 0x0202,

        /// <summary>
        /// Original was GL_LEQUAL = 0x0203
        /// </summary>
        Lequal = 0x0203,

        /// <summary>
        /// Original was GL_GREATER = 0x0204
        /// </summary>
        Greater = 0x0204,

        /// <summary>
        /// Original was GL_NOTEQUAL = 0x0205
        /// </summary>
        Notequal = 0x0205,

        /// <summary>
        /// Original was GL_GEQUAL = 0x0206
        /// </summary>
        Gequal = 0x0206,

        /// <summary>
        /// Original was GL_ALWAYS = 0x0207
        /// </summary>
        Always = 0x0207
    }

    /// <summary>
    /// Draw buffer mode enumeration.
    /// </summary>
    internal enum DrawBufferMode
    {
        /// <summary>
        /// The none. (GL_NONE = 0)
        /// </summary>
        None = 0,

        /// <summary>
        /// The none OES. (GL_NONE_OES = 0)
        /// </summary>
        NoneOes = 0,

        /// <summary>
        /// The front left. (GL_FRONT_LEFT = 0x0400)
        /// </summary>
        FrontLeft = 0x0400,

        /// <summary>
        /// The front right. (GL_FRONT_RIGHT = 0x0401)
        /// </summary>
        FrontRight = 0x0401,

        /// <summary>
        /// The back left. (GL_BACK_LEFT = 0x0402)
        /// </summary>
        BackLeft = 0x0402,

        /// <summary>
        /// The back right. (GL_BACK_RIGHT = 0x0403)
        /// </summary>
        BackRight = 0x0403,

        /// <summary>
        /// The front. (GL_FRONT = 0x0404)
        /// </summary>
        Front = 0x0404,

        /// <summary>
        /// The Back. (GL_BACK = 0x0405)
        /// </summary>
        Back = 0x0405,

        /// <summary>
        /// The left. (GL_LEFT = 0x0406)
        /// </summary>
        Left = 0x0406,

        /// <summary>
        /// The right. (GL_RIGHT = 0x0407)
        /// </summary>
        Right = 0x0407,

        /// <summary>
        /// The front and back. (GL_FRONT_AND_BACK = 0x0408)
        /// </summary>
        FrontAndBack = 0x0408,

        /// <summary>
        /// The ColorAttachment 0. (GL_COLOR_ATTACHMENT0 = 0x8CE0)
        /// </summary>
        ColorAttachment0 = 0x8CE0,

        /// <summary>
        /// The ColorAttachment 1. (GL_COLOR_ATTACHMENT1 = 0x8CE1)
        /// </summary>
        ColorAttachment1 = 0x8CE1,

        /// <summary>
        /// The ColorAttachment 2. (GL_COLOR_ATTACHMENT2 = 0x8CE2)
        /// </summary>
        ColorAttachment2 = 0x8CE2,

        /// <summary>
        /// The ColorAttachment 3. (GL_COLOR_ATTACHMENT3 = 0x8CE3)
        /// </summary>
        ColorAttachment3 = 0x8CE3,

        /// <summary>
        /// The ColorAttachment 4. (GL_COLOR_ATTACHMENT4 = 0x8CE4)
        /// </summary>
        ColorAttachment4 = 0x8CE4,

        /// <summary>
        /// The ColorAttachment 5. (GL_COLOR_ATTACHMENT5 = 0x8CE5)
        /// </summary>
        ColorAttachment5 = 0x8CE5,

        /// <summary>
        /// The ColorAttachment 6. (GL_COLOR_ATTACHMENT6 = 0x8CE6)
        /// </summary>
        ColorAttachment6 = 0x8CE6,

        /// <summary>
        /// The ColorAttachment 7. (GL_COLOR_ATTACHMENT7 = 0x8CE7)
        /// </summary>
        ColorAttachment7 = 0x8CE7,

        /// <summary>
        /// The ColorAttachment 8. (GL_COLOR_ATTACHMENT8 = 0x8CE8)
        /// </summary>
        ColorAttachment8 = 0x8CE8,

        /// <summary>
        /// The ColorAttachment 9. (GL_COLOR_ATTACHMENT9 = 0x8CE9)
        /// </summary>
        ColorAttachment9 = 0x8CE9,

        /// <summary>
        /// The ColorAttachment 10. (GL_COLOR_ATTACHMENT10 = 0x8CEA)
        /// </summary>
        ColorAttachment10 = 0x8CEA,

        /// <summary>
        /// The ColorAttachment 11. (GL_COLOR_ATTACHMENT11 = 0x8CEB)
        /// </summary>
        ColorAttachment11 = 0x8CEB,

        /// <summary>
        /// The ColorAttachment 12. (GL_COLOR_ATTACHMENT12 = 0x8CEC)
        /// </summary>
        ColorAttachment12 = 0x8CEC,

        /// <summary>
        /// The ColorAttachment 13. (GL_COLOR_ATTACHMENT13 = 0x8CED)
        /// </summary>
        ColorAttachment13 = 0x8CED,

        /// <summary>
        /// The ColorAttachment 14. (GL_COLOR_ATTACHMENT4 = 0x8CEE)
        /// </summary>
        ColorAttachment14 = 0x8CEE,

        /// <summary>
        /// The ColorAttachment 15. (GL_COLOR_ATTACHMENT15 = 0x8CEF)
        /// </summary>
        ColorAttachment15 = 0x8CEF
    }

    /// <summary>
    /// Draw elements type enumeration.
    /// </summary>
    internal enum DrawElementsType
    {
        /// <summary>
        /// Original was GL_UNSIGNED_BYTE = 0x1401
        /// </summary>
        UnsignedByte = 0x1401,

        /// <summary>
        /// Original was GL_UNSIGNED_SHORT = 0x1403
        /// </summary>
        UnsignedShort = 0x1403,

        /// <summary>
        /// Original was GL_UNSIGNED_INT = 0x1405
        /// </summary>
        UnsignedInt = 0x1405
    }

    /// <summary>
    /// Frame buffer attachment enumeration.
    /// </summary>
    internal enum FrameBufferAttachment
    {
        /// <summary>
        /// The front left. (GL_FRONT_LEFT = 0x0400)
        /// </summary>
        FrontLeft = 0x0400,

        /// <summary>
        /// The front right. (GL_FRONT_RIGHT = 0x0401)
        /// </summary>
        FrontRight = 0x0401,

        /// <summary>
        /// The back left. (GL_BACK_LEFT = 0x0402)
        /// </summary>
        BackLeft = 0x0402,

        /// <summary>
        /// The back right. (GL_BACK_RIGHT = 0x0403)
        /// </summary>
        BackRight = 0x0403,

        /// <summary>
        /// The AUX 0. (GL_AUX0 = 0x0409)
        /// </summary>
        Aux0 = 1033,

        /// <summary>
        /// The AUX 1. (GL_AUX1 = 0x040A)
        /// </summary>
        Aux1 = 0x040A,

        /// <summary>
        /// The AUX 2. (GL_AUX2 = 0x040B)
        /// </summary>
        Aux2 = 0x040B,

        /// <summary>
        /// The AUX 3. (GL_AUX3 = 0x040C)
        /// </summary>
        Aux3 = 0x040C,

        /// <summary>
        /// The color. (GL_AUX3 = 0x1800)
        /// </summary>
        Color = 0x1800,

        /// <summary>
        /// The depth. (GL_AUX3 = 0x1801)
        /// </summary>
        Depth = 0x1801,

        /// <summary>
        /// The stencil. (GL_STENCIL = 0x1802)
        /// </summary>
        Stencil = 0x1802,

        /// <summary>
        /// The depth stencil attachment. (GL_DEPTH_STENCIL_ATTACHMENT = 0x821A)
        /// </summary>
        DepthStencilAttachment = 0x821A,

        /// <summary>
        /// The max color attachments. (GL_MAX_COLOR_ATTACHMENTS = 0x8CDF)
        /// </summary>
        MaxColorAttachments = 0x8CDF,

        /// <summary>
        /// The max color attachments EXT. (GL_MAX_COLOR_ATTACHMENTS_EXT = 0x8CDF)
        /// </summary>
        MaxColorAttachmentsExt = 0x8CDF,

        /// <summary>
        /// The max color attachments NV. (GL_MAX_COLOR_ATTACHMENTS_NV = 0x8CDF)
        /// </summary>
        MaxColorAttachmentsNv = 0x8CDF,

        /// <summary>
        /// The color attachment 0. (GL_COLOR_ATTACHMENT0 = 0x8CE0)
        /// </summary>
        ColorAttachment0 = 0x8CE0,

        /// <summary>
        /// The color attachment 0 EXT. (GL_COLOR_ATTACHMENT0_EXT = 0x8CE0)
        /// </summary>
        ColorAttachment0Ext = 0x8CE0,

        /// <summary>
        /// The color attachment 0 NV. (GL_COLOR_ATTACHMENT0_NV = 0x8CE0)
        /// </summary>
        ColorAttachment0Nv = 0x8CE0,

        /// <summary>
        /// The color attachment 0 OES. (GL_COLOR_ATTACHMENT0_OES = 0x8CE0)
        /// </summary>
        ColorAttachment0Oes = 0x8CE0,

        /// <summary>
        /// The color attachment 1. (GL_COLOR_ATTACHMENT1 = 0x8CE1)
        /// </summary>
        ColorAttachment1 = 0x8CE1,

        /// <summary>
        /// The color attachment 1 EXT. (GL_COLOR_ATTACHMENT1_EXT = 0x8CE1)
        /// </summary>
        ColorAttachment1Ext = 0x8CE1,

        /// <summary>
        /// The color attachment 1 NV. (GL_COLOR_ATTACHMENT1_NV = 0x8CE1)
        /// </summary>
        ColorAttachment1Nv = 0x8CE1,

        /// <summary>
        /// The color attachment 2. (GL_COLOR_ATTACHMENT2 = 0x8CE2)
        /// </summary>
        ColorAttachment2 = 0x8CE2,

        /// <summary>
        /// The color attachment 2 EXT. (GL_COLOR_ATTACHMENT2_EXT = 0x8CE2)
        /// </summary>
        ColorAttachment2Ext = 0x8CE2,

        /// <summary>
        /// The color attachment 2 NV. (GL_COLOR_ATTACHMENT2_NV = 0x8CE2)
        /// </summary>
        ColorAttachment2Nv = 0x8CE2,

        /// <summary>
        /// The color attachment 3. (GL_COLOR_ATTACHMENT3 = 0x8CE3)
        /// </summary>
        ColorAttachment3 = 0x8CE3,

        /// <summary>
        /// The color attachment 3 EXT. (GL_COLOR_ATTACHMENT3_EXT = 0x8CE3)
        /// </summary>
        ColorAttachment3Ext = 0x8CE3,

        /// <summary>
        /// The color attachment 3 NV. (GL_COLOR_ATTACHMENT3_NV = 0x8CE3)
        /// </summary>
        ColorAttachment3Nv = 0x8CE3,

        /// <summary>
        /// The color attachment 4. (GL_COLOR_ATTACHMENT4 = 0x8CE4)
        /// </summary>
        ColorAttachment4 = 0x8CE4,

        /// <summary>
        /// The color attachment 4 EXT. (GL_COLOR_ATTACHMENT4_EXT = 0x8CE4)
        /// </summary>
        ColorAttachment4Ext = 0x8CE4,

        /// <summary>
        /// The color attachment 4 NV. (GL_COLOR_ATTACHMENT4_NV = 0x8CE4)
        /// </summary>
        ColorAttachment4Nv = 0x8CE4,

        /// <summary>
        /// The color attachment 5. (GL_COLOR_ATTACHMENT5 = 0x8CE5)
        /// </summary>
        ColorAttachment5 = 0x8CE5,

        /// <summary>
        /// The color attachment 5 EXT. (GL_COLOR_ATTACHMENT5_EXT = 0x8CE5)
        /// </summary>
        ColorAttachment5Ext = 0x8CE5,

        /// <summary>
        /// The color attachment 5 NV. (GL_COLOR_ATTACHMENT5_NV = 0x8CE5)
        /// </summary>
        ColorAttachment5Nv = 0x8CE5,

        /// <summary>
        /// The color attachment 6. (GL_COLOR_ATTACHMENT6 = 0x8CE6)
        /// </summary>
        ColorAttachment6 = 0x8CE6,

        /// <summary>
        /// The color attachment 6 EXT. (GL_COLOR_ATTACHMENT6_EXT = 0x8CE6)
        /// </summary>
        ColorAttachment6Ext = 0x8CE6,

        /// <summary>
        /// The color attachment 6 NV. (GL_COLOR_ATTACHMENT6_NV = 0x8CE6)
        /// </summary>
        ColorAttachment6Nv = 0x8CE6,

        /// <summary>
        /// The color attachment 7. (GL_COLOR_ATTACHMENT7 = 0x8CE7)
        /// </summary>
        ColorAttachment7 = 0x8CE7,

        /// <summary>
        /// The color attachment 7 EXT. (GL_COLOR_ATTACHMENT7_EXT = 0x8CE7)
        /// </summary>
        ColorAttachment7Ext = 0x8CE7,

        /// <summary>
        /// The color attachment 7 NV. (GL_COLOR_ATTACHMENT7_NV = 0x8CE7)
        /// </summary>
        ColorAttachment7Nv = 0x8CE7,

        /// <summary>
        /// The color attachment 8. (GL_COLOR_ATTACHMENT8 = 0x8CE8)
        /// </summary>
        ColorAttachment8 = 0x8CE8,

        /// <summary>
        /// The color attachment 8 EXT. (GL_COLOR_ATTACHMENT8_EXT = 0x8CE8)
        /// </summary>
        ColorAttachment8Ext = 0x8CE8,

        /// <summary>
        /// The color attachment 8 NV. (GL_COLOR_ATTACHMENT8_NV = 0x8CE8)
        /// </summary>
        ColorAttachment8Nv = 0x8CE8,

        /// <summary>
        /// The color attachment 9. (GL_COLOR_ATTACHMENT9 = 0x8CE9)
        /// </summary>
        ColorAttachment9 = 0x8CE9,

        /// <summary>
        /// The color attachment 9 EXT. (GL_COLOR_ATTACHMENT9_EXT = 0x8CE9)
        /// </summary>
        ColorAttachment9Ext = 0x8CE9,

        /// <summary>
        /// The color attachment 9 NV. (GL_COLOR_ATTACHMENT9_NV = 0x8CE9)
        /// </summary>
        ColorAttachment9Nv = 0x8CE9,

        /// <summary>
        /// The color attachment 10. (GL_COLOR_ATTACHMENT10 = 0x8CEA)
        /// </summary>
        ColorAttachment10 = 0x8CEA,

        /// <summary>
        /// The color attachment 10 EXT. (GL_COLOR_ATTACHMENT10_EXT = 0x8CEA)
        /// </summary>
        ColorAttachment10Ext = 0x8CEA,

        /// <summary>
        /// The color attachment 10 NV. (GL_COLOR_ATTACHMENT10_NV = 0x8CEA)
        /// </summary>
        ColorAttachment10Nv = 0x8CEA,

        /// <summary>
        /// The color attachment 11. (GL_COLOR_ATTACHMENT11 = 0x8CEB)
        /// </summary>
        ColorAttachment11 = 0x8CEB,

        /// <summary>
        /// The color attachment 11 EXT. (GL_COLOR_ATTACHMENT11_EXT = 0x8CEB)
        /// </summary>
        ColorAttachment11Ext = 0x8CEB,

        /// <summary>
        /// The color attachment 11 NV. (GL_COLOR_ATTACHMENT11_NV = 0x8CEB)
        /// </summary>
        ColorAttachment11Nv = 0x8CEB,

        /// <summary>
        /// The color attachment 12. (GL_COLOR_ATTACHMENT12 = 0x8CEC)
        /// </summary>
        ColorAttachment12 = 0x8CEC,

        /// <summary>
        /// The color attachment 12 EXT. (GL_COLOR_ATTACHMENT12_EXT = 0x8CEC)
        /// </summary>
        ColorAttachment12Ext = 0x8CEC,

        /// <summary>
        /// The color attachment 12 NV. (GL_COLOR_ATTACHMENT12_NV = 0x8CEC)
        /// </summary>
        ColorAttachment12Nv = 0x8CEC,

        /// <summary>
        /// The color attachment 13. (GL_COLOR_ATTACHMENT13 = 0x8CED)
        /// </summary>
        ColorAttachment13 = 0x8CED,

        /// <summary>
        /// The color attachment 13 EXT. (GL_COLOR_ATTACHMENT13_EXT = 0x8CED)
        /// </summary>
        ColorAttachment13Ext = 0x8CED,

        /// <summary>
        /// The color attachment 13 NV. (GL_COLOR_ATTACHMENT13_NV = 0x8CED)
        /// </summary>
        ColorAttachment13Nv = 0x8CED,

        /// <summary>
        /// The color attachment 14. (GL_COLOR_ATTACHMENT14 = 0x8CEE)
        /// </summary>
        ColorAttachment14 = 0x8CEE,

        /// <summary>
        /// The color attachment 14 EXT. (GL_COLOR_ATTACHMENT14_EXT = 0x8CEE)
        /// </summary>
        ColorAttachment14Ext = 0x8CEE,

        /// <summary>
        /// The color attachment 14 NV. (GL_COLOR_ATTACHMENT14_NV = 0x8CEE)
        /// </summary>
        ColorAttachment14Nv = 0x8CEE,

        /// <summary>
        /// The color attachment 15. (GL_COLOR_ATTACHMENT15 = 0x8CEF)
        /// </summary>
        ColorAttachment15 = 0x8CEF,

        /// <summary>
        /// The color attachment 15 EXT. (GL_COLOR_ATTACHMENT15_EXT = 0x8CEF)
        /// </summary>
        ColorAttachment15Ext = 0x8CEF,

        /// <summary>
        /// The color attachment 15 NV. (GL_COLOR_ATTACHMENT15_NV = 0x8CEF)
        /// </summary>
        ColorAttachment15Nv = 0x8CEF,

        /// <summary>
        /// The color attachment 16 (GL_COLOR_ATTACHMENT16 = 0x8CF0)
        /// </summary>
        ColorAttachment16 = 0x8CF0,

        /// <summary>
        /// The color attachment 17 (GL_COLOR_ATTACHMENT17 = 0x8CF1)
        /// </summary>
        ColorAttachment17 = 0x8CF1,

        /// <summary>
        /// The color attachment 18 (GL_COLOR_ATTACHMENT18 = 0x8CF2)
        /// </summary>
        ColorAttachment18 = 0x8CF2,

        /// <summary>
        /// The color attachment 19 (GL_COLOR_ATTACHMENT19 = 0x8CF3)
        /// </summary>
        ColorAttachment19 = 0x8CF3,

        /// <summary>
        /// The color attachment 20 (GL_COLOR_ATTACHMENT20 = 0x8CF4)
        /// </summary>
        ColorAttachment20 = 0x8CF4,

        /// <summary>
        /// The color attachment 21 (GL_COLOR_ATTACHMENT21 = 0x8CF5)
        /// </summary>
        ColorAttachment21 = 0x8CF5,

        /// <summary>
        /// The color attachment 22 (GL_COLOR_ATTACHMENT22 = 0x8CF6)
        /// </summary>
        ColorAttachment22 = 0x8CF6,

        /// <summary>
        /// The color attachment 23 (GL_COLOR_ATTACHMENT23 = 0x8CF7)
        /// </summary>
        ColorAttachment23 = 0x8CF7,

        /// <summary>
        /// The color attachment 24 (GL_COLOR_ATTACHMENT24 = 0x8CF8)
        /// </summary>
        ColorAttachment24 = 0x8CF8,

        /// <summary>
        /// The color attachment 25 (GL_COLOR_ATTACHMENT25 = 0x8CF9)
        /// </summary>
        ColorAttachment25 = 0x8CF9,

        /// <summary>
        /// The color attachment 26 (GL_COLOR_ATTACHMENT26 = 0x8CFA)
        /// </summary>
        ColorAttachment26 = 0x8CFA,

        /// <summary>
        /// The color attachment 27 (GL_COLOR_ATTACHMENT27 = 0x8CFB)
        /// </summary>
        ColorAttachment27 = 0x8CFB,

        /// <summary>
        /// The color attachment 28 (GL_COLOR_ATTACHMENT28 = 0x8CFC)
        /// </summary>
        ColorAttachment28 = 0x8CFC,

        /// <summary>
        /// The color attachment 29 (GL_COLOR_ATTACHMENT29 = 0x8CFD)
        /// </summary>
        ColorAttachment29 = 0x8CFD,

        /// <summary>
        /// The color attachment 30 (GL_COLOR_ATTACHMENT30 = 0x8CFE)
        /// </summary>
        ColorAttachment30 = 0x8CFE,

        /// <summary>
        /// The color attachment 31 (GL_COLOR_ATTACHMENT31 = 0x8CFF)
        /// </summary>
        ColorAttachment31 = 0x8CFF,

        /// <summary>
        /// The depth attachment (GL_DEPTH_ATTACHMENT = 0x8D00)
        /// </summary>
        DepthAttachment = 0x8D00,

        /// <summary>
        /// The depth attachment EXT (GL_DEPTH_ATTACHMENT_EXT = 0x8D00)
        /// </summary>
        DepthAttachmentExt = 0x8D00,

        /// <summary>
        /// The depth attachment OES (GL_DEPTH_ATTACHMENT_OES = 0x8D00)
        /// </summary>
        DepthAttachmentOes = 36096,

        /// <summary>
        /// The stencil attachment (GL_STENCIL_ATTACHMENT = 0x8D20)
        /// </summary>
        StencilAttachment = 0x8D20,

        /// <summary>
        /// The stencil attachment EXT (GL_STENCIL_ATTACHMENT_EXT = 0x8D20)
        /// </summary>
        StencilAttachmentExt = 0x8D20
    }

    /// <summary>
    /// Frame buffer target enumeration.
    /// </summary>
    internal enum FrameBufferTarget : uint
    {
        /// <summary>
        /// The read frame buffer (GL_READ_FRAMEBUFFER = 0x8CA8)
        /// </summary>
        ReadFramebuffer = 0x8CA8,

        /// <summary>
        /// The draw frame buffer (GL_DRAW_FRAMEBUFFER = 0x8CA9)
        /// </summary>
        DrawFramebuffer = 0x8CA9,

        /// <summary>
        /// The frame buffer (GL_FRAMEBUFFER = 0x8D40)
        /// </summary>
        Framebuffer = 0x8D40,

        /// <summary>
        /// The frame buffer extension (GL_FRAMEBUFFER_EXT = 0x8D40)
        /// </summary>
        FramebufferExt = 0x8D40
    }

    /// <summary>
    /// Front face direction enumeration.
    /// </summary>
    internal enum FrontFaceDirection
    {
        /// <summary>
        ///  Original was GL_CW = 0x0900
        /// </summary>
        Clockwise = 0x0900,

        /// <summary>
        /// Original was GL_CCW = 0x0901
        /// </summary>
        CounterClockwise = 0x0901
    }

    /// <summary>
    /// Mipmap target enumeration type.
    /// </summary>
    internal enum GenerateMipmapTarget
    {
        /// <summary>
        /// Original was GL_TEXTURE_1D = 0x0DE0
        /// </summary>
        Texture1D = 0x0DE0,

        /// <summary>
        /// Original was GL_TEXTURE_2D = 0x0DE1
        /// </summary>
        Texture2D = 0x0DE1,

        /// <summary>
        /// Original was GL_TEXTURE_3D = 0x806F
        /// </summary>
        Texture3D = 0x806F,

        /// <summary>
        /// Original was GL_TEXTURE_CUBE_MAP = 0x8513
        /// </summary>
        TextureCubeMap = 0x8513,

        /// <summary>
        /// Original was GL_TEXTURE_1D_ARRAY = 0x8C18
        /// </summary>
        Texture1DArray = 0x8C18,

        /// <summary>
        /// Original was GL_TEXTURE_2D_ARRAY = 0x8C1A
        /// </summary>
        Texture2DArray = 0x8C1A,

        /// <summary>
        /// Original was GL_TEXTURE_CUBE_MAP_ARRAY = 0x9009
        /// </summary>
        TextureCubeMapArray = 0x9009,

        /// <summary>
        /// Original was GL_TEXTURE_2D_MULTISAMPLE = 0x9100
        /// </summary>
        Texture2DMultisample = 0x9100,

        /// <summary>
        /// Original was GL_TEXTURE_2D_MULTISAMPLE_ARRAY = 0x9102
        /// </summary>
        Texture2DMultisampleArray = 0x9102
    }

    /// <summary>
    /// Shader program parameter name enumeration.
    /// </summary>
    internal enum ProgramParameterName
    {
        /// <summary>
        /// Original was GL_PROGRAM_BINARY_RETRIEVABLE_HINT = 0x8257
        /// </summary>
        ProgramBinaryRetrievableHint = 33367,

        /// <summary>
        /// Original was GL_PROGRAM_SEPARABLE = 0x8258
        /// </summary>
        ProgramSeparable = 33368,

        /// <summary>
        /// Original was GL_GEOMETRY_SHADER_INVOCATIONS = 0x887F
        /// </summary>
        GeometryShaderInvocations = 34943,

        /// <summary>
        /// Original was GL_GEOMETRY_VERTICES_OUT = 0x8916
        /// </summary>
        GeometryVerticesOut = 35094,

        /// <summary>
        /// Original was GL_GEOMETRY_INPUT_TYPE = 0x8917
        /// </summary>
        GeometryInputType = 35095,

        /// <summary>
        /// Original was GL_GEOMETRY_OUTPUT_TYPE = 0x8918
        /// </summary>
        GeometryOutputType = 35096,

        /// <summary>
        /// Original was GL_ACTIVE_UNIFORM_BLOCK_MAX_NAME_LENGTH = 0x8A35
        /// </summary>
        ActiveUniformBlockMaxNameLength = 35381,

        /// <summary>
        /// Original was GL_ACTIVE_UNIFORM_BLOCKS = 0x8A36
        /// </summary>
        ActiveUniformBlocks = 35382,

        /// <summary>
        /// Original was GL_DELETE_STATUS = 0x8B80
        /// </summary>
        DeleteStatus = 35712,

        /// <summary>
        /// Original was GL_LINK_STATUS = 0x8B82
        /// </summary>
        LinkStatus = 35714,

        /// <summary>
        /// Original was GL_VALIDATE_STATUS = 0x8B83
        /// </summary>
        ValidateStatus = 35715,

        /// <summary>
        /// Original was GL_INFO_LOG_LENGTH = 0x8B84
        /// </summary>
        InfoLogLength = 35716,

        /// <summary>
        /// Original was GL_ATTACHED_SHADERS = 0x8B85
        /// </summary>
        AttachedShaders = 35717,

        /// <summary>
        /// Original was GL_ACTIVE_UNIFORMS = 0x8B86
        /// </summary>
        ActiveUniforms = 35718,

        /// <summary>
        /// Original was GL_ACTIVE_UNIFORM_MAX_LENGTH = 0x8B87
        /// </summary>
        ActiveUniformMaxLength = 35719,

        /// <summary>
        /// Original was GL_ACTIVE_ATTRIBUTES = 0x8B89
        /// </summary>
        ActiveAttributes = 35721,

        /// <summary>
        /// Original was GL_ACTIVE_ATTRIBUTE_MAX_LENGTH = 0x8B8A
        /// </summary>
        ActiveAttributeMaxLength = 35722,

        /// <summary>
        /// Original was GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH = 0x8C76
        /// </summary>
        TransformFeedbackVaryingMaxLength = 35958,

        /// <summary>
        /// Original was GL_TRANSFORM_FEEDBACK_BUFFER_MODE = 0x8C7F
        /// </summary>
        TransformFeedbackBufferMode = 35967,

        /// <summary>
        /// Original was GL_TRANSFORM_FEEDBACK_VARYINGS = 0x8C83
        /// </summary>
        TransformFeedbackVaryings = 35971,

        /// <summary>
        /// Original was GL_TESS_CONTROL_OUTPUT_VERTICES = 0x8E75
        /// </summary>
        TessControlOutputVertices = 36469,

        /// <summary>
        /// Original was GL_TESS_GEN_MODE = 0x8E76
        /// </summary>
        TessGenMode = 36470,

        /// <summary>
        /// Original was GL_TESS_GEN_SPACING = 0x8E77
        /// </summary>
        TessGenSpacing = 36471,

        /// <summary>
        /// Original was GL_TESS_GEN_VERTEX_ORDER = 0x8E78
        /// </summary>
        TessGenVertexOrder = 36472,

        /// <summary>
        /// Original was GL_TESS_GEN_POINT_MODE = 0x8E79
        /// </summary>
        TessGenPointMode = 36473,

        /// <summary>
        /// Original was GL_MAX_COMPUTE_WORK_GROUP_SIZE = 0x91BF
        /// </summary>
        MaxComputeWorkGroupSize = 37311,

        /// <summary>
        /// Original was GL_ACTIVE_ATOMIC_COUNTER_BUFFERS = 0x92D9
        /// </summary>
        ActiveAtomicCounterBuffers = 37593
    }

    /// <summary>
    /// Pixel format enumeration.
    /// </summary>
    internal enum PixelFormat
    {
        /// <summary>
        /// Original was GL_UNSIGNED_SHORT = 0x1403
        /// </summary>
        UnsignedShort = 0x1403,

        /// <summary>
        /// Original was GL_UNSIGNED_INT = 0x1405
        /// </summary>
        UnsignedInt = 0x1405,

        /// <summary>
        /// Original was GL_COLOR_INDEX = 0x1900
        /// </summary>
        ColorIndex = 0x1900,

        /// <summary>
        /// Original was GL_STENCIL_INDEX = 0x1901
        /// </summary>
        StencilIndex = 0x1901,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT = 0x1902
        /// </summary>
        DepthComponent = 0x1902,

        /// <summary>
        /// Original was GL_RED = 0x1903
        /// </summary>
        Red = 0x1903,

        /// <summary>
        /// Original was GL_RED_EXT = 0x1903
        /// </summary>
        RedExt = 0x1903,

        /// <summary>
        /// Original was GL_GREEN = 0x1904
        /// </summary>
        Green = 0x1904,

        /// <summary>
        /// Original was GL_BLUE = 0x1905
        /// </summary>
        Blue = 0x1905,

        /// <summary>
        /// Original was GL_ALPHA = 0x1906
        /// </summary>
        Alpha = 0x1906,

        /// <summary>
        /// Original was GL_RGB = 0x1907
        /// </summary>
        Rgb = 0x1907,

        /// <summary>
        /// Original was GL_RGBA = 0x1908
        /// </summary>
        Rgba = 0x1908,

        /// <summary>
        /// Original was GL_LUMINANCE = 0x1909
        /// </summary>
        Luminance = 0x1909,

        /// <summary>
        /// Original was GL_LUMINANCE_ALPHA = 0x190A
        /// </summary>
        LuminanceAlpha = 0x190A,

        /// <summary>
        /// Original was GL_ABGR_EXT = 0x8000
        /// </summary>
        AbgrExt = 0x8000,

        /// <summary>
        /// Original was GL_CMYK_EXT = 0x800C
        /// </summary>
        CmykExt = 0x800C,

        /// <summary>
        /// Original was GL_CMYKA_EXT = 0x800D
        /// </summary>
        CmykaExt = 0x800D,

        /// <summary>
        /// Original was GL_BGR = 0x80E0
        /// </summary>
        Bgr = 0x80E0,

        /// <summary>
        /// Original was GL_BGRA = 0x80E1
        /// </summary>
        Bgra = 0x80E1,

        /// <summary>
        /// Original was GL_YCRCB_422_SGIX = 0x81BB
        /// </summary>
        Ycrcb422Sgix = 0x81BB,

        /// <summary>
        /// Original was GL_YCRCB_444_SGIX = 0x81BC
        /// </summary>
        Ycrcb444Sgix = 0x81BC,

        /// <summary>
        /// Original was GL_RG = 0x8227
        /// </summary>
        Rg = 0x8227,

        /// <summary>
        /// Original was GL_RG_INTEGER = 0x8228
        /// </summary>
        RgInteger = 0x8228,

        /// <summary>
        /// Original was GL_R5_G6_B5_ICC_SGIX = 0x8466
        /// </summary>
        R5G6B5IccSgix = 0x8466,

        /// <summary>
        /// Original was GL_R5_G6_B5_A8_ICC_SGIX = 0x8467
        /// </summary>
        R5G6B5A8IccSgix = 0x8467,

        /// <summary>
        /// Original was GL_ALPHA16_ICC_SGIX = 0x8468
        /// </summary>
        Alpha16IccSgix = 0x8468,

        /// <summary>
        /// Original was GL_LUMINANCE16_ICC_SGIX = 0x8469
        /// </summary>
        Luminance16IccSgix = 0x8469,

        /// <summary>
        /// Original was GL_LUMINANCE16_ALPHA8_ICC_SGIX = 0x846B
        /// </summary>
        Luminance16Alpha8IccSgix = 0x846B,

        /// <summary>
        /// Original was GL_DEPTH_STENCIL = 0x84F9
        /// </summary>
        DepthStencil = 0x84F9,

        /// <summary>
        /// Original was GL_RED_INTEGER = 0x8D94
        /// </summary>
        RedInteger = 0x8D94,

        /// <summary>
        /// Original was GL_GREEN_INTEGER = 0x8D95
        /// </summary>
        GreenInteger = 0x8D95,

        /// <summary>
        /// Original was GL_BLUE_INTEGER = 0x8D96
        /// </summary>
        BlueInteger = 0x8D96,

        /// <summary>
        /// Original was GL_ALPHA_INTEGER = 0x8D97
        /// </summary>
        AlphaInteger = 0x8D97,

        /// <summary>
        /// Original was GL_RGB_INTEGER = 0x8D98
        /// </summary>
        RgbInteger = 0x8D98,

        /// <summary>
        /// Original was GL_RGBA_INTEGER = 0x8D99
        /// </summary>
        RgbaInteger = 0x8D99,

        /// <summary>
        /// Original was GL_BGR_INTEGER = 0x8D9A
        /// </summary>
        BgrInteger = 0x8D9A,

        /// <summary>
        /// Original was GL_BGRA_INTEGER = 0x8D9B
        /// </summary>
        BgraInteger = 0x8D9B
    }

    /// <summary>
    /// Internal pixel format enumeration.
    /// </summary>
    internal enum PixelInternalFormat
    {
        /// <summary>
        /// Original was GL_ONE = 1
        /// </summary>
        One = 1,

        /// <summary>
        /// Original was GL_TWO = 2
        /// </summary>
        Two = 2,

        /// <summary>
        /// Original was GL_THREE = 3
        /// </summary>
        Three = 3,

        /// <summary>
        /// Original was GL_FOUR = 4
        /// </summary>
        Four = 4,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT = 0x1902
        /// </summary>
        DepthComponent = 0x1902,

        /// <summary>
        /// Original was GL_ALPHA = 0x1906
        /// </summary>
        Alpha = 0x1906,

        /// <summary>
        /// Original was GL_RGB = 0x1907
        /// </summary>
        Rgb = 0x1907,

        /// <summary>
        /// Original was GL_RGBA = 0x1908
        /// </summary>
        Rgba = 0x1908,

        /// <summary>
        /// Original was GL_LUMINANCE = 0x1909
        /// </summary>
        Luminance = 0x1909,

        /// <summary>
        /// Original was GL_LUMINANCE_ALPHA = 0x190A
        /// </summary>
        LuminanceAlpha = 0x190A,

        /// <summary>
        /// Original was GL_R3_G3_B2 = 0x2A10
        /// </summary>
        R3G3B2 = 0x2A10,

        /// <summary>
        /// Original was GL_RGB2_EXT = 0x804E
        /// </summary>
        Rgb2Ext = 0x804E,

        /// <summary>
        /// Original was GL_RGB4 = 0x804F
        /// </summary>
        Rgb4 = 0x804F,

        /// <summary>
        /// Original was GL_RGB5 = 0x8050
        /// </summary>
        Rgb5 = 0x8050,

        /// <summary>
        /// Original was GL_RGB8 = 0x8051
        /// </summary>
        Rgb8 = 0x8051,

        /// <summary>
        /// Original was GL_RGB10 = 0x8052
        /// </summary>
        Rgb10 = 0x8052,

        /// <summary>
        /// Original was GL_RGB12 = 0x8053
        /// </summary>
        Rgb12 = 0x8053,

        /// <summary>
        /// Original was GL_RGB16 = 0x8054
        /// </summary>
        Rgb16 = 0x8054,

        /// <summary>
        /// Original was GL_RGBA2 = 0x8055
        /// </summary>
        Rgba2 = 0x8055,

        /// <summary>
        /// Original was GL_RGBA4 = 0x8056
        /// </summary>
        Rgba4 = 0x8056,

        /// <summary>
        /// Original was GL_RGB5_A1 = 0x8057
        /// </summary>
        Rgb5A1 = 0x8057,

        /// <summary>
        /// Original was GL_RGBA8 = 0x8058
        /// </summary>
        Rgba8 = 0x8058,

        /// <summary>
        /// Original was GL_RGB10_A2 = 0x8059
        /// </summary>
        Rgb10A2 = 0x8059,

        /// <summary>
        /// Original was GL_RGBA12 = 0x805A
        /// </summary>
        Rgba12 = 0x805A,

        /// <summary>
        /// Original was GL_RGBA16 = 0x805B
        /// </summary>
        Rgba16 = 0x805B,

        /// <summary>
        /// Original was GL_DUAL_ALPHA4_SGIS = 0x8110
        /// </summary>
        DualAlpha4Sgis = 0x8110,

        /// <summary>
        /// Original was GL_DUAL_ALPHA8_SGIS = 0x8111
        /// </summary>
        DualAlpha8Sgis = 0x8111,

        /// <summary>
        /// Original was GL_DUAL_ALPHA12_SGIS = 0x8112
        /// </summary>
        DualAlpha12Sgis = 0x8112,

        /// <summary>
        /// Original was GL_DUAL_ALPHA16_SGIS = 0x8113
        /// </summary>
        DualAlpha16Sgis = 0x8113,

        /// <summary>
        /// Original was GL_DUAL_LUMINANCE4_SGIS = 0x8114
        /// </summary>
        DualLuminance4Sgis = 0x8114,

        /// <summary>
        /// Original was GL_DUAL_LUMINANCE8_SGIS = 0x8115
        /// </summary>
        DualLuminance8Sgis = 0x8115,

        /// <summary>
        /// Original was GL_DUAL_LUMINANCE12_SGIS = 0x8116
        /// </summary>
        DualLuminance12Sgis = 0x8116,

        /// <summary>
        /// Original was GL_DUAL_LUMINANCE16_SGIS = 0x8117
        /// </summary>
        DualLuminance16Sgis = 0x8117,

        /// <summary>
        /// Original was GL_DUAL_INTENSITY4_SGIS = 0x8118
        /// </summary>
        DualIntensity4Sgis = 0x8118,

        /// <summary>
        /// Original was GL_DUAL_INTENSITY8_SGIS = 0x8119
        /// </summary>
        DualIntensity8Sgis = 0x8119,

        /// <summary>
        /// Original was GL_DUAL_INTENSITY12_SGIS = 0x811A
        /// </summary>
        DualIntensity12Sgis = 0x811A,

        /// <summary>
        /// Original was GL_DUAL_INTENSITY16_SGIS = 0x811B
        /// </summary>
        DualIntensity16Sgis = 0x811B,

        /// <summary>
        /// Original was GL_DUAL_LUMINANCE_ALPHA4_SGIS = 0x811C
        /// </summary>
        DualLuminanceAlpha4Sgis = 0x811C,

        /// <summary>
        /// Original was GL_DUAL_LUMINANCE_ALPHA8_SGIS = 0x811D
        /// </summary>
        DualLuminanceAlpha8Sgis = 0x811D,

        /// <summary>
        /// Original was GL_QUAD_ALPHA4_SGIS = 0x811E
        /// </summary>
        QuadAlpha4Sgis = 0x811E,

        /// <summary>
        /// Original was GL_QUAD_ALPHA8_SGIS = 0x811F
        /// </summary>
        QuadAlpha8Sgis = 0x811F,

        /// <summary>
        /// Original was GL_QUAD_LUMINANCE4_SGIS = 0x8120
        /// </summary>
        QuadLuminance4Sgis = 0x8120,

        /// <summary>
        /// Original was GL_QUAD_LUMINANCE8_SGIS = 0x8121
        /// </summary>
        QuadLuminance8Sgis = 0x8121,

        /// <summary>
        /// Original was GL_QUAD_INTENSITY4_SGIS = 0x8122
        /// </summary>
        QuadIntensity4Sgis = 0x8122,

        /// <summary>
        /// Original was GL_QUAD_INTENSITY8_SGIS = 0x8123
        /// </summary>
        QuadIntensity8Sgis = 0x8123,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT16 = 0x81A5
        /// </summary>
        DepthComponent16 = 0x81A5,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT16_SGIX = 0x81A5
        /// </summary>
        DepthComponent16Sgix = 0x81A5,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT24 = 0x81A6
        /// </summary>
        DepthComponent24 = 0x81A6,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT24_SGIX = 0x81A6
        /// </summary>
        DepthComponent24Sgix = 0x81A6,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT32 = 0x81A7
        /// </summary>
        DepthComponent32 = 0x81A7,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT32_SGIX = 0x81A7
        /// </summary>
        DepthComponent32Sgix = 0x81A7,

        /// <summary>
        /// Original was GL_COMPRESSED_RED = 0x8225
        /// </summary>
        CompressedRed = 0x8225,

        /// <summary>
        /// Original was GL_COMPRESSED_RG = 0x8226
        /// </summary>
        CompressedRg = 0x8226,

        /// <summary>
        /// Original was GL_R8 = 0x8229
        /// </summary>
        R8 = 0x8229,

        /// <summary>
        /// Original was GL_R16 = 0x822A
        /// </summary>
        R16 = 0x822A,

        /// <summary>
        /// Original was GL_RG8 = 0x822B
        /// </summary>
        Rg8 = 0x822B,

        /// <summary>
        /// Original was GL_RG16 = 0x822C
        /// </summary>
        Rg16 = 0x822C,

        /// <summary>
        /// Original was GL_R16F = 0x822D
        /// </summary>
        R16f = 0x822D,

        /// <summary>
        /// Original was GL_R32F = 0x822E
        /// </summary>
        R32f = 0x822E,

        /// <summary>
        /// Original was GL_RG16F = 0x822F
        /// </summary>
        Rg16f = 0x822F,

        /// <summary>
        /// Original was GL_RG32F = 0x8230
        /// </summary>
        Rg32f = 0x8230,

        /// <summary>
        /// Original was GL_R8I = 0x8231
        /// </summary>
        R8i = 0x8231,

        /// <summary>
        /// Original was GL_R8UI = 0x8232
        /// </summary>
        R8ui = 0x8232,

        /// <summary>
        /// Original was GL_R16I = 0x8233
        /// </summary>
        R16i = 0x8233,

        /// <summary>
        /// Original was GL_R16UI = 0x8234
        /// </summary>
        R16ui = 0x8234,

        /// <summary>
        /// Original was GL_R32I = 0x8235
        /// </summary>
        R32i = 0x8235,

        /// <summary>
        /// Original was GL_R32UI = 0x8236
        /// </summary>
        R32ui = 0x8236,

        /// <summary>
        /// Original was GL_RG8I = 0x8237
        /// </summary>
        Rg8i = 0x8237,

        /// <summary>
        /// Original was GL_RG8UI = 0x8238
        /// </summary>
        Rg8ui = 0x8238,

        /// <summary>
        /// Original was GL_RG16I = 0x8239
        /// </summary>
        Rg16i = 0x8239,

        /// <summary>
        /// Original was GL_RG16UI = 0x823A
        /// </summary>
        Rg16ui = 0x823A,

        /// <summary>
        /// Original was GL_RG32I = 0x823B
        /// </summary>
        Rg32i = 0x823B,

        /// <summary>
        /// Original was GL_RG32UI = 0x823C
        /// </summary>
        Rg32ui = 0x823C,

        /// <summary>
        /// Original was GL_COMPRESSED_RGB_S3TC_DXT1_EXT = 0x83F0
        /// </summary>
        CompressedRgbS3tcDxt1Ext = 0x83F0,

        /// <summary>
        /// Original was GL_COMPRESSED_RGBA_S3TC_DXT1_EXT = 0x83F1
        /// </summary>
        CompressedRgbaS3tcDxt1Ext = 0x83F1,

        /// <summary>
        /// Original was GL_COMPRESSED_RGBA_S3TC_DXT3_EXT = 0x83F2
        /// </summary>
        CompressedRgbaS3tcDxt3Ext = 0x83F2,

        /// <summary>
        /// Original was GL_COMPRESSED_RGBA_S3TC_DXT5_EXT = 0x83F3
        /// </summary>
        CompressedRgbaS3tcDxt5Ext = 0x83F3,

        /// <summary>
        /// Original was GL_RGB_ICC_SGIX = 0x8460
        /// </summary>
        RgbIccSgix = 0x8460,

        /// <summary>
        /// Original was GL_RGBA_ICC_SGIX = 0x8461
        /// </summary>
        RgbaIccSgix = 0x8461,

        /// <summary>
        /// Original was GL_ALPHA_ICC_SGIX = 0x8462
        /// </summary>
        AlphaIccSgix = 0x8462,

        /// <summary>
        /// Original was GL_LUMINANCE_ICC_SGIX = 0x8463
        /// </summary>
        LuminanceIccSgix = 0x8463,

        /// <summary>
        /// Original was GL_INTENSITY_ICC_SGIX = 0x8464
        /// </summary>
        IntensityIccSgix = 0x8464,

        /// <summary>
        /// Original was GL_LUMINANCE_ALPHA_ICC_SGIX = 0x8465
        /// </summary>
        LuminanceAlphaIccSgix = 0x8465,

        /// <summary>
        /// Original was GL_R5_G6_B5_ICC_SGIX = 0x8466
        /// </summary>
        R5G6B5IccSgix = 0x8466,

        /// <summary>
        /// Original was GL_R5_G6_B5_A8_ICC_SGIX = 0x8467
        /// </summary>
        R5G6B5A8IccSgix = 0x8467,

        /// <summary>
        /// Original was GL_ALPHA16_ICC_SGIX = 0x8468
        /// </summary>
        Alpha16IccSgix = 0x8468,

        /// <summary>
        /// Original was GL_LUMINANCE16_ICC_SGIX = 0x8469
        /// </summary>
        Luminance16IccSgix = 0x8469,

        /// <summary>
        /// Original was GL_INTENSITY16_ICC_SGIX = 0x846A
        /// </summary>
        Intensity16IccSgix = 0x846A,

        /// <summary>
        /// Original was GL_LUMINANCE16_ALPHA8_ICC_SGIX = 0x846B
        /// </summary>
        Luminance16Alpha8IccSgix = 0x846B,

        /// <summary>
        /// Original was GL_COMPRESSED_ALPHA = 0x84E9
        /// </summary>
        CompressedAlpha = 0x84E9,

        /// <summary>
        /// Original was GL_COMPRESSED_LUMINANCE = 0x84EA
        /// </summary>
        CompressedLuminance = 0x84EA,

        /// <summary>
        /// Original was GL_COMPRESSED_LUMINANCE_ALPHA = 0x84EB
        /// </summary>
        CompressedLuminanceAlpha = 0x84EB,

        /// <summary>
        /// Original was GL_COMPRESSED_INTENSITY = 0x84EC
        /// </summary>
        CompressedIntensity = 0x84EC,

        /// <summary>
        /// Original was GL_COMPRESSED_RGB = 0x84ED
        /// </summary>
        CompressedRgb = 0x84ED,

        /// <summary>
        /// Original was GL_COMPRESSED_RGBA = 0x84EE
        /// </summary>
        CompressedRgba = 0x84EE,

        /// <summary>
        /// Original was GL_DEPTH_STENCIL = 0x84F9
        /// </summary>
        DepthStencil = 0x84F9,

        /// <summary>
        /// Original was GL_RGBA32F = 0x8814
        /// </summary>
        Rgba32f = 0x8814,

        /// <summary>
        /// Original was GL_RGB32F = 0x8815
        /// </summary>
        Rgb32f = 0x8815,

        /// <summary>
        /// Original was GL_RGBA16F = 0x881A
        /// </summary>
        Rgba16f = 0x881A,

        /// <summary>
        /// Original was GL_RGB16F = 0x881B
        /// </summary>
        Rgb16f = 0x881B,

        /// <summary>
        /// Original was GL_DEPTH24_STENCIL8 = 0x88F0
        /// </summary>
        Depth24Stencil8 = 0x88F0,

        /// <summary>
        /// Original was GL_R11F_G11F_B10F = 0x8C3A
        /// </summary>
        R11fG11fB10f = 0x8C3A,

        /// <summary>
        /// Original was GL_RGB9_E5 = 0x8C3D
        /// </summary>
        Rgb9E5 = 0x8C3D,

        /// <summary>
        /// Original was GL_SRGB = 0x8C40
        /// </summary>
        Srgb = 0x8C40,

        /// <summary>
        /// Original was GL_SRGB8 = 0x8C41
        /// </summary>
        Srgb8 = 0x8C41,

        /// <summary>
        /// Original was GL_SRGB_ALPHA = 0x8C42
        /// </summary>
        SrgbAlpha = 0x8C42,

        /// <summary>
        /// Original was GL_SRGB8_ALPHA8 = 0x8C43
        /// </summary>
        Srgb8Alpha8 = 0x8C43,

        /// <summary>
        /// Original was GL_SLUMINANCE_ALPHA = 0x8C44
        /// </summary>
        SluminanceAlpha = 0x8C44,

        /// <summary>
        /// Original was GL_SLUMINANCE8_ALPHA8 = 0x8C45
        /// </summary>
        Sluminance8Alpha8 = 0x8C45,

        /// <summary>
        /// Original was GL_SLUMINANCE = 0x8C46
        /// </summary>
        Sluminance = 0x8C46,

        /// <summary>
        /// Original was GL_SLUMINANCE8 = 0x8C47
        /// </summary>
        Sluminance8 = 0x8C47,

        /// <summary>
        /// Original was GL_COMPRESSED_SRGB = 0x8C48
        /// </summary>
        CompressedSrgb = 0x8C48,

        /// <summary>
        /// Original was GL_COMPRESSED_SRGB_ALPHA = 0x8C49
        /// </summary>
        CompressedSrgbAlpha = 0x8C49,

        /// <summary>
        /// Original was GL_COMPRESSED_SLUMINANCE = 0x8C4A
        /// </summary>
        CompressedSluminance = 0x8C4A,

        /// <summary>
        /// Original was GL_COMPRESSED_SLUMINANCE_ALPHA = 0x8C4B
        /// </summary>
        CompressedSluminanceAlpha = 0x8C4B,

        /// <summary>
        /// Original was GL_COMPRESSED_SRGB_S3TC_DXT1_EXT = 0x8C4C
        /// </summary>
        CompressedSrgbS3tcDxt1Ext = 0x8C4C,

        /// <summary>
        /// Original was GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT1_EXT = 0x8C4D
        /// </summary>
        CompressedSrgbAlphaS3tcDxt1Ext = 0x8C4D,

        /// <summary>
        /// Original was GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT3_EXT = 0x8C4E
        /// </summary>
        CompressedSrgbAlphaS3tcDxt3Ext = 0x8C4E,

        /// <summary>
        /// Original was GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT5_EXT = 0x8C4F
        /// </summary>
        CompressedSrgbAlphaS3tcDxt5Ext = 0x8C4F,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT32F = 0x8CAC
        /// </summary>
        DepthComponent32f = 0x8CAC,

        /// <summary>
        /// Original was GL_DEPTH32F_STENCIL8 = 0x8CAD
        /// </summary>
        Depth32fStencil8 = 0x8CAD,

        /// <summary>
        /// Original was GL_RGBA32UI = 0x8D70
        /// </summary>
        Rgba32ui = 0x8D70,

        /// <summary>
        /// Original was GL_RGB32UI = 0x8D71
        /// </summary>
        Rgb32ui = 0x8D71,

        /// <summary>
        /// Original was GL_RGBA16UI = 0x8D76
        /// </summary>
        Rgba16ui = 0x8D76,

        /// <summary>
        /// Original was GL_RGB16UI = 0x8D77
        /// </summary>
        Rgb16ui = 0x8D77,

        /// <summary>
        /// Original was GL_RGBA8UI = 0x8D7C
        /// </summary>
        Rgba8ui = 0x8D7C,

        /// <summary>
        /// Original was GL_RGB8UI = 0x8D7D
        /// </summary>
        Rgb8ui = 0x8D7D,

        /// <summary>
        /// Original was GL_RGBA32I = 0x8D82
        /// </summary>
        Rgba32i = 0x8D82,

        /// <summary>
        /// Original was GL_RGB32I = 0x8D83
        /// </summary>
        Rgb32i = 0x8D83,

        /// <summary>
        /// Original was GL_RGBA16I = 0x8D88
        /// </summary>
        Rgba16i = 0x8D88,

        /// <summary>
        /// Original was GL_RGB16I = 0x8D89
        /// </summary>
        Rgb16i = 0x8D89,

        /// <summary>
        /// Original was GL_RGBA8I = 0x8D8E
        /// </summary>
        Rgba8i = 0x8D8E,

        /// <summary>
        /// Original was GL_RGB8I = 0x8D8F
        /// </summary>
        Rgb8i = 0x8D8F,

        /// <summary>
        /// Original was GL_FLOAT_32_UNSIGNED_INT_24_8_REV = 0x8DAD
        /// </summary>
        Float32UnsignedInt248Rev = 0x8DAD,

        /// <summary>
        /// Original was GL_COMPRESSED_RED_RGTC1 = 0x8DBB
        /// </summary>
        CompressedRedRgtc1 = 0x8DBB,

        /// <summary>
        /// Original was GL_COMPRESSED_SIGNED_RED_RGTC1 = 0x8DBC
        /// </summary>
        CompressedSignedRedRgtc1 = 0x8DBC,

        /// <summary>
        /// Original was GL_COMPRESSED_RG_RGTC2 = 0x8DBD
        /// </summary>
        CompressedRgRgtc2 = 0x8DBD,

        /// <summary>
        /// Original was GL_COMPRESSED_SIGNED_RG_RGTC2 = 0x8DBE
        /// </summary>
        CompressedSignedRgRgtc2 = 0x8DBE,

        /// <summary>
        /// Original was GL_COMPRESSED_RGBA_BPTC_UNORM = 0x8E8C
        /// </summary>
        CompressedRgbaBptcUnorm = 0x8E8C,

        /// <summary>
        /// Original was GL_COMPRESSED_SRGB_ALPHA_BPTC_UNORM = 0x8E8D
        /// </summary>
        CompressedSrgbAlphaBptcUnorm = 0x8E8D,

        /// <summary>
        /// Original was GL_COMPRESSED_RGB_BPTC_SIGNED_FLOAT = 0x8E8E
        /// </summary>
        CompressedRgbBptcSignedFloat = 0x8E8E,

        /// <summary>
        /// Original was GL_COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT = 0x8E8F
        /// </summary>
        CompressedRgbBptcUnsignedFloat = 0x8E8F,

        /// <summary>
        /// Original was GL_R8_SNORM = 0x8F94
        /// </summary>
        R8Snorm = 0x8F94,

        /// <summary>
        /// Original was GL_RG8_SNORM = 0x8F95
        /// </summary>
        Rg8Snorm = 0x8F95,

        /// <summary>
        /// Original was GL_RGB8_SNORM = 0x8F96
        /// </summary>
        Rgb8Snorm = 0x8F96,

        /// <summary>
        /// Original was GL_RGBA8_SNORM = 0x8F97
        /// </summary>
        Rgba8Snorm = 0x8F97,

        /// <summary>
        /// Original was GL_R16_SNORM = 0x8F98
        /// </summary>
        R16Snorm = 0x8F98,

        /// <summary>
        /// Original was GL_RG16_SNORM = 0x8F99
        /// </summary>
        Rg16Snorm = 0x8F99,

        /// <summary>
        /// Original was GL_RGB16_SNORM = 0x8F9A
        /// </summary>
        Rgb16Snorm = 0x8F9A,

        /// <summary>
        /// Original was GL_RGBA16_SNORM = 0x8F9B
        /// </summary>
        Rgba16Snorm = 0x8F9B,

        /// <summary>
        /// Original was GL_RGB10_A2UI = 0x906F
        /// </summary>
        Rgb10A2ui = 0x906F
    }

    /// <summary>
    /// Pixel type numeration.
    /// </summary>
    internal enum PixelType
    {
        /// <summary>
        /// Original was GL_BYTE = 0x1400
        /// </summary>
        Byte = 0x1400,

        /// <summary>
        /// Original was GL_UNSIGNED_BYTE = 0x1401
        /// </summary>
        UnsignedByte = 0x1401,

        /// <summary>
        /// Original was GL_SHORT = 0x1402
        /// </summary>
        Short = 0x1402,

        /// <summary>
        /// Original was GL_UNSIGNED_SHORT = 0x1403
        /// </summary>
        UnsignedShort = 0x1403,

        /// <summary>
        /// Original was GL_INT = 0x1404
        /// </summary>
        Int = 0x1404,

        /// <summary>
        /// Original was GL_UNSIGNED_INT = 0x1405
        /// </summary>
        UnsignedInt = 0x1405,

        /// <summary>
        /// Original was GL_FLOAT = 0x1406
        /// </summary>
        Float = 0x1406,

        /// <summary>
        /// Original was GL_HALF_FLOAT = 0x140B
        /// </summary>
        HalfFloat = 0x140B,

        /// <summary>
        /// Original was GL_UNSIGNED_BYTE_3_3_2 = 0x8032
        /// </summary>
        UnsignedByte332 = 0x8032,

        /// <summary>
        /// Original was GL_UNSIGNED_BYTE_3_3_2_EXT = 0x8032
        /// </summary>
        UnsignedByte332Ext = 0x8032,

        /// <summary>
        /// Original was GL_UNSIGNED_SHORT_4_4_4_4 = 0x8033
        /// </summary>
        UnsignedShort4444 = 0x8033,

        /// <summary>
        /// Original was GL_UNSIGNED_SHORT_4_4_4_4_EXT = 0x8033
        /// </summary>
        UnsignedShort4444Ext = 0x8033,

        /// <summary>
        /// Original was GL_UNSIGNED_SHORT_5_5_5_1 = 0x8034
        /// </summary>
        UnsignedShort5551 = 0x8034,

        /// <summary>
        /// Original was GL_UNSIGNED_SHORT_5_5_5_1_EXT = 0x8034
        /// </summary>
        UnsignedShort5551Ext = 0x8034,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_8_8_8_8 = 0x8035
        /// </summary>
        UnsignedInt8888 = 0x8035,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_8_8_8_8_EXT = 0x8035
        /// </summary>
        UnsignedInt8888Ext = 0x8035,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_10_10_10_2 = 0x8036
        /// </summary>
        UnsignedInt1010102 = 0x8036,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_10_10_10_2_EXT = 0x8036
        /// </summary>
        UnsignedInt1010102Ext = 0x8036,

        /// <summary>
        /// Original was GL_UNSIGNED_BYTE_2_3_3_REVERSED = 0x8362
        /// </summary>
        UnsignedByte233Reversed = 0x8362,

        /// <summary>
        /// Original was GL_UNSIGNED_SHORT_5_6_5 = 0x8363
        /// </summary>
        UnsignedShort565 = 0x8363,

        /// <summary>
        /// Original was GL_UNSIGNED_SHORT_5_6_5_REVERSED = 0x8364
        /// </summary>
        UnsignedShort565Reversed = 0x8364,

        /// <summary>
        /// Original was GL_UNSIGNED_SHORT_4_4_4_4_REVERSED = 0x8365
        /// </summary>
        UnsignedShort4444Reversed = 0x8365,

        /// <summary>
        /// Original was GL_UNSIGNED_SHORT_1_5_5_5_REVERSED = 0x8366
        /// </summary>
        UnsignedShort1555Reversed = 0x8366,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_8_8_8_8_REVERSED = 0x8367
        /// </summary>
        UnsignedInt8888Reversed = 0x8367,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_2_10_10_10_REVERSED = 0x8368
        /// </summary>
        UnsignedInt2101010Reversed = 0x8368,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_24_8 = 0x84FA
        /// </summary>
        UnsignedInt248 = 0x84FA,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_10F_11F_11F_REV = 0x8C3B
        /// </summary>
        UnsignedInt10F11F11FRev = 0x8C3B,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_5_9_9_9_REV = 0x8C3E
        /// </summary>
        UnsignedInt5999Rev = 0x8C3E,

        /// <summary>
        /// Original was GL_FLOAT_32_UNSIGNED_INT_24_8_REV = 0x8DAD
        /// </summary>
        Float32UnsignedInt248Rev = 0x8DAD
    }

    /// <summary>
    /// Drawing primitive type enumeration.
    /// </summary>
    internal enum PrimitiveType
    {
        /// <summary>
        /// Original was GL_POINTS = 0x0000
        /// </summary>
        Points = 0x0000,

        /// <summary>
        /// Original was GL_LINES = 0x0001
        /// </summary>
        Lines = 0x0001,

        /// <summary>
        /// Original was GL_LINE_LOOP = 0x0002
        /// </summary>
        LineLoop = 0x0002,

        /// <summary>
        /// Original was GL_LINE_STRIP = 0x0003
        /// </summary>
        LineStrip = 0x0003,

        /// <summary>
        /// Original was GL_TRIANGLES = 0x0004
        /// </summary>
        Triangles = 0x0004,

        /// <summary>
        /// Original was GL_TRIANGLE_STRIP = 0x0005
        /// </summary>
        TriangleStrip = 0x0005,

        /// <summary>
        /// Original was GL_TRIANGLE_FAN = 0x0006
        /// </summary>
        TriangleFan = 0x0006,

        /// <summary>
        /// Original was GL_QUADS = 0x0007
        /// </summary>
        Quads = 0x0007,

        /// <summary>
        /// Original was GL_QUADS_EXT = 0x0007
        /// </summary>
        QuadsExt = 0x0007,

        /// <summary>
        /// Original was GL_LINES_ADJACENCY = 0x000A
        /// </summary>
        LinesAdjacency = 0x000A,

        /// <summary>
        /// Original was GL_LINES_ADJACENCY_ARB = 0x000A
        /// </summary>
        LinesAdjacencyArb = 0x000A,

        /// <summary>
        /// Original was GL_LINES_ADJACENCY_EXT = 0x000A
        /// </summary>
        LinesAdjacencyExt = 0x000A,

        /// <summary>
        /// Original was GL_LINE_STRIP_ADJACENCY = 0x000B
        /// </summary>
        LineStripAdjacency = 0x000B,

        /// <summary>
        /// Original was GL_LINE_STRIP_ADJACENCY_ARB = 0x000B
        /// </summary>
        LineStripAdjacencyArb = 0x000B,

        /// <summary>
        /// Original was GL_LINE_STRIP_ADJACENCY_EXT = 0x000B
        /// </summary>
        LineStripAdjacencyExt = 0x000B,

        /// <summary>
        /// Original was GL_TRIANGLES_ADJACENCY = 0x000C
        /// </summary>
        TrianglesAdjacency = 0x000C,

        /// <summary>
        /// Original was GL_TRIANGLES_ADJACENCY_ARB = 0x000C
        /// </summary>
        TrianglesAdjacencyArb = 0x000C,

        /// <summary>
        /// Original was GL_TRIANGLES_ADJACENCY_EXT = 0x000C
        /// </summary>
        TrianglesAdjacencyExt = 0x000C,

        /// <summary>
        /// Original was GL_TRIANGLE_STRIP_ADJACENCY = 0x000D
        /// </summary>
        TriangleStripAdjacency = 0x000D,

        /// <summary>
        /// Original was GL_TRIANGLE_STRIP_ADJACENCY_ARB = 0x000D
        /// </summary>
        TriangleStripAdjacencyArb = 0x000D,

        /// <summary>
        /// Original was GL_TRIANGLE_STRIP_ADJACENCY_EXT = 0x000D
        /// </summary>
        TriangleStripAdjacencyExt = 0x000D,

        /// <summary>
        /// Original was GL_PATCHES = 0x000E
        /// </summary>
        Patches = 0x000E,

        /// <summary>
        /// Original was GL_PATCHES_EXT = 0x000E
        /// </summary>
        PatchesExt = 0x000E
    }

    /// <summary>
    /// Read buffer mode enumeration.
    /// </summary>
    internal enum ReadBufferMode
    {
        /// <summary>
        /// The none. (GL_NONE = 0)
        /// </summary>
        None = 0,

        /// <summary>
        /// The none OES. (GL_NONE_OES = 0)
        /// </summary>
        NoneOes = 0,

        /// <summary>
        /// The front left. (GL_FRONT_LEFT = 0x0400)
        /// </summary>
        FrontLeft = 0x0400,

        /// <summary>
        /// The front right. (GL_FRONT_RIGHT = 0x0401)
        /// </summary>
        FrontRight = 0x0401,

        /// <summary>
        /// The back left. (GL_BACK_LEFT = 0x0402)
        /// </summary>
        BackLeft = 0x0402,

        /// <summary>
        /// The back right. (GL_BACK_RIGHT = 0x0403)
        /// </summary>
        BackRight = 0x0403,

        /// <summary>
        /// The front. (GL_FRONT = 0x0404)
        /// </summary>
        Front = 0x0404,

        /// <summary>
        /// The Back. (GL_BACK = 0x0405)
        /// </summary>
        Back = 0x0405,

        /// <summary>
        /// The left. (GL_LEFT = 0x0406)
        /// </summary>
        Left = 0x0406,

        /// <summary>
        /// The right. (GL_RIGHT = 0x0407)
        /// </summary>
        Right = 0x0407,

        /// <summary>
        /// The front and back. (GL_FRONT_AND_BACK = 0x0408)
        /// </summary>
        FrontAndBack = 0x0408,

        /// <summary>
        /// The AUX 0. (GL_AUX0 = 0x0409)
        /// </summary>
        Aux0 = 1033,

        /// <summary>
        /// The AUX 1. (GL_AUX1 = 0x040A)
        /// </summary>
        Aux1 = 0x040A,

        /// <summary>
        /// The AUX 2. (GL_AUX2 = 0x040B)
        /// </summary>
        Aux2 = 0x040B,

        /// <summary>
        /// The AUX 3. (GL_AUX3 = 0x040C)
        /// </summary>
        Aux3 = 0x040C,

        /// <summary>
        /// The ColorAttachment 0. (GL_COLOR_ATTACHMENT0 = 0x8CE0)
        /// </summary>
        ColorAttachment0 = 0x8CE0,

        /// <summary>
        /// The ColorAttachment 1. (GL_COLOR_ATTACHMENT1 = 0x8CE1)
        /// </summary>
        ColorAttachment1 = 0x8CE1,

        /// <summary>
        /// The ColorAttachment 2. (GL_COLOR_ATTACHMENT2 = 0x8CE2)
        /// </summary>
        ColorAttachment2 = 0x8CE2,

        /// <summary>
        /// The ColorAttachment 3. (GL_COLOR_ATTACHMENT3 = 0x8CE3)
        /// </summary>
        ColorAttachment3 = 0x8CE3,

        /// <summary>
        /// The ColorAttachment 4. (GL_COLOR_ATTACHMENT4 = 0x8CE4)
        /// </summary>
        ColorAttachment4 = 0x8CE4,

        /// <summary>
        /// The ColorAttachment 5. (GL_COLOR_ATTACHMENT5 = 0x8CE5)
        /// </summary>
        ColorAttachment5 = 0x8CE5,

        /// <summary>
        /// The ColorAttachment 6. (GL_COLOR_ATTACHMENT6 = 0x8CE6)
        /// </summary>
        ColorAttachment6 = 0x8CE6,

        /// <summary>
        /// The ColorAttachment 7. (GL_COLOR_ATTACHMENT7 = 0x8CE7)
        /// </summary>
        ColorAttachment7 = 0x8CE7,

        /// <summary>
        /// The ColorAttachment 8. (GL_COLOR_ATTACHMENT8 = 0x8CE8)
        /// </summary>
        ColorAttachment8 = 0x8CE8,

        /// <summary>
        /// The ColorAttachment 9. (GL_COLOR_ATTACHMENT9 = 0x8CE9)
        /// </summary>
        ColorAttachment9 = 0x8CE9,

        /// <summary>
        /// The ColorAttachment 10. (GL_COLOR_ATTACHMENT10 = 0x8CEA)
        /// </summary>
        ColorAttachment10 = 0x8CEA,

        /// <summary>
        /// The ColorAttachment 11. (GL_COLOR_ATTACHMENT11 = 0x8CEB)
        /// </summary>
        ColorAttachment11 = 0x8CEB,

        /// <summary>
        /// The ColorAttachment 12. (GL_COLOR_ATTACHMENT12 = 0x8CEC)
        /// </summary>
        ColorAttachment12 = 0x8CEC,

        /// <summary>
        /// The ColorAttachment 13. (GL_COLOR_ATTACHMENT13 = 0x8CED)
        /// </summary>
        ColorAttachment13 = 0x8CED,

        /// <summary>
        /// The ColorAttachment 14. (GL_COLOR_ATTACHMENT4 = 0x8CEE)
        /// </summary>
        ColorAttachment14 = 0x8CEE,

        /// <summary>
        /// The ColorAttachment 15. (GL_COLOR_ATTACHMENT15 = 0x8CEF)
        /// </summary>
        ColorAttachment15 = 0x8CEF
    }

    /// <summary>
    /// Render buffer storage enumeration.
    /// </summary>
    internal enum RenderBufferStorage
    {
        /// <summary>
        /// Original was GL_DEPTH_COMPONENT = 0x1902
        /// </summary>
        DepthComponent = 0x1902,

        /// <summary>
        /// Original was GL_R3_G3_B2 = 0x2A10
        /// </summary>
        R3G3B2 = 0x2A10,

        /// <summary>
        /// Original was GL_RGB4 = 0x804F
        /// </summary>
        Rgb4 = 0x804F,

        /// <summary>
        /// Original was GL_RGB5 = 0x8050
        /// </summary>
        Rgb5 = 0x8050,

        /// <summary>
        /// Original was GL_RGB8 = 0x8051
        /// </summary>
        Rgb8 = 0x8051,

        /// <summary>
        /// Original was GL_RGB10 = 0x8053
        /// </summary>
        Rgb10 = 0x8053,

        /// <summary>
        /// Original was GL_RGB12 = 0x8053
        /// </summary>
        Rgb12 = 0x8053,

        /// <summary>
        /// Original was GL_RGB16 = 0x8054
        /// </summary>
        Rgb16 = 0x8054,

        /// <summary>
        /// Original was GL_RGBA2 = 0x8055
        /// </summary>
        Rgba2 = 0x8055,

        /// <summary>
        /// Original was GL_RGBA4 = 0x8056
        /// </summary>
        Rgba4 = 0x8056,

        /// <summary>
        /// Original was GL_RGBA8 = 0x8058
        /// </summary>
        Rgba8 = 0x8058,

        /// <summary>
        /// Original was GL_RGB10_A2 = 0x8059
        /// </summary>
        Rgb10A2 = 0x8059,

        /// <summary>
        /// Original was GL_RGBA12 = 0x805A
        /// </summary>
        Rgba12 = 0x805A,

        /// <summary>
        /// Original was GL_RGBA16 = 0x805B
        /// </summary>
        Rgba16 = 0x805B,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT16 = 0x81a5
        /// </summary>
        DepthComponent16 = 0x81a5,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT24 = 0x81a6
        /// </summary>
        DepthComponent24 = 0x81a6,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT32 = 0x81a7
        /// </summary>
        DepthComponent32 = 0x81a7,

        /// <summary>
        /// Original was GL_R8 = 0x8229
        /// </summary>
        R8 = 0x8229,

        /// <summary>
        /// Original was GL_R16 = 0x822A
        /// </summary>
        R16 = 0x822A,

        /// <summary>
        /// Original was GL_RG8 = 0x822B
        /// </summary>
        Rg8 = 0x822B,

        /// <summary>
        /// Original was GL_RG16 = 0x822C
        /// </summary>
        Rg16 = 0x822C,

        /// <summary>
        /// Original was GL_R16F = 0x822D
        /// </summary>
        R16f = 0x822D,

        /// <summary>
        /// Original was GL_R32F = 0x822E
        /// </summary>
        R32f = 0x822E,

        /// <summary>
        /// Original was GL_RG16F = 0x8230
        /// </summary>
        Rg16f = 0x8230,

        /// <summary>
        /// Original was GL_RG32F = 0x8230
        /// </summary>
        Rg32f = 0x8230,

        /// <summary>
        /// Original was GL_R8I = 0x8231
        /// </summary>
        R8i = 0x8231,

        /// <summary>
        /// Original was GL_R8UI = 0x8232
        /// </summary>
        R8ui = 0x8232,

        /// <summary>
        /// Original was GL_R16I = 0x8233
        /// </summary>
        R16i = 0x8233,

        /// <summary>
        /// Original was GL_R16UI = 0x8234
        /// </summary>
        R16ui = 0x8234,

        /// <summary>
        /// Original was GL_R32I = 0x8235
        /// </summary>
        R32i = 0x8235,

        /// <summary>
        /// Original was GL_R32UI = 0x8236
        /// </summary>
        R32ui = 0x8236,

        /// <summary>
        /// Original was GL_RG8I = 0x8237
        /// </summary>
        Rg8i = 0x8237,

        /// <summary>
        /// Original was GL_RG8UI = 0x8238
        /// </summary>
        Rg8ui = 0x8238,

        /// <summary>
        /// Original was GL_RG16I = 0x8239
        /// </summary>
        Rg16i = 0x8239,

        /// <summary>
        /// Original was GL_RG16UI = 0x823A
        /// </summary>
        Rg16ui = 0x823A,

        /// <summary>
        /// Original was GL_RG32I = 0x823B
        /// </summary>
        Rg32i = 0x823B,

        /// <summary>
        /// Original was GL_RG32UI = 0x823C
        /// </summary>
        Rg32ui = 0x823C,

        /// <summary>
        /// Original was GL_DEPTH_STENCIL = 0x84F9
        /// </summary>
        DepthStencil = 0x84F9,

        /// <summary>
        /// Original was GL_RGBA32F = 0x8814
        /// </summary>
        Rgba32f = 0x8814,

        /// <summary>
        /// Original was GL_RGB32F = 0x8815
        /// </summary>
        Rgb32f = 0x8815,

        /// <summary>
        /// Original was GL_RGBA16F = 0x881A
        /// </summary>
        Rgba16f = 0x881A,

        /// <summary>
        /// Original was GL_RGB16F = 0x881B
        /// </summary>
        Rgb16f = 0x881B,

        /// <summary>
        /// Original was GL_DEPTH24_STENCIL8 = 0x88F0
        /// </summary>
        Depth24Stencil8 = 0x88F0,

        /// <summary>
        /// Original was GL_R11F_G11F_B10F = 0x8C3A
        /// </summary>
        R11fG11fB10f = 0x8C3A,

        /// <summary>
        /// Original was GL_RGB9_E5 = 0x8C3D
        /// </summary>
        Rgb9E5 = 0x8C3D,

        /// <summary>
        /// Original was GL_SRGB8 = 0x8C41
        /// </summary>
        Srgb8 = 0x8C41,

        /// <summary>
        /// Original was GL_SRGB8_ALPHA8 = 0x8C43
        /// </summary>
        Srgb8Alpha8 = 0x8C43,

        /// <summary>
        /// Original was GL_DEPTH_COMPONENT32F = 0x8CAC
        /// </summary>
        DepthComponent32f = 0x8CAC,

        /// <summary>
        /// Original was GL_DEPTH32F_STENCIL8 = 0x8CAD
        /// </summary>
        Depth32fStencil8 = 0x8CAD,

        /// <summary>
        /// Original was GL_STENCIL_INDEX1 = 0x8D46
        /// </summary>
        StencilIndex1 = 0x8D46,

        /// <summary>
        /// Original was GL_STENCIL_INDEX1_EXT = 0x8D46
        /// </summary>
        StencilIndex1Ext = 0x8D46,

        /// <summary>
        /// Original was GL_STENCIL_INDEX4 = 0x8D47
        /// </summary>
        StencilIndex4 = 0x8D47,

        /// <summary>
        /// Original was GL_STENCIL_INDEX4_EXT = 0x8D47
        /// </summary>
        StencilIndex4Ext = 0x8D47,

        /// <summary>
        /// Original was GL_STENCIL_INDEX8 = 0x8D48
        /// </summary>
        StencilIndex8 = 0x8D48,

        /// <summary>
        /// Original was GL_STENCIL_INDEX8_EXT = 0x8D48
        /// </summary>
        StencilIndex8Ext = 0x8D48,

        /// <summary>
        /// Original was GL_STENCIL_INDEX16 = 0x8D49
        /// </summary>
        StencilIndex16 = 0x8D49,

        /// <summary>
        /// Original was GL_STENCIL_INDEX16_EXT = 0x8D49
        /// </summary>
        StencilIndex16Ext = 0x8D49,

        /// <summary>
        /// Original was GL_RGBA32UI = 0x8D70
        /// </summary>
        Rgba32ui = 0x8D70,

        /// <summary>
        /// Original was GL_RGB32UI = 0x8D71
        /// </summary>
        Rgb32ui = 0x8D71,

        /// <summary>
        /// Original was GL_RGBA16UI = 0x8D76
        /// </summary>
        Rgba16ui = 0x8D76,

        /// <summary>
        /// Original was GL_RGB16UI = 0x8D77
        /// </summary>
        Rgb16ui = 0x8D77,

        /// <summary>
        /// Original was GL_RGBA8UI = 0x8D7C
        /// </summary>
        Rgba8ui = 0x8D7C,

        /// <summary>
        /// Original was GL_RGB8UI = 0x8D7D
        /// </summary>
        Rgb8ui = 0x8D7D,

        /// <summary>
        /// Original was GL_RGBA32I = 0x8D82
        /// </summary>
        Rgba32i = 0x8D82,

        /// <summary>
        /// Original was GL_RGB32I = 0x8D83
        /// </summary>
        Rgb32i = 0x8D83,

        /// <summary>
        /// Original was GL_RGBA16I = 0x8D88
        /// </summary>
        Rgba16i = 0x8D88,

        /// <summary>
        /// Original was GL_RGB16I = 0x8D89
        /// </summary>
        Rgb16i = 0x8D89,

        /// <summary>
        /// Original was GL_RGBA8I = 0x8D8E
        /// </summary>
        Rgba8i = 0x8D8E,

        /// <summary>
        /// Original was GL_RGB8I = 0x8D8F
        /// </summary>
        Rgb8i = 0x8D8F,

        /// <summary>
        /// Original was GL_RGB10_A2UI = 0x906F
        /// </summary>
        Rgb10A2ui = 0x906F
    }

    /// <summary>
    /// Render buffer target enumeration.
    /// </summary>
    internal enum RenderBufferTarget
    {
        /// <summary>
        /// Original was GL_RENDERBUFFER = 0x8D41
        /// </summary>
        Renderbuffer = 0x8D41,

        /// <summary>
        /// Original was GL_RENDERBUFFER_EXT = 0x8D41
        /// </summary>
        RenderbufferExt = 0x8D41
    }

    /// <summary>
    /// Shader parameter type enumeration.
    /// </summary>
    internal enum ShaderParameter
    {
        /// <summary>
        /// Original was GL_SHADER_TYPE = 0x8B4F
        /// </summary>
        ShaderType = 0x8B4F,

        /// <summary>
        /// Original was GL_DELETE_STATUS = 0x8B80
        /// </summary>
        DeleteStatus = 0x8B80,

        /// <summary>
        /// Original was GL_COMPILE_STATUS = 0x8B81
        /// </summary>
        CompileStatus = 0x8B81,

        /// <summary>
        /// Original was GL_INFO_LOG_LENGTH = 0x8B84
        /// </summary>
        InfoLogLength = 0x8B84,

        /// <summary>
        /// Original was GL_SHADER_SOURCE_LENGTH = 0x8B88
        /// </summary>
        ShaderSourceLength = 0x8B88
    }

    /// <summary>
    /// Shader type enumeration.
    /// </summary>
    internal enum ShaderType
    {
        /// <summary>
        /// Original was GL_FRAGMENT_SHADER = 0x8B30
        /// </summary>
        FragmentShader = 0x8B30,

        /// <summary>
        /// Original was GL_FRAGMENT_SHADER_ARB = 0x8B30
        /// </summary>
        FragmentShaderArb = 0x8B30,

        /// <summary>
        /// Original was GL_VERTEX_SHADER = 0x8B31
        /// </summary>
        VertexShader = 0x8B31,

        /// <summary>
        /// Original was GL_VERTEX_SHADER_ARB = 0x8B31
        /// </summary>
        VertexShaderArb = 0x8B31,

        /// <summary>
        /// Original was GL_GEOMETRY_SHADER = 0x8DD9
        /// </summary>
        GeometryShader = 0x8DD9,

        /// <summary>
        /// Original was GL_TESS_EVALUATION_SHADER = 0x8E87
        /// </summary>
        TessEvaluationShader = 0x8E87,

        /// <summary>
        /// Original was GL_TESS_CONTROL_SHADER = 0x8E88
        /// </summary>
        TessControlShader = 0x8E88,

        /// <summary>
        /// Original was GL_COMPUTE_SHADER = 0x91B9
        /// </summary>
        ComputeShader = 0x91B9
    }

    /// <summary>
    /// GL string type enumeration.
    /// </summary>
    internal enum StringType
    {
        /// <summary>
        /// The extensions string.
        /// </summary>
        GL_EXTENSIONS = 0x1F03,

        /// <summary>
        /// The renderer name string.
        /// </summary>
        GL_RENDERER = 0x1F01,

        /// <summary>
        /// The vendor name string.
        /// </summary>
        GL_VENDOR = 0x1F00,

        /// <summary>
        /// The version string.
        /// </summary>
        GL_VERSION = 0x1F02
    }

    /// <summary>
    /// Texture compare mode enumeration.
    /// </summary>
    internal enum TextureCompareMode
    {
        /// <summary>
        /// Original was GL_COMPARE_REF_TO_TEXTURE = 0x884E
        /// </summary>
        CompareRefToTexture = 0x884E,
    }

    /// <summary>
    /// Texture mag filter enumeration.
    /// </summary>
    internal enum TextureMagFilter
    {
        /// <summary>
        /// Original was GL_NEAREST = 0x2600
        /// </summary>
        Nearest = 0x2600,

        /// <summary>
        /// Original was GL_LINEAR = 0x2601
        /// </summary>
        Linear = 0x2601,

        /// <summary>
        /// Original was GL_LINEAR_DETAIL_SGIS = 0x8097
        /// </summary>
        LinearDetailSgis = 0x8097,

        /// <summary>
        /// Original was GL_LINEAR_DETAIL_ALPHA_SGIS = 0x8098
        /// </summary>
        LinearDetailAlphaSgis = 0x8098,

        /// <summary>
        /// Original was GL_LINEAR_DETAIL_COLOR_SGIS = 0x8099
        /// </summary>
        LinearDetailColorSgis = 0x8099,

        /// <summary>
        /// Original was GL_LINEAR_SHARPEN_SGIS = 0x80AD
        /// </summary>
        LinearSharpenSgis = 0x80AD,

        /// <summary>
        /// Original was GL_LINEAR_SHARPEN_ALPHA_SGIS = 0x80AE
        /// </summary>
        LinearSharpenAlphaSgis = 0x80AE,

        /// <summary>
        /// Original was GL_LINEAR_SHARPEN_COLOR_SGIS = 0x80AF
        /// </summary>
        LinearSharpenColorSgis = 0x80AF,

        /// <summary>
        /// Original was GL_FILTER4_SGIS = 0x8146
        /// </summary>
        Filter4Sgis = 0x8146,

        /// <summary>
        /// Original was GL_PIXEL_TEX_GEN_Q_CEILING_SGIX = 0x8184
        /// </summary>
        PixelTexGenQCeilingSgix = 0x8184,

        /// <summary>
        /// Original was GL_PIXEL_TEX_GEN_Q_ROUND_SGIX = 0x8185
        /// </summary>
        PixelTexGenQRoundSgix = 0x8185,

        /// <summary>
        /// Original was GL_PIXEL_TEX_GEN_Q_FLOOR_SGIX = 0x8186
        /// </summary>
        PixelTexGenQFloorSgix = 0x8186
    }

    /// <summary>
    /// Texture min filter type enumeration.
    /// </summary>
    internal enum TextureMinFilter
    {
        /// <summary>
        /// Original was GL_NEAREST = 0x2600
        /// </summary>
        Nearest = 0x2600,

        /// <summary>
        /// Original was GL_LINEAR = 0x2601
        /// </summary>
        Linear = 0x2601,

        /// <summary>
        /// Original was GL_NEAREST_MIPMAP_NEAREST = 0x2700
        /// </summary>
        NearestMipmapNearest = 0x2700,

        /// <summary>
        /// Original was GL_LINEAR_MIPMAP_NEAREST = 0x2701
        /// </summary>
        LinearMipmapNearest = 0x2701,

        /// <summary>
        /// Original was GL_NEAREST_MIPMAP_LINEAR = 0x2702
        /// </summary>
        NearestMipmapLinear = 0x2702,

        /// <summary>
        /// Original was GL_LINEAR_MIPMAP_LINEAR = 0x2703
        /// </summary>
        LinearMipmapLinear = 0x2703,

        /// <summary>
        /// Original was GL_FILTER4_SGIS = 0x8146
        /// </summary>
        Filter4Sgis = 0x8146,

        /// <summary>
        /// Original was GL_LINEAR_CLIPMAP_LINEAR_SGIX = 0x8170
        /// </summary>
        LinearClipmapLinearSgix = 0x8170,

        /// <summary>
        /// Original was GL_PIXEL_TEX_GEN_Q_CEILING_SGIX = 0x8184
        /// </summary>
        PixelTexGenQCeilingSgix = 0x8184,

        /// <summary>
        /// Original was GL_PIXEL_TEX_GEN_Q_ROUND_SGIX = 0x8185
        /// </summary>
        PixelTexGenQRoundSgix = 0x8185,

        /// <summary>
        /// Original was GL_PIXEL_TEX_GEN_Q_FLOOR_SGIX = 0x8186
        /// </summary>
        PixelTexGenQFloorSgix = 0x8186,

        /// <summary>
        /// Original was GL_NEAREST_CLIPMAP_NEAREST_SGIX = 0x844D
        /// </summary>
        NearestClipmapNearestSgix = 0x844D,

        /// <summary>
        /// Original was GL_NEAREST_CLIPMAP_LINEAR_SGIX = 0x844E
        /// </summary>
        NearestClipmapLinearSgix = 0x844E,

        /// <summary>
        /// Original was GL_LINEAR_CLIPMAP_NEAREST_SGIX = 0x844F
        /// </summary>
        LinearClipmapNearestSgix = 0x844F
    }

    /// <summary>
    /// Texture parameter name enumeration.
    /// </summary>
    internal enum TextureParameterName
    {
        /// <summary>
        /// Original was GL_TEXTURE_WIDTH = 0x1000
        /// </summary>
        TextureWidth = 0x1000,

        /// <summary>
        /// Original was GL_TEXTURE_HEIGHT = 0x1001
        /// </summary>
        TextureHeight = 0x1001,

        /// <summary>
        /// Original was GL_TEXTURE_COMPONENTS = 0x1003
        /// </summary>
        TextureComponents = 0x1003,

        /// <summary>
        /// Original was GL_TEXTURE_INTERNAL_FORMAT = 0x1003
        /// </summary>
        TextureInternalFormat = 0x1003,

        /// <summary>
        /// Original was GL_TEXTURE_BORDER_COLOR = 0x1004
        /// </summary>
        TextureBorderColor = 0x1004,

        /// <summary>
        /// Original was GL_TEXTURE_BORDER_COLOR_NV = 0x1004
        /// </summary>
        TextureBorderColorNv = 0x1004,

        /// <summary>
        /// Original was GL_TEXTURE_BORDER = 0x1005
        /// </summary>
        TextureBorder = 0x1005,

        /// <summary>
        /// Original was GL_TEXTURE_MAG_FILTER = 0x2800
        /// </summary>
        TextureMagFilter = 0x2800,

        /// <summary>
        /// Original was GL_TEXTURE_MIN_FILTER = 0x2801
        /// </summary>
        TextureMinFilter = 0x2801,

        /// <summary>
        /// Original was GL_TEXTURE_WRAP_S = 0x2802
        /// </summary>
        TextureWrapS = 10242,

        /// <summary>
        /// Original was GL_TEXTURE_WRAP_T = 0x2803
        /// </summary>
        TextureWrapT = 0x2803,

        /// <summary>
        /// Original was GL_TEXTURE_RED_SIZE = 0x805C
        /// </summary>
        TextureRedSize = 0x805C,

        /// <summary>
        /// Original was GL_TEXTURE_GREEN_SIZE = 0x805D
        /// </summary>
        TextureGreenSize = 0x805D,

        /// <summary>
        /// Original was GL_TEXTURE_BLUE_SIZE = 0x805E
        /// </summary>
        TextureBlueSize = 0x805E,

        /// <summary>
        /// Original was GL_TEXTURE_ALPHA_SIZE = 0x805F
        /// </summary>
        TextureAlphaSize = 0x805F,

        /// <summary>
        /// Original was GL_TEXTURE_LUMINANCE_SIZE = 0x8060
        /// </summary>
        TextureLuminanceSize = 0x8060,

        /// <summary>
        /// Original was GL_TEXTURE_INTENSITY_SIZE = 0x8061
        /// </summary>
        TextureIntensitySize = 0x8061,

        /// <summary>
        /// Original was GL_TEXTURE_PRIORITY = 0x8066
        /// </summary>
        TexturePriority = 0x8066,

        /// <summary>
        /// Original was GL_TEXTURE_PRIORITY_EXT = 0x8066
        /// </summary>
        TexturePriorityExt = 0x8066,

        /// <summary>
        /// Original was GL_TEXTURE_RESIDENT = 0x8067
        /// </summary>
        TextureResident = 0x8067,

        /// <summary>
        /// Original was GL_TEXTURE_DEPTH = 0x8071
        /// </summary>
        TextureDepth = 0x8071,

        /// <summary>
        /// Original was GL_TEXTURE_DEPTH_EXT = 0x8071
        /// </summary>
        TextureDepthExt = 0x8071,

        /// <summary>
        /// Original was GL_TEXTURE_WRAP_R = 0x8072
        /// </summary>
        TextureWrapR = 0x8072,

        /// <summary>
        /// Original was GL_TEXTURE_WRAP_R_EXT = 0x8072
        /// </summary>
        TextureWrapRExt = 0x8072,

        /// <summary>
        /// Original was GL_TEXTURE_WRAP_R_OES = 0x8072
        /// </summary>
        TextureWrapROes = 0x8072,

        /// <summary>
        /// Original was GL_DETAIL_TEXTURE_LEVEL_SGIS = 0x809A
        /// </summary>
        DetailTextureLevelSgis = 0x809A,

        /// <summary>
        /// Original was GL_DETAIL_TEXTURE_MODE_SGIS = 0x809B
        /// </summary>
        DetailTextureModeSgis = 0x809B,

        /// <summary>
        /// Original was GL_DETAIL_TEXTURE_FUNC_POINTS_SGIS = 0x809C
        /// </summary>
        DetailTextureFuncPointsSgis = 0x809C,

        /// <summary>
        /// Original was GL_SHARPEN_TEXTURE_FUNC_POINTS_SGIS = 0x80B0
        /// </summary>
        SharpenTextureFuncPointsSgis = 0x80B0,

        /// <summary>
        /// Original was GL_SHADOW_AMBIENT_SGIX = 0x80BF
        /// </summary>
        ShadowAmbientSgix = 0x80BF,

        /// <summary>
        /// Original was GL_TEXTURE_COMPARE_FAIL_VALUE = 0x80BF
        /// </summary>
        TextureCompareFailValue = 0x80BF,

        /// <summary>
        /// Original was GL_DUAL_TEXTURE_SELECT_SGIS = 0x8124
        /// </summary>
        DualTextureSelectSgis = 0x8124,

        /// <summary>
        /// Original was GL_QUAD_TEXTURE_SELECT_SGIS = 0x8125
        /// </summary>
        QuadTextureSelectSgis = 0x8125,

        /// <summary>
        /// Original was GL_CLAMP_TO_BORDER = 0x812D
        /// </summary>
        ClampToBorder = 0x812D,

        /// <summary>
        /// Original was GL_CLAMP_TO_EDGE = 0x812F
        /// </summary>
        ClampToEdge = 0x812F,

        /// <summary>
        /// Original was GL_TEXTURE_4DSIZE_SGIS = 0x8136
        /// </summary>
        Texture4DsizeSgis = 0x8136,

        /// <summary>
        /// Original was GL_TEXTURE_WRAP_Q_SGIS = 0x8137
        /// </summary>
        TextureWrapQSgis = 0x8137,

        /// <summary>
        /// Original was GL_TEXTURE_MIN_LOD = 0x813A
        /// </summary>
        TextureMinLod = 0x813A,

        /// <summary>
        /// Original was GL_TEXTURE_MIN_LOD_SGIS = 0x813A
        /// </summary>
        TextureMinLodSgis = 0x813A,

        /// <summary>
        /// Original was GL_TEXTURE_MAX_LOD = 0x813B
        /// </summary>
        TextureMaxLod = 0x813B,

        /// <summary>
        /// Original was GL_TEXTURE_MAX_LOD_SGIS = 0x813B
        /// </summary>
        TextureMaxLodSgis = 0x813B,

        /// <summary>
        /// Original was GL_TEXTURE_BASE_LEVEL = 0x813C
        /// </summary>
        TextureBaseLevel = 0x813C,

        /// <summary>
        /// Original was GL_TEXTURE_BASE_LEVEL_SGIS = 0x813C
        /// </summary>
        TextureBaseLevelSgis = 0x813C,

        /// <summary>
        /// Original was GL_TEXTURE_MAX_LEVEL = 0x813D
        /// </summary>
        TextureMaxLevel = 0x813D,

        /// <summary>
        /// Original was GL_TEXTURE_MAX_LEVEL_SGIS = 0x813D
        /// </summary>
        TextureMaxLevelSgis = 0x813D,

        /// <summary>
        /// Original was GL_TEXTURE_FILTER4_SIZE_SGIS = 0x8147
        /// </summary>
        TextureFilter4SizeSgis = 0x8147,

        /// <summary>
        /// Original was GL_TEXTURE_CLIPMAP_CENTER_SGIX = 0x8171
        /// </summary>
        TextureClipmapCenterSgix = 0x8171,

        /// <summary>
        /// Original was GL_TEXTURE_CLIPMAP_FRAME_SGIX = 0x8172
        /// </summary>
        TextureClipmapFrameSgix = 0x8172,

        /// <summary>
        /// Original was GL_TEXTURE_CLIPMAP_OFFSET_SGIX = 0x8173
        /// </summary>
        TextureClipmapOffsetSgix = 0x8173,

        /// <summary>
        /// Original was GL_TEXTURE_CLIPMAP_VIRTUAL_DEPTH_SGIX = 0x8174
        /// </summary>
        TextureClipmapVirtualDepthSgix = 0x8174,

        /// <summary>
        /// Original was GL_TEXTURE_CLIPMAP_LOD_OFFSET_SGIX = 0x8175
        /// </summary>
        TextureClipmapLodOffsetSgix = 0x8175,

        /// <summary>
        /// Original was GL_TEXTURE_CLIPMAP_DEPTH_SGIX = 0x8176
        /// </summary>
        TextureClipmapDepthSgix = 0x8176,

        /// <summary>
        /// Original was GL_POST_TEXTURE_FILTER_BIAS_SGIX = 0x8179
        /// </summary>
        PostTextureFilterBiasSgix = 0x8179,

        /// <summary>
        /// Original was GL_POST_TEXTURE_FILTER_SCALE_SGIX = 0x817A
        /// </summary>
        PostTextureFilterScaleSgix = 0x817A,

        /// <summary>
        /// Original was GL_TEXTURE_LOD_BIAS_S_SGIX = 0x818E
        /// </summary>
        TextureLodBiasSSgix = 0x818E,

        /// <summary>
        /// Original was GL_TEXTURE_LOD_BIAS_T_SGIX = 0x818F
        /// </summary>
        TextureLodBiasTSgix = 0x818F,

        /// <summary>
        /// Original was GL_TEXTURE_LOD_BIAS_R_SGIX = 0x8190
        /// </summary>
        TextureLodBiasRSgix = 0x8190,

        /// <summary>
        /// Original was GL_GENERATE_MIPMAP = 0x8191
        /// </summary>
        GenerateMipmap = 0x8191,

        /// <summary>
        /// Original was GL_GENERATE_MIPMAP_SGIS = 0x8191
        /// </summary>
        GenerateMipmapSgis = 0x8191,

        /// <summary>
        /// Original was GL_TEXTURE_COMPARE_SGIX = 0x819A
        /// </summary>
        TextureCompareSgix = 0x819A,

        /// <summary>
        /// Original was GL_TEXTURE_COMPARE_OPERATOR_SGIX = 0x819B
        /// </summary>
        TextureCompareOperatorSgix = 0x819B,

        /// <summary>
        /// Original was GL_TEXTURE_LEQUAL_R_SGIX = 0x819C
        /// </summary>
        TextureLequalRSgix = 0x819C,

        /// <summary>
        /// Original was GL_TEXTURE_GEQUAL_R_SGIX = 0x8369
        /// </summary>
        TextureGequalRSgix = 0x8369,

        /// <summary>
        /// Original was GL_TEXTURE_MAX_CLAMP_S_SGIX = 0x8369
        /// </summary>
        TextureMaxClampSSgix = 0x8369,

        /// <summary>
        /// Original was GL_TEXTURE_MAX_CLAMP_T_SGIX = 0x836A
        /// </summary>
        TextureMaxClampTSgix = 0x836A,

        /// <summary>
        /// Original was GL_TEXTURE_MAX_CLAMP_R_SGIX = 0x836B
        /// </summary>
        TextureMaxClampRSgix = 0x836B,

        /// <summary>
        /// Originally was GL_TEXTURE_MAX_ANISOTROPY_EXT = 0x84FE
        /// </summary>
        MaxAnisotropy = 0x84FE,

        /// <summary>
        /// Originally was GL_MAX_TEXTURE_MAX_ANISOTROPY_EXT = 0x84FF
        /// </summary>
        MaxOfMaxAnisotropy = 0x84FF,

        /// <summary>
        /// Original was GL_TEXTURE_LOD_BIAS = 0x8501
        /// </summary>
        TextureLodBias = 0x8501,

        /// <summary>
        /// Original was GL_DEPTH_TEXTURE_MODE = 0x884B
        /// </summary>
        DepthTextureMode = 0x884B,

        /// <summary>
        /// Original was GL_TEXTURE_COMPARE_MODE = 0x884C
        /// </summary>
        TextureCompareMode = 0x884C,

        /// <summary>
        /// Original was GL_TEXTURE_COMPARE_FUNC = 0x884D
        /// </summary>
        TextureCompareFunc = 0x884D,

        /// <summary>
        /// Original was GL_TEXTURE_SWIZZLE_R = 0x8E42
        /// </summary>
        TextureSwizzleR = 0x8E42,

        /// <summary>
        /// Original was GL_TEXTURE_SWIZZLE_G = 0x8E43
        /// </summary>
        TextureSwizzleG = 0x8E43,

        /// <summary>
        /// Original was GL_TEXTURE_SWIZZLE_B = 0x8E44
        /// </summary>
        TextureSwizzleB = 0x8E44,

        /// <summary>
        /// Original was GL_TEXTURE_SWIZZLE_A = 0x8E45
        /// </summary>
        TextureSwizzleA = 0x8E45,

        /// <summary>
        /// Original was GL_TEXTURE_SWIZZLE_RGBA = 0x8E46
        /// </summary>
        TextureSwizzleRgba = 0x8E46,

        /// <summary>
        /// Original was GL_DEPTH_STENCIL_TEXTURE_MODE = 0x90EA
        /// </summary>
        DepthStencilTextureMode = 0x90EA,

        /// <summary>
        /// Original was GL_TEXTURE_TILING_EXT = 0x9580
        /// </summary>
        TextureTilingExt = 0x9580,
    }

    /// <summary>
    /// Texture target enumeration.
    /// </summary>
    internal enum TextureTarget
    {
        /// <summary>
        /// The texture 1D target (GL_TEXTURE_1D = 0x0DE0)
        /// </summary>
        Texture1D = 0x0DE0,

        /// <summary>
        /// The texture 2D target (GL_TEXTURE_2D = 0x0DE1)
        /// </summary>
        Texture2D = 0x0DE1,

        /// <summary>
        /// The proxy texture 1D target (GL_PROXY_TEXTURE_1D = 0x8063)
        /// </summary>
        ProxyTexture1D = 0x8063,

        /// <summary>
        /// The proxy texture 1D EXT target (GL_PROXY_TEXTURE_1D_EXT = 0x8063)
        /// </summary>
        ProxyTexture1DExt = 0x8063,

        /// <summary>
        /// The proxy texture 2D target (GL_PROXY_TEXTURE_2D = 0x8064)
        /// </summary>
        ProxyTexture2D = 0x8064,

        /// <summary>
        /// The proxy texture 2D EXT target (GL_PROXY_TEXTURE_2D_EXT = 0x8064)
        /// </summary>
        ProxyTexture2DExt = 0x8064,

        /// <summary>
        /// The texture 3D target (GL_TEXTURE_3D = 0x806F)
        /// </summary>
        Texture3D = 0x806F,

        /// <summary>
        /// The texture 3D EXT target (GL_TEXTURE_3D_EXT = 0x806F)
        /// </summary>
        Texture3DExt = 0x806F,

        /// <summary>
        /// The texture 3D OES target (GL_TEXTURE_3D_OES = 0x806F)
        /// </summary>
        Texture3DOes = 0x806F,

        /// <summary>
        /// The proxy texture 3D target (GL_PROXY_TEXTURE_3D = 0x8070)
        /// </summary>
        ProxyTexture3D = 0x8070,

        /// <summary>
        /// The proxy texture 3D EXT target (GL_PROXY_TEXTURE_3D_EXT = 0x8070)
        /// </summary>
        ProxyTexture3DExt = 0x8070,

        /// <summary>
        /// The detail texture 2D SGIS target (GL_DETAIL_TEXTURE_2D_SGIS = 0x8095)
        /// </summary>
        DetailTexture2DSgis = 0x8095,

        /// <summary>
        /// The texture 4D SGIS target (GL_TEXTURE_4D_SGIS = 0x8134)
        /// </summary>
        Texture4DSgis = 0x8134,

        /// <summary>
        /// The proxy texture 4D SGIS target (GL_PROXY_TEXTURE_4D_SGIS = 0x8135)
        /// </summary>
        ProxyTexture4DSgis = 0x8135,

        /// <summary>
        /// The texture rectangle target (GL_TEXTURE_RECTANGLE = 0x84F5)
        /// </summary>
        TextureRectangle = 0x84F5,

        /// <summary>
        /// The proxy texture rectangle target (GL_PROXY_TEXTURE_RECTANGLE = 0x84F7)
        /// </summary>
        ProxyTextureRectangle = 0x84F7,

        /// <summary>
        /// The proxy texture rectangle ARB target (GL_PROXY_TEXTURE_RECTANGLE_ARB = 0x84F7)
        /// </summary>
        ProxyTextureRectangleArb = 0x84F7,

        /// <summary>
        /// The proxy texture rectangle NV target (GL_PROXY_TEXTURE_RECTANGLE_NV = 0x84F7)
        /// </summary>
        ProxyTextureRectangleNv = 0x84F7,

        /// <summary>
        /// The texture cube map target (GL_TEXTURE_CUBE_MAP = 0x8513)
        /// </summary>
        TextureCubeMap = 0x8513,

        /// <summary>
        /// The binding for cube map texture target (GL_TEXTURE_BINDING_CUBE_MAP = 0x8514)
        /// </summary>
        TextureBindingCubeMap = 0x8514,

        /// <summary>
        /// The texture cube map positive X target (GL_TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515)
        /// </summary>
        TextureCubeMapPositiveX = 0x8515,

        /// <summary>
        /// The texture cube map negative X target (GL_TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516)
        /// </summary>
        TextureCubeMapNegativeX = 0x8516,

        /// <summary>
        /// The texture cube map positive Y target (GL_TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517)
        /// </summary>
        TextureCubeMapPositiveY = 0x8517,

        /// <summary>
        /// The texture cube map negative Y target (GL_TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518)
        /// </summary>
        TextureCubeMapNegativeY = 0x8518,

        /// <summary>
        /// The texture cube map positive Z target (GL_TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519)
        /// </summary>
        TextureCubeMapPositiveZ = 0x8519,

        /// <summary>
        /// The texture cube map negative Z target (GL_TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A)
        /// </summary>
        TextureCubeMapNegativeZ = 0x851A,

        /// <summary>
        /// The proxy cube map texture target (GL_PROXY_TEXTURE_CUBE_MAP = 0x851B)
        /// </summary>
        ProxyTextureCubeMap = 0x851B,

        /// <summary>
        /// The proxy cube map texture ARB target (GL_PROXY_TEXTURE_CUBE_MAP_ARB = 0x851B)
        /// </summary>
        ProxyTextureCubeMapArb = 0x851B,

        /// <summary>
        /// The proxy cube map texture EXT target (GL_PROXY_TEXTURE_CUBE_MAP_EXT = 0x851B)
        /// </summary>
        ProxyTextureCubeMapExt = 0x851B,

        /// <summary>
        /// The texture 1D array target (GL_TEXTURE_1D_ARRAY = 0x8C18)
        /// </summary>
        Texture1DArray = 0x8C18,

        /// <summary>
        /// The proxy texture 1D array target (GL_PROXY_TEXTURE_1D_ARRAY = 0x8C19)
        /// </summary>
        ProxyTexture1DArray = 0x8C19,

        /// <summary>
        /// The proxy texture 1D array EXT target (GL_PROXY_TEXTURE_1D_ARRAY_EXT = 0x8C19)
        /// </summary>
        ProxyTexture1DArrayExt = 0x8C19,

        /// <summary>
        /// The texture 2D array target (GL_TEXTURE_2D_ARRAY = 0x8C1A)
        /// </summary>
        Texture2DArray = 0x8C1A,

        /// <summary>
        /// The proxy texture 2D array target (GL_PROXY_TEXTURE_2D_ARRAY = 0x8C1B)
        /// </summary>
        ProxyTexture2DArray = 0x8C1B,

        /// <summary>
        /// The proxy texture 2D array EXT target (GL_PROXY_TEXTURE_2D_ARRAY_EXT = 0x8C1B)
        /// </summary>
        ProxyTexture2DArrayExt = 0x8C1B,

        /// <summary>
        /// The texture buffer target (GL_TEXTURE_BUFFER = 0x8C2A)
        /// </summary>
        TextureBuffer = 0x8C2A,

        /// <summary>
        /// The cube map texture array target (GL_TEXTURE_CUBE_MAP_ARRAY = 0x9009)
        /// </summary>
        TextureCubeMapArray = 0x9009,

        /// <summary>
        /// The cube map texture array ARB target (GL_TEXTURE_CUBE_MAP_ARRAY_ARB = 0x9009)
        /// </summary>
        TextureCubeMapArrayArb = 0x9009,

        /// <summary>
        /// The cube map texture array EXT target (GL_TEXTURE_CUBE_MAP_ARRAY_EXT = 0x9009)
        /// </summary>
        TextureCubeMapArrayExt = 0x9009,

        /// <summary>
        /// The cube map texture array OES target (GL_TEXTURE_CUBE_MAP_ARRAY_OES = 0x9009)
        /// </summary>
        TextureCubeMapArrayOes = 0x9009,

        /// <summary>
        /// The proxy cube map texture array target (GL_PROXY_TEXTURE_CUBE_MAP_ARRAY = 0x900B)
        /// </summary>
        ProxyTextureCubeMapArray = 0x900B,

        /// <summary>
        /// The proxy cube map texture array ARB target (GL_PROXY_TEXTURE_CUBE_MAP_ARRAY_ARB = 0x900B)
        /// </summary>
        ProxyTextureCubeMapArrayArb = 0x900B,

        /// <summary>
        /// The multi-sample texture 2D target (GL_TEXTURE_2D_MULTISAMPLE = 0x9100)
        /// </summary>
        Texture2DMultisample = 0x9100,

        /// <summary>
        /// The proxy multi-sample texture 2D target (GL_PROXY_TEXTURE_2D_MULTISAMPLE = 0x9101)
        /// </summary>
        ProxyTexture2DMultisample = 0x9101,

        /// <summary>
        /// The multi-sample texture 2D array target (GL_TEXTURE_2D_MULTISAMPLE_ARRAY = 0x9102)
        /// </summary>
        Texture2DMultisampleArray = 0x9102,

        /// <summary>
        /// The proxy multi-sample texture 2D array target (GL_PROXY_TEXTURE_2D_MULTISAMPLE_ARRAY = 0x9103)
        /// </summary>
        ProxyTexture2DMultisampleArray = 0x9103
    }

    /// <summary>
    /// Texture unit enumeration.
    /// </summary>
    internal enum TextureUnit : int
    {
        /// <summary>
        /// Original was GL_TEXTURE0 = 0x84C0
        /// </summary>
        Texture0 = 0x84C0,

        /// <summary>
        /// Original was GL_TEXTURE1 = 0x84C1
        /// </summary>
        Texture1 = 0x84C1,

        /// <summary>
        /// Original was GL_TEXTURE2 = 0x84C2
        /// </summary>
        Texture2 = 0x84C2,

        /// <summary>
        /// Original was GL_TEXTURE3 = 0x84C3
        /// </summary>
        Texture3 = 0x84C3,

        /// <summary>
        /// Original was GL_TEXTURE4 = 0x84C4
        /// </summary>
        Texture4 = 0x84C4,

        /// <summary>
        /// Original was GL_TEXTURE5 = 0x84C5
        /// </summary>
        Texture5 = 0x84C5,

        /// <summary>
        /// Original was GL_TEXTURE6 = 0x84C6
        /// </summary>
        Texture6 = 0x84C6,

        /// <summary>
        /// Original was GL_TEXTURE7 = 0x84C7
        /// </summary>
        Texture7 = 0x84C7,

        /// <summary>
        /// Original was GL_TEXTURE8 = 0x84C8
        /// </summary>
        Texture8 = 0x84C8,

        /// <summary>
        /// Original was GL_TEXTURE9 = 0x84C9
        /// </summary>
        Texture9 = 0x84C9,

        /// <summary>
        /// Original was GL_TEXTURE10 = 0x84CA
        /// </summary>
        Texture10 = 0x84CA,

        /// <summary>
        /// Original was GL_TEXTURE11 = 0x84CB
        /// </summary>
        Texture11 = 0x84CB,

        /// <summary>
        /// Original was GL_TEXTURE12 = 0x84CC
        /// </summary>
        Texture12 = 0x84CC,

        /// <summary>
        /// Original was GL_TEXTURE13 = 0x84CD
        /// </summary>
        Texture13 = 0x84CD,

        /// <summary>
        /// Original was GL_TEXTURE14 = 0x84CE
        /// </summary>
        Texture14 = 0x84CE,

        /// <summary>
        /// Original was GL_TEXTURE15 = 0x84CF
        /// </summary>
        Texture15 = 0x84CF,

        /// <summary>
        /// Original was GL_TEXTURE16 = 0x84D0
        /// </summary>
        Texture16 = 0x84D0,

        /// <summary>
        /// Original was GL_TEXTURE17 = 0x84D1
        /// </summary>
        Texture17 = 0x84D1,

        /// <summary>
        /// Original was GL_TEXTURE18 = 0x84D2
        /// </summary>
        Texture18 = 0x84D2,

        /// <summary>
        /// Original was GL_TEXTURE19 = 0x84D3
        /// </summary>
        Texture19 = 0x84D3,

        /// <summary>
        /// Original was GL_TEXTURE20 = 0x84D4
        /// </summary>
        Texture20 = 0x84D4,

        /// <summary>
        /// Original was GL_TEXTURE21 = 0x84D5
        /// </summary>
        Texture21 = 0x84D5,

        /// <summary>
        /// Original was GL_TEXTURE22 = 0x84D6
        /// </summary>
        Texture22 = 0x84D6,

        /// <summary>
        /// Original was GL_TEXTURE23 = 0x84D7
        /// </summary>
        Texture23 = 0x84D7,

        /// <summary>
        /// Original was GL_TEXTURE24 = 0x84D8
        /// </summary>
        Texture24 = 0x84D8,

        /// <summary>
        /// Original was GL_TEXTURE25 = 0x84D9
        /// </summary>
        Texture25 = 0x84D9,

        /// <summary>
        /// Original was GL_TEXTURE26 = 0x84DA
        /// </summary>
        Texture26 = 0x84DA,

        /// <summary>
        /// Original was GL_TEXTURE27 = 0x84DB
        /// </summary>
        Texture27 = 0x84DB,

        /// <summary>
        /// Original was GL_TEXTURE28 = 0x84DC
        /// </summary>
        Texture28 = 0x84DC,

        /// <summary>
        /// Original was GL_TEXTURE29 = 0x84DD
        /// </summary>
        Texture29 = 0x84DD,

        /// <summary>
        /// Original was GL_TEXTURE30 = 0x84DE
        /// </summary>
        Texture30 = 0x84DE,

        /// <summary>
        /// Original was GL_TEXTURE31 = 0x84DF
        /// </summary>
        Texture31 = 0x84DF
    }

    /// <summary>
    /// Texture wrap mode enumeration.
    /// </summary>
    internal enum TextureWrapMode
    {
        /// <summary>
        /// Original was GL_REPEAT = 0x2901
        /// </summary>
        Repeat = 0x2901,

        /// <summary>
        /// Original was GL_CLAMP_TO_BORDER = 0x812D
        /// </summary>
        ClampToBorder = 0x812D,

        /// <summary>
        /// Original was GL_CLAMP_TO_BORDER_ARB = 0x812D
        /// </summary>
        ClampToBorderArb = 0x812D,

        /// <summary>
        /// Original was GL_CLAMP_TO_BORDER_NV = 0x812D
        /// </summary>
        ClampToBorderNv = 0x812D,

        /// <summary>
        /// Original was GL_CLAMP_TO_BORDER_SGIS = 0x812D
        /// </summary>
        ClampToBorderSgis = 0x812D,

        /// <summary>
        /// Original was GL_CLAMP_TO_EDGE = 0x812F
        /// </summary>
        ClampToEdge = 0x812F,

        /// <summary>
        /// Original was GL_CLAMP_TO_EDGE_SGIS = 0x812F
        /// </summary>
        ClampToEdgeSgis = 0x812F,

        /// <summary>
        /// Original was GL_MIRRORED_REPEAT = 0x8370
        /// </summary>
        MirroredRepeat = 0x8370
    }

    /// <summary>
    /// Vertex attribute pointer type enumeration.
    /// </summary>
    internal enum VertexAttributePointerType
    {
        /// <summary>
        /// Original was GL_BYTE = 0x1400
        /// </summary>
        Byte = 0x1400,

        /// <summary>
        /// Original was GL_UNSIGNED_BYTE = 0x1401
        /// </summary>
        UnsignedByte = 0x1401,

        /// <summary>
        /// Original was GL_SHORT = 0x1402
        /// </summary>
        Short = 0x1402,

        /// <summary>
        /// Original was GL_UNSIGNED_SHORT = 0x1403
        /// </summary>
        UnsignedShort = 0x1403,

        /// <summary>
        /// Original was GL_INT = 0x1404
        /// </summary>
        Int = 0x1404,

        /// <summary>
        /// Original was GL_UNSIGNED_INT = 0x1405
        /// </summary>
        UnsignedInt = 0x1405,

        /// <summary>
        /// Original was GL_FLOAT = 0x1406
        /// </summary>
        Float = 0x1406,

        /// <summary>
        /// Original was GL_DOUBLE = 0x140A
        /// </summary>
        Double = 0x140A,

        /// <summary>
        /// Original was GL_HALF_FLOAT = 0x140B
        /// </summary>
        HalfFloat = 0x140B,

        /// <summary>
        /// Original was GL_FIXED = 0x140C
        /// </summary>
        Fixed = 0x140C,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_2_10_10_10_REV = 0x8368
        /// </summary>
        UnsignedInt2101010Rev = 0x8368,

        /// <summary>
        /// Original was GL_UNSIGNED_INT_10F_11F_11F_REV = 0x8C3B
        /// </summary>
        UnsignedInt10F11F11FRev = 0x8C3B,

        /// <summary>
        /// Original was GL_INT_2_10_10_10_REV = 0x8D9F
        /// </summary>
        Int2101010Rev = 0x8D9F
    }
}
