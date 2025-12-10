//-----------------------------------------------------------------------
// <copyright file="ComponentTableManager.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

namespace Quasar.Entities.Internals
{
    /// <summary>
    /// Represents a manager object to manipulate component data tables.
    /// </summary>
    internal sealed unsafe class ComponentTableManager
    {
        ////private readonly Dictionary<Type, IntPtr> componentTables
        ////    = new Dictionary<Type, IntPtr>();


        ////private ComponentTable* GetComponentTable<T>()
        ////    where T : struct
        ////{
        ////    var componentType = typeof(T);
        ////    if (componentTables.TryGetValue(componentType, out var componentTablePtr))
        ////    {
        ////        return (ComponentTable*)componentTablePtr;
        ////    }

        ////    componentTablePtr = AllocateComponentTable(componentType);
        ////    componentTables.Add(componentType, componentTablePtr);
        ////    return (ComponentTable*)componentTablePtr;
        ////}

        ////private IntPtr AllocateComponentTable(Type componentType)
        ////{
        ////    var componentSize = Marshal.SizeOf(componentType);

        ////    throw new NotImplementedException();
        ////}

        ////private void FreeComponentTableChunk(ComponentTableChunk* componentTableChunk)
        ////{
        ////    componentTableChunk->Remove();
        ////}

        ////private void FreeComponentTable(ComponentTable* componentTable)
        ////{
        ////    // free all table chunk data
        ////    var chunk = componentTable->Head;
        ////    while (chunk != null)
        ////    {
        ////        var nextChunk = chunk->Next;
        ////        Marshal.FreeHGlobal(new IntPtr(chunk));
        ////        chunk = nextChunk;
        ////    }

        ////    // free table data
        ////    Marshal.FreeHGlobal(new IntPtr(componentTable));
        ////}
    }
}
