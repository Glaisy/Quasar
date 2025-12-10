//-----------------------------------------------------------------------
// <copyright file="AL.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Diagnostics;

using Quasar.Utilities;

using Space.Core;

namespace Quasar.OpenAL.Api
{
    /// <summary>
    /// OpenAL function wrapper.
    /// </summary>
    internal static unsafe partial class AL
    {
        private static alDeleteBuffers deleteBuffers;
        private static alDeleteSources deleteSources;
        private static alGenBuffers genBuffers;
        private static alGenSources genSources;
        private static alGetListenerf getListenerf;
        private static alGetListener3f getListener3f;
        private static alGetListenerfv getListenerfv;
        private static alListenerfv listenerfv;
        private static alcGetIntegerv getIntegerv;
        private static alcGetString getString;
        private static alcIsExtensionPresent isExtensionPresent;


        public static alBufferData BufferData;
        public static alcCloseDevice CloseDevice;
        public static alcCreateContext CreateContext;
        public static alcDestroyContext DestroyContext;
        public static alDistanceModel DistanceModel;
        public static alGetError GetError;
        public static alListenerf Listenerf;
        public static alListener3f Listener3f;
        public static alcMakeContextCurrent MakeContextCurrent;
        public static alcOpenDevice OpenDevice;
        public static alSourcef Sourcef;
        public static alSource3f Source3f;
        public static alSourcei Sourcei;
        public static alSourcePlay SourcePlay;
        public static alSourceStop SourceStop;

        /// <summary>
        /// Checks the error of the last function call.
        /// </summary>
        [Conditional("DEBUG")]
        public static void CheckErrors()
        {
            var errorCode = GetError();
            if (errorCode != 0)
            {
                throw new OpenALException($"OpenAL error occured: {errorCode}");
            }
        }

        /// <summary>
        /// Initializes the OpenGL function wrapper by the specified function provider.
        /// </summary>
        /// <param name="functionProvider">The function provider.</param>
        public static void Initialize(IInteropFunctionProvider functionProvider)
        {
            Assertion.ThrowIfNull(functionProvider, nameof(functionProvider));

            // initialize private OpenAL functions
            deleteBuffers = functionProvider.GetFunction<alDeleteBuffers>();
            deleteSources = functionProvider.GetFunction<alDeleteSources>();
            genBuffers = functionProvider.GetFunction<alGenBuffers>();
            genSources = functionProvider.GetFunction<alGenSources>();
            getListenerf = functionProvider.GetFunction<alGetListenerf>();
            getListener3f = functionProvider.GetFunction<alGetListener3f>();
            getListenerfv = functionProvider.GetFunction<alGetListenerfv>();
            getString = functionProvider.GetFunction<alcGetString>();
            getIntegerv = functionProvider.GetFunction<alcGetIntegerv>();
            listenerfv = functionProvider.GetFunction<alListenerfv>();
            isExtensionPresent = functionProvider.GetFunction<alcIsExtensionPresent>();

            // initialize the rest of the OpenAL functions
            BufferData = functionProvider.GetFunction<alBufferData>();
            CloseDevice = functionProvider.GetFunction<alcCloseDevice>();
            CreateContext = functionProvider.GetFunction<alcCreateContext>();
            DestroyContext = functionProvider.GetFunction<alcDestroyContext>();
            DistanceModel = functionProvider.GetFunction<alDistanceModel>();
            GetError = functionProvider.GetFunction<alGetError>();
            Listenerf = functionProvider.GetFunction<alListenerf>();
            Listener3f = functionProvider.GetFunction<alListener3f>();
            MakeContextCurrent = functionProvider.GetFunction<alcMakeContextCurrent>();
            OpenDevice = functionProvider.GetFunction<alcOpenDevice>();
            Sourcef = functionProvider.GetFunction<alSourcef>();
            Source3f = functionProvider.GetFunction<alSource3f>();
            Sourcei = functionProvider.GetFunction<alSourcei>();
            SourcePlay = functionProvider.GetFunction<alSourcePlay>();
            SourceStop = functionProvider.GetFunction<alSourceStop>();
        }
    }
}
