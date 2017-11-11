using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

[assembly: AssemblyTitle("Truss")]
[assembly: AssemblyDescription("A Plain Old CLR Object (POCO) binding library.")]
[assembly: CLSCompliant(true)]
[assembly: ComVisibleAttribute(false)]
[assembly: AssemblyCompany("Kent Boogaart")]
[assembly: AssemblyProduct("Truss")]
[assembly: AssemblyCopyright("?Copyright. Kent Boogaart.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("1.1.0.0")]
[assembly: AssemblyFileVersion("1.1.0.0")]
[assembly: AllowPartiallyTrustedCallers]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
