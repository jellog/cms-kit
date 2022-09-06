using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog;
using DataGap.Jellog.AutoMapper;
using DataGap.Jellog.Emailing;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.UI.Navigation;
using DataGap.Jellog.VirtualFileSystem;
using DataGap.CmsKit.Public;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitProDomainModule),
    typeof(CmsKitProPublicApplicationContractsModule),
    typeof(CmsKitPublicApplicationModule),
    typeof(JellogEmailingModule),
    typeof(JellogUiNavigationModule)
    )]
public class CmsKitProPublicApplicationModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CmsKitProPublicApplicationModule>();
        Configure<JellogAutoMapperOptions>(options =>
        {
            options.AddMaps<CmsKitProPublicApplicationModule>(validate: true);
        });

        Configure<JellogVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitProPublicApplicationModule>();
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
    }
}
