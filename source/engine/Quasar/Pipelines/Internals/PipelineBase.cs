//-----------------------------------------------------------------------
// <copyright file="PipelineBase.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Collections;
using Quasar.Extensions;
using Quasar.Settings;

using Space.Core.Diagnostics;

namespace Quasar.Pipelines.Internals
{
    /// <summary>
    /// Represents an abstract base class for engine pipelines.
    /// </summary>
    /// <typeparam name="TStage">The pipeline stage type.</typeparam>
    internal abstract class PipelineBase<TStage>
        where TStage : PipelineStageBase
    {
        private readonly IServiceProvider serviceProvider;


        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineBase{TStage}" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        protected PipelineBase(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            Name = GetType().Name;

            SettingsService = serviceProvider.GetRequiredService<ISettingsService>();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            Logger = loggerFactory.Create(Name);
        }


        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }


        /// <summary>
        /// Executes the processing steps.
        /// </summary>
        public void Execute()
        {
            try
            {
                Logger.TraceMethodStart();

                OnExecute();
            }
            finally
            {
                Logger.TraceMethodEnd();
            }
        }

        /// <summary>
        /// Start the pipeline state and stages.
        /// </summary>
        public void Start()
        {
            try
            {
                Logger.TraceMethodStart();

                var unorderedStages = serviceProvider.GetServices<TStage>();
                Stages = BuildOrderedStageList(unorderedStages);

                OnStart(serviceProvider);
            }
            finally
            {
                Logger.TraceMethodEnd();
            }
        }

        /// <summary>
        /// Shuts down the pipeline instance.
        /// </summary>
        public void Shutdown()
        {
            try
            {
                Logger.TraceMethodStart();

                OnShutdown();
            }
            finally
            {
                Logger.TraceMethodEnd();
            }
        }


        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the settings service.
        /// </summary>
        protected ISettingsService SettingsService { get; }

        /// <summary>
        /// Gets the stages.
        /// </summary>
        protected List<TStage> Stages { get; private set; }


        /// <summary>
        /// Execute event handler.
        /// </summary>
        protected abstract void OnExecute();

        /// <summary>
        /// Start event handler.
        /// </summary>
        /// <param name="service">The service.</param>
        protected virtual void OnStart(IServiceProvider service)
        {
            foreach (var stage in Stages)
            {
                stage.Start(serviceProvider);
            }
        }

        /// <summary>
        /// Shutdown event handler.
        /// </summary>
        protected virtual void OnShutdown()
        {
            for (var i = Stages.Count - 1; i >= 0; i--)
            {
                Stages[i].Shutdown();
            }
        }


        private static List<TStage> BuildOrderedStageList(IEnumerable<TStage> unorderedStages)
        {
            // build stage dependency node table
            var dependencyNodes = new Dictionary<Type, DependencyNode<TStage>>();
            foreach (var stage in unorderedStages)
            {
                var stageType = stage.GetType();
                var stageDependencyNode = GetOrCreateDependencyNode(dependencyNodes, stageType, stage);

                // "BEFORE" dependency nodes
                var beforeAttributes = stageType.GetCustomAttributes<ExecuteBeforeAttribute>();
                foreach (var beforeAttribute in beforeAttributes)
                {
                    var beforeItem = unorderedStages.FirstOrDefault(x => x.GetType() == beforeAttribute.ExecutedBefore);
                    if (beforeItem == null)
                    {
                        continue;
                    }

                    var beforeDependencyNode = GetOrCreateDependencyNode(dependencyNodes, beforeItem.GetType(), beforeItem);
                    beforeDependencyNode.AddDependencyNode(stageDependencyNode);
                }

                // "AFTER" dependency nodes
                var afterAttributes = stageType.GetCustomAttributes<ExecuteAfterAttribute>();
                foreach (var afterAttribute in afterAttributes)
                {
                    // find the pipeline which should be executed befpre the current pipeline
                    var afterItem = unorderedStages.FirstOrDefault(x => x.GetType() == afterAttribute.ExecutedAfter);
                    if (afterItem == null)
                    {
                        continue;
                    }

                    var afterDependencyNode = GetOrCreateDependencyNode(dependencyNodes, afterItem.GetType(), afterItem);
                    stageDependencyNode.AddDependencyNode(afterDependencyNode);
                }
            }

            // sort dependencies
            return dependencyNodes.Values.Sort(true);
        }

        private static DependencyNode<TStage> GetOrCreateDependencyNode(
            Dictionary<Type, DependencyNode<TStage>> dependencyNodes,
            Type itemType,
            TStage item)
        {
            if (!dependencyNodes.TryGetValue(itemType, out var dependencyNode))
            {
                dependencyNode = new DependencyNode<TStage>(item);
                dependencyNodes.Add(itemType, dependencyNode);
            }

            return dependencyNode;
        }
    }
}
