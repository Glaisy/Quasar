//-----------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

[assembly: AssemblyTitle("Quasar - Windows")]
[assembly: AssemblyDescription("Quasar 3D engine - Windows platform library")]
[assembly: SupportedOSPlatform("windows")]

[assembly: InternalsVisibleTo("Quasar.Windows.Tests")]
[assembly: InternalsVisibleTo("Quasar.OpenGL.Tests")]
[assembly: InternalsVisibleTo("Quasar.OpenAL.Tests")]