﻿using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(CursorLockFix.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(CursorLockFix.BuildInfo.Author)]
[assembly: AssemblyProduct(CursorLockFix.BuildInfo.Name)]
[assembly: AssemblyCopyright("Copyright ©  2021")]
[assembly: AssemblyTrademark(CursorLockFix.BuildInfo.Author)]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("bcd54003-2a52-4f17-9df0-82e7de792a8e")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(CursorLockFix.BuildInfo.Version)]
[assembly: AssemblyFileVersion(CursorLockFix.BuildInfo.Version)]

[assembly: MelonInfo(typeof(CursorLockFix.Main), CursorLockFix.BuildInfo.Name, CursorLockFix.BuildInfo.Version, CursorLockFix.BuildInfo.Author)]
[assembly:MelonGame] 