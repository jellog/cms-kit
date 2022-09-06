using DataGap.Jellog.Localization;
using DataGap.Jellog.Localization.ExceptionHandling;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.VirtualFileSystem;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitDomainSharedModule)
)]
public class CmsKitProDomainSharedModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<JellogVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitProDomainSharedModule>();
        });

        Configure<JellogLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CmsKitResource>()
                .AddVirtualJson("DataGap/CmsKit/Localization/Resources/Pro");
        });

        Configure<JellogExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("CmsKitPro", typeof(CmsKitResource));
        });
    }
}
