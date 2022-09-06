using DataGap.Jellog.Modularity;
using DataGap.Jellog.Studio;
using DataGap.Jellog.VirtualFileSystem;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(JellogStudioModuleInstallerModule),
    typeof(JellogVirtualFileSystemModule)
    )]
public class CmsKitProInstallerModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<JellogVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitProInstallerModule>();
        });
    }
}
