using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog;
using DataGap.Jellog.AutoMapper;
using DataGap.Jellog.Modularity;
using DataGap.CmsKit.Admin;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitProDomainModule),
    typeof(CmsKitProAdminApplicationContractsModule),
    typeof(CmsKitAdminApplicationModule)
    )]
public class CmsKitProAdminApplicationModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CmsKitProAdminApplicationModule>();
        Configure<JellogAutoMapperOptions>(options =>
        {
            options.AddMaps<CmsKitProAdminApplicationModule>(validate: true);
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
    }
}
