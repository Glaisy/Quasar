//-----------------------------------------------------------------------
// <copyright file="AudioUpdatePipelineStage.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using Quasar.Pipelines;

using Space.Core.DependencyInjection;

namespace Quasar.Audio.Internals.Pipeline
{
    /// <summary>
    /// Audio system's update pipeline stage implementation.
    /// </summary>
    /// <seealso cref="UpdatePipelineStageBase" />
    [Export(typeof(UpdatePipelineStageBase), nameof(AudioUpdatePipelineStage))]
    [ExecuteAfter(typeof(DispatcherUpdatePipelineStage))]
    [Singleton]
    internal sealed class AudioUpdatePipelineStage : UpdatePipelineStageBase
    {
        private readonly IAudioDeviceContextFactory audioDeviceContextFactory;
        private IAudioDeviceContext audioDeviceContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="AudioUpdatePipelineStage" /> class.
        /// </summary>
        /// <param name="audioDeviceContextFactory">The audio device context factory.</param>
        public AudioUpdatePipelineStage(IAudioDeviceContextFactory audioDeviceContextFactory)
        {
            this.audioDeviceContextFactory = audioDeviceContextFactory;
        }


        /// <inheritdoc/>
        protected override void OnExecute()
        {
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            audioDeviceContext = audioDeviceContextFactory.Create(AudioPlatform.OpenAL);
        }
    }
}
