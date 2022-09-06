using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.VirtualFileSystem;
using DataGap.CmsKit.Public;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitProPublicApplicationContractsModule),
    typeof(CmsKitPublicHttpApiClientModule)
    )]
public class CmsKitProPublicHttpApiClientModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(CmsKitProPublicApplicationContractsModule).Assembly,
            CmsKitProPublicRemoteServiceConsts.RemoteServiceName
        );

        Configure<JellogVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitProPublicHttpApiClientModule>();
        });
    }
}
