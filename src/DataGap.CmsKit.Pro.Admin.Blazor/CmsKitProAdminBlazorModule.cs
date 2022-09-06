using DataGap.Jellog.AutoMapper;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.SettingManagement.Blazor;
using DataGap.CmsKit.Pro.Admin.Blazor.Settings;

namespace DataGap.CmsKit.Pro.Admin.Blazor;

[DependsOn(
    typeof(JellogAutoMapperModule),
    typeof(JellogSettingManagementBlazorModule),
    typeof(CmsKitProAdminApplicationContractsModule)
)]
public class CmsKitProAdminBlazorModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SettingManagementComponentOptions>(options =>
        {
            options.Contributors.Add(new CmsKitProAdminSettingManagementComponentContributor());
        });
    }
}
