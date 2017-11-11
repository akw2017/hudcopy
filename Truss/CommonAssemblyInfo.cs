using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

[assembly: AssemblyCompany("Kent Boogaart")]
[assembly: AssemblyProduct("Truss")]
[assembly: AssemblyCopyright("?Copyright. Kent Boogaart.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.1.0.0")]
[assembly: AssemblyFileVersion("1.1.0.0")]
[assembly: AllowPartiallyTrustedCallers]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif