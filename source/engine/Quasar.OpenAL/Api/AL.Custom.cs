//-----------------------------------------------------------------------
// <copyright file="AL.Custom.cs" company="Space Development">
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

using Quasar.Utilities;

namespace Quasar.OpenAL.Api
{
    /// <summary>
    /// OpenAL custom functions.
    /// </summary>
    internal static unsafe partial class AL
    {
        /// <summary>
        /// Deletes an audio buffer by the specified identifier.
        /// </summary>
        /// <param name="id">The buffer identifier.</param>
        public static void DeleteBuffer(int id)
        {
            deleteBuffers(1, &id);
        }

        /// <summary>
        /// Deletes an audio source by the specified identifier.
        /// </summary>
        /// <param name="id">The source identifier.</param>
        public static void DeleteSource(int id)
        {
            deleteSources(1, &id);
        }

        /// <summary>
        /// Generates an audio buffer.
        /// </summary>
        /// <returns>The buffer identifier.</returns>
        public static int GenBuffer()
        {
            int id;
            genBuffers(1, &id);
            return id;
        }

        /// <summary>
        /// Generates an audio source.
        /// </summary>
        /// <returns>The source identifier.</returns>
        public static int GenSource()
        {
            int id;
            genSources(1, &id);
            return id;
        }

        /// <summary>
        /// Gets a float property of the listener.
        /// </summary>
        /// <param name="property">The property.</param>
        public static float GetListenerFloat(ListenerProperty property)
        {
            float value;
            getListenerf(property, &value);
            return value;
        }

        /// <summary>
        /// Gets a Vector3 property of the listener.
        /// </summary>
        /// <param name="property">The property.</param>
        public static Vector3 GetListenerVector3(ListenerProperty property)
        {
            float x, y, z;
            getListener3f(property, &x, &y, &z);
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Gets the Orientation property of the listener.
        /// </summary>
        /// <param name="forward">The forward vector.</param>
        /// <param name="up">The up vector.</param>
        public static void GetListenerOrientation(out Vector3 forward, out Vector3 up)
        {
            var values = stackalloc float[6];
            getListenerfv(ListenerProperty.Orientation, values);
            forward = new Vector3(values[0], values[1], values[2]);
            up = new Vector3(values[3], values[4], values[5]);
        }

        /// <summary>
        /// Gets the integer value for the specified device by the type..
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="integerType">Type of the integer.</param>
        public static int GetInteger(IntPtr deviceId, IntegerType integerType)
        {
            var buffer = stackalloc int[1];
            getIntegerv(deviceId, integerType, 4, buffer);
            return *buffer;
        }

        /// <summary>
        /// Gets the string by the specified device identifier and type.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="stringType">The string type enumeration value.</param>
        /// <returns>
        /// The string value.
        /// </returns>
        public static string GetString(IntPtr deviceId, StringType stringType)
        {
            var bytes = getString(deviceId, stringType);
            return UnsafeUtility.GetString(bytes);
        }

        /// <summary>
        /// Gets the string by the specified device identifier and type.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="stringType">The string type enumeration value.</param>
        /// <returns>
        /// The string value.
        /// </returns>
        public static List<string> GetStrings(IntPtr deviceId, StringType stringType)
        {
            var bytes = getString(deviceId, stringType);
            var list = new List<string>();
            while (*bytes != 0)
            {
                var str = UnsafeUtility.GetString(bytes);
                list.Add(str);

                bytes += str.Length + 1;
            }

            return list;
        }

        /// <summary>
        /// Determines whether the extension by the specified name presents for the device.
        /// </summary>
        /// <param name="deviceId">The device identifier (Can be NULL if all devices included).</param>
        /// <param name="extensionName">Name of the extension.</param>
        public static bool IsExtensionPresent(IntPtr deviceId, ExtensionNames extensionName)
        {
            return isExtensionPresent(deviceId, extensionName.ToString());
        }

        /// <summary>
        /// Sets the array of float for the specified listener property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="vectors">The vectors.</param>
        public static void Listenerfv(ListenerProperty property, params float[] vectors)
        {
            fixed (float* pointer = vectors)
            {
                listenerfv(property, pointer);
            }
        }
    }
}
