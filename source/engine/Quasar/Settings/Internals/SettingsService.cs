//-----------------------------------------------------------------------
// <copyright file="SettingsService.cs" company="Space Development">
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;

using Microsoft.Extensions.DependencyInjection;

using Space.Core;
using Space.Core.DependencyInjection;
using Space.Core.Diagnostics;
using Space.Core.IO;

namespace Quasar.Settings.Internals
{
    /// <summary>
    /// Settings service implementation.
    /// </summary>
    /// <seealso cref="ISettingsService" />
    [Export(typeof(ISettingsService))]
    [Singleton]
    internal sealed class SettingsService : ISettingsService
    {
        private const string DefaultSettingsFilePath = "./settings.cfg";


        private readonly IFileSystemHelper fileSystemHelper;
        private readonly ILogger logger;
        private readonly ReaderWriterLockSlim settingsLock = new ReaderWriterLockSlim();
        private readonly Dictionary<Type, SettingsEntry> settingsEntries = new Dictionary<Type, SettingsEntry>();
        private readonly Dictionary<string, SettingsEntry> settingsEntriesByName = new Dictionary<string, SettingsEntry>();
        private readonly HashSet<string> scannedAssemblyNames = new HashSet<string>();
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = true,
        };
        private readonly string settingsFilePath;


        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService" /> class.
        /// </summary>
        /// <param name="fileSystemHelper">The file system helper.</param>
        /// <param name="environmentInformation">The environment information.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public SettingsService(
            IFileSystemHelper fileSystemHelper,
            IEnvironmentInformation environmentInformation,
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory)
        {
            this.fileSystemHelper = fileSystemHelper;

            logger = loggerFactory.Create<SettingsService>();

            var settingsServiceConfiguration = serviceProvider.GetService<SettingsServiceConfiguration>();
            settingsFilePath = GetSettingsFilePath(fileSystemHelper, environmentInformation, settingsServiceConfiguration);

            ScanAssemblyForSettingsInternal(GetType().Assembly);
            var assembliesToScan = settingsServiceConfiguration?.Assemblies ?? Array.Empty<Assembly>();
            foreach (var assembly in assembliesToScan)
            {
                ScanAssemblyForSettingsInternal(assembly);
            }
        }


        /// <inheritdoc/>
        public T Get<T>()
            where T : ISettings
        {
            logger.TraceMethodStart();

            T value = default;
            try
            {
                settingsLock.EnterReadLock();

                var settingsEntry = GetEntry<T>();
                if (settingsEntry != null)
                {
                    value = (T)settingsEntry.Value;
                }
            }
            finally
            {
                settingsLock.ExitReadLock();
            }

            logger.TraceMethodEnd();
            return value;
        }

        /// <inheritdoc/>
        public void Load()
        {
            logger.TraceMethodStart();
            logger.Info($"Loading settings from: '{settingsFilePath}'.");

            try
            {
                settingsLock.EnterWriteLock();

                List<SettingsSerializationEntry> serializationEntries;
                using (var stream = File.Open(settingsFilePath, FileMode.Open, FileAccess.Read))
                {
                    serializationEntries = JsonSerializer.Deserialize<List<SettingsSerializationEntry>>(stream, jsonSerializerOptions);
                }

                foreach (var serializationEntry in serializationEntries)
                {
                    try
                    {
                        if (serializationEntry.Value == null)
                        {
                            continue;
                        }

                        var settingsTypeName = GetSettingsTypeName(serializationEntry.Value);
                        var settingsEntry = GetEntryByName(settingsTypeName);
                        if (settingsEntry == null)
                        {
                            continue;
                        }

                        settingsEntry.SetValue(serializationEntry.Value);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, $"Unable to load settings of type name '{serializationEntry.TypeName}'. Skipped.");
                    }
                }
            }
            catch
            {
                logger.Info($"Settings file is corrupted/not found '{settingsFilePath}'. Saving defaults.");
                SetDefaultsInternal();

                try
                {
                    SaveInternal();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Unable to save settings to '{settingsFilePath}'.");
                }
            }
            finally
            {
                settingsLock.ExitWriteLock();
            }

            logger.TraceMethodEnd();
        }

        /// <inheritdoc/>
        public void ScanAssembliesForSettings(params Assembly[] assemblies)
        {
            logger.TraceMethodStart();

            try
            {
                settingsLock.EnterWriteLock();

                foreach (var assembly in assemblies)
                {
                    ScanAssemblyForSettingsInternal(assembly);
                }
            }
            finally
            {
                settingsLock.ExitWriteLock();
            }

            logger.TraceMethodEnd();
        }

        /// <inheritdoc/>
        public void Save(bool createBackup = false)
        {
            logger.TraceMethodStart();
            logger.Info($"Saving settings to: '{settingsFilePath}'.");

            try
            {
                settingsLock.EnterReadLock();

                if (createBackup)
                {
                    fileSystemHelper.CreateBackup(settingsFilePath);
                }

                SaveInternal();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Unable to save settings to '{settingsFilePath}'.");
            }
            finally
            {
                settingsLock.ExitReadLock();
            }

            logger.TraceMethodEnd();
        }

        /// <inheritdoc/>
        public void SetDefaults()
        {
            logger.TraceMethodStart();
            logger.Info($"Setting the defaults for all settings.");

            try
            {
                settingsLock.EnterWriteLock();

                SetDefaultsInternal();
            }
            finally
            {
                settingsLock.ExitWriteLock();
            }

            logger.TraceMethodEnd();
        }

        /// <inheritdoc/>
        public void SetDefaults<T>()
            where T : ISettings
        {
            logger.TraceMethodStart();
            logger.Info($"Setting the defaults for {typeof(T).FullName} settings.");

            try
            {
                settingsLock.EnterWriteLock();

                // find settings entry by the type.
                var settingsEntry = GetEntry<T>();
                if (settingsEntry == null)
                {
                    return;
                }

                settingsEntry.SetDefaultValue();
            }
            finally
            {
                settingsLock.ExitWriteLock();
            }

            logger.TraceMethodEnd();
        }

        /// <inheritdoc/>
        public IDisposable Subscribe<T>(IObserver<T> observer)
            where T : ISettings
        {
            logger.TraceMethodStart();
            ArgumentNullException.ThrowIfNull(observer, nameof(observer));

            IDisposable subscription;
            try
            {
                settingsLock.EnterReadLock();

                // find slot by the interface type.
                var entry = GetEntry<T>();
                if (entry == null)
                {
                    throw new ArgumentOutOfRangeException($"Non-registered settings type: '{typeof(T).FullName}'.");
                }

                subscription = entry.Changed.Subscribe(observer);
            }
            finally
            {
                settingsLock.ExitReadLock();
            }

            logger.TraceMethodEnd();
            return subscription;
        }

        /// <inheritdoc/>
        public void Update<T>(T settings)
            where T : ISettings
        {
            logger.TraceMethodStart();

            ArgumentNullException.ThrowIfNull(settings, nameof(settings));
            logger.Info($"Updating settings for <{typeof(T).Name}>.");

            try
            {
                settingsLock.EnterWriteLock();

                var entry = GetEntry<T>();
                if (entry == null)
                {
                    return;
                }

                entry.SetValue(settings);
            }
            finally
            {
                settingsLock.ExitWriteLock();
            }

            logger.TraceMethodEnd();
        }


        private SettingsEntry<T> GetEntry<T>()
            where T : ISettings
        {
            var key = typeof(T);
            if (!settingsEntries.TryGetValue(key, out var settingsEntry))
            {
                logger.Warning($"Settings type <{key}> is not registered. Skipped.");
            }

            return (SettingsEntry<T>)settingsEntry;
        }

        private SettingsEntry GetEntryByName(string name)
        {
            if (!settingsEntriesByName.TryGetValue(name, out var settingsEntry))
            {
                logger.Warning($"Settings with type <{name}> is not registered. Skipped.");
            }

            return settingsEntry;
        }

        private static string GetSettingsFilePath(
            IFileSystemHelper fileSystemHelper,
            IEnvironmentInformation environmentInformation,
            SettingsServiceConfiguration settingsServiceConfiguration)
        {
            var settingsFilePath = settingsServiceConfiguration?.SettingsFilePath;
            if (String.IsNullOrEmpty(settingsFilePath))
            {
                settingsFilePath = DefaultSettingsFilePath;
            }

            if (!Path.IsPathRooted(settingsFilePath))
            {
                settingsFilePath = Path.Combine(environmentInformation.BaseDirectory, settingsFilePath);
            }

            var settingsDirectory = Path.GetDirectoryName(settingsFilePath);
            fileSystemHelper.EnsureDirectoryExists(settingsDirectory);

            return settingsFilePath;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetSettingsTypeName(ISettings settings)
        {
            return GetSettingsTypeName(settings.GetType());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetSettingsTypeName(Type settingsType)
        {
            return settingsType.FullName;
        }


        private void SaveInternal()
        {
            var serializationEntries = settingsEntries.Values.Select(x =>
            {
                var typeName = GetSettingsTypeName(x.Value);
                return new SettingsSerializationEntry(typeName, x.Value);
            });

            using (var stream = File.Create(settingsFilePath))
            {
                JsonSerializer.Serialize(stream, serializationEntries, jsonSerializerOptions);
            }
        }

        private void ScanAssemblyForSettingsInternal(Assembly assembly)
        {
            var assemblyName = assembly.GetName().FullName;
            if (scannedAssemblyNames.Contains(assemblyName))
            {
                return;
            }

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var settingsEntry = TryCreateSettingsEntryForType(type, out var registeredType);
                if (settingsEntry == null)
                {
                    continue;
                }

                var settingsTypeName = GetSettingsTypeName(type);
                settingsEntriesByName.Add(settingsTypeName, settingsEntry);
                settingsEntries.Add(registeredType, settingsEntry);
            }

            scannedAssemblyNames.Add(assemblyName);
        }

        private void SetDefaultsInternal()
        {
            foreach (var settingsEntry in settingsEntries.Values)
            {
                settingsEntry.SetDefaultValue();
            }
        }

        private SettingsEntry TryCreateSettingsEntryForType(Type type, out Type registeredType)
        {
            registeredType = null;

            // validate the type
            if (!typeof(SettingsBase).IsAssignableFrom(type))
            {
                return null;
            }

            var settingsAttribute = type.GetCustomAttribute<SettingsAttribute>();
            if (settingsAttribute == null)
            {
                return null;
            }

            registeredType = settingsAttribute.RegisteredType ?? type;
            if (!typeof(ISettings).IsAssignableFrom(registeredType))
            {
                logger.Warning($"Invalid registered type '{registeredType.FullName}' " +
                    $"for '{type.FullName}' in the SettingsAttribute. Skipped.");
                return null;
            }

            if (type.GetConstructor(Type.EmptyTypes) == null)
            {
                logger.Warning($"Settings type has no default constructor <{registeredType.FullName}, " +
                    $"{type.FullName}>. Skipped");
                return null;
            }

            // create settings entry for the type
            var settingsEntryType = typeof(SettingsEntry<>).MakeGenericType(registeredType);
            var initialValue = Activator.CreateInstance(type);
            return (SettingsEntry)Activator.CreateInstance(settingsEntryType, initialValue);
        }
    }
}
