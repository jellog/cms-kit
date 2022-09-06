using DataGap.Jellog;
using DataGap.Jellog.Emailing;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.AutoMapper;
using DataGap.Jellog.SettingManagement;
using DataGap.Jellog.TextTemplating;
using DataGap.Jellog.VirtualFileSystem;
using DataGap.CmsKit;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitProDomainSharedModule),
    typeof(CmsKitDomainModule),
    typeof(JellogEmailingModule),
    typeof(JellogTextTemplatingModule),
    typeof(JellogAutoMapperModule),
    typeof(JellogSettingManagementDomainModule)
)]
public class CmsKitProDomainModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<JellogVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitProDomainModule>();
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
    }
}
