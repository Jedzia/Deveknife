// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="">
//   
// </copyright>
// <summary>
//   AssemblyInfo.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Reflection;
using System.Runtime.InteropServices;
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="">
//   
// </copyright>
// <summary>
//   AssemblyInfo.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------




using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

using Deveknife.Properties;

#if CF
[assembly:AssemblyTitle("Jedzia.Deveknife.Version.Compact")]
#else
[assembly: AssemblyTitle("Deveknife")]
#endif
[assembly: AssemblyDescription("Deveknife Runtime")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(AssemblyProductInfo.AssemblyCompany)]
[assembly: AssemblyProduct("Deveknife")]
[assembly: AssemblyCopyright(AssemblyProductInfo.AssemblyCopyright)]
[assembly: AssemblyTrademark("Acc")]
[assembly: AssemblyCulture("")]
//[assembly: System.Security.AllowPartiallyTrustedCallers()]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: SatelliteContractVersion(AssemblyProductInfo.SatelliteContractVersion)]
[assembly: AssemblyVersion(AssemblyProductInfo.Version)]
[assembly: AssemblyFileVersion(AssemblyProductInfo.Version)]
[assembly: AssemblyInformationalVersion(AssemblyProductInfo.Version)]
[assembly: Guid("9856721e-41c4-4bd1-8d99-aebc3a480eed")]
#if Whidbey
#pragma warning disable 1699
#endif
//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile(@"..\..\..\Key\StrongKey.snk")]
//[assembly: AssemblyKeyName("")]
#if Whidbey
#pragma warning restore 1699
#endif

