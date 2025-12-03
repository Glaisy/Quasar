//-----------------------------------------------------------------------
// <copyright file="AL.Delegates.cs" company="Space Development">
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

namespace Quasar.OpenAL.Api
{
    /// <summary>
    /// OpenAL function wrapper delegate types.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed.")]
    internal static unsafe partial class AL
    {
        private delegate void alDeleteBuffers(int size, int* ids);
        private delegate void alDeleteSources(int size, int* ids);
        private delegate void alGenBuffers(int size, int* ids);
        private delegate void alGenSources(int size, int* ids);
        private delegate void alGetListenerf(ListenerProperty property, float* value);
        private delegate void alGetListener3f(ListenerProperty property, float* x, float* y, float* z);
        private delegate void alGetListenerfv(ListenerProperty property, float* values);

        private delegate byte* alcGetString(IntPtr deviceId, StringType stringType);
        private delegate int alcGetIntegerv(IntPtr deviceId, IntegerType intergerType, int bufferSize, int* values);
        private delegate bool alcIsExtensionPresent(IntPtr deviceId, string extensionName);

        public delegate void alBufferData(int bufferId, BufferFormat format, IntPtr data, int size, int sampleRate);
        public delegate void alDistanceModel(DistanceModel distanceModel);
        public delegate int alGetError();
        public delegate void alListenerf(ListenerProperty property, float value);
        public delegate void alListener3f(ListenerProperty property, float x, float y, float z);
        public delegate void alListenerfv(ListenerProperty property, float* values);
        public delegate void alSourcef(int sourceId, SourceProperty property, float value);
        public delegate void alSource3f(int sourceId, SourceProperty property, float x, float y, float z);
        public delegate void alSourcei(int sourceId, SourceProperty property, int value);
        public delegate void alSourcePlay(int sourceId);
        public delegate void alSourceStop(int sourceId);

        public delegate void alcCloseDevice(IntPtr deviceId);
        public delegate IntPtr alcCreateContext(IntPtr deviceId, IntPtr attributes);
        public delegate void alcDestroyContext(IntPtr context);
        public delegate void alcMakeContextCurrent(IntPtr context);
        public delegate IntPtr alcOpenDevice(string name);
    }
}
