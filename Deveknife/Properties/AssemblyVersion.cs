#if VERCommon
public class AssemblyProductInfo
{
#else
namespace Deveknife.Properties
{
    internal class AssemblyProductInfo
    {
#endif
    public const string AssemblyCopyright = "Copyright (c) 2000-2014 Evepanix.";
    public const string AssemblyCompany = "Evepanix.";

    public static string InstanceMe = "This";
    public const int VersionId = 10;
    private const string VMajor = "1";
    private const string VMinor = "0";
    private const string VBuild = "0";
    private const string VRevision = "0";

    public const string SVNRevision = Revision.SVNRevision;

    public const string VersionShort = VMajor + "." + VMinor;
    public const string VirtDirSuffix = "_v" + VMajor + "_" + VMinor;
    public const string Version = VersionShort + "." + VBuild + "." + VRevision;
    public const string SatelliteContractVersion = VersionShort + ".0.0";
    public const string VSuffixWithoutSeparator = "v" + VersionShort;
    public const string VSuffix = "." + VSuffixWithoutSeparator;
    public const string VSuffixDesign = VSuffix + ".Design";
    public const string DllSuffix = ".dll";
    public const string
        SRAssemblyTemp = "Jedzia.Temp" + VSuffix,
        SRAssemblyTempDesign = "Jedzia.Temp" + VSuffixDesign,
        SRAssemblyEditors = "Jedzia.XtraEditors" + VSuffix,
        SRAssemblyEditorsDesign = "Jedzia.XtraEditors" + VSuffixDesign,
        SRAssemblyFoo = "Jedzia.tmp.Foo" + VSuffix;
}
#if !VERCommon
}
#endif
