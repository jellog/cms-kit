using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.VirtualFileSystem;
using DataGap.CmsKit.Admin;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitProAdminApplicationContractsModule),
    typeof(CmsKitAdminHttpApiClientModule)
    )]
public class CmsKitProAdminHttpApiClientModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(CmsKitProAdminApplicationContractsModule).Assembly,
            CmsKitAdminRemoteServiceConsts.RemoteServiceName
        );

        Configure<JellogVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitProAdminHttpApiClientModule>();
        });
    }
}
