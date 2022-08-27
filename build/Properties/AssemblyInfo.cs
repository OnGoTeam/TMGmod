using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("TMGmod")]
#if WORKSHOP
[assembly: AssemblyDescription("[1.2] Fifth Anniversary Update")]
#elif FEATURES_1_2_X
[assembly: AssemblyDescription("[1.2.X] Fifth Anniversary Update Patch X")]
#elif FEATURES_1_3
[assembly: AssemblyDescription("[1.3] HP")]
#elif FEATURES_1_4
[assembly: AssemblyDescription("[1.4] Explosives")]
#else
[assembly: AssemblyDescription("[1.200] Fifth Anniversary Update")]
#endif
//[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("OnGoTeam")]
[assembly: AssemblyProduct("TMGmod")]
[assembly: AssemblyCopyright("Copyright © 2022 OGT")]
//[assembly: AssemblyTrademark("")]
//[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("190a2e48-c24a-4075-89ae-2e60aea4c0f1")]

#if WORKSHOP
[assembly: AssemblyVersion("1.2.0.1")]
#else
[assembly: AssemblyVersion("1.200.*")]
#endif
