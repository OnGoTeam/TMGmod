using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("TMGmod")]
#if WORKSHOP
[assembly: AssemblyDescription("[1.1.7] QoL Patches")]
#elif FEATURES_1_2
[assembly: AssemblyDescription("[1.2] HP")]
#elif FEATURES_1_3
[assembly: AssemblyDescription("[1.3] Explosive Things")]
#else
[assembly: AssemblyDescription("[1.107] QoL Patches")]
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
[assembly: AssemblyVersion("1.1.7.0")]
#else
[assembly: AssemblyVersion("1.107.*")]
#endif
