using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("TMGmod")]
[assembly: AssemblyDescription("[1.2] QoL Patches")]
//[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("OnGoTeam")]
[assembly: AssemblyProduct("TMGmod")]
[assembly: AssemblyCopyright("Copyright © 2022 OGT")]
//[assembly: AssemblyTrademark("")]
//[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("190a2e48-c24a-4075-89ae-2e60aea4c0f1")]

// proposed system:
// major                            1
// minor*100 + major milestone    203
// minor milestone                  4
// patch (revision)                 5

#if WORKSHOP
[assembly: AssemblyVersion("1.2.0.0")]
#else
[assembly: AssemblyVersion("1.102.*")]
#endif
