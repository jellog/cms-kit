using DataGap.Jellog.Modularity;
using DataGap.Jellog.SettingManagement.Blazor.Server;

namespace DataGap.CmsKit.Pro.Admin.Blazor.Server;

[DependsOn(
    typeof(JellogSettingManagementBlazorServerModule),
    typeof(CmsKitProAdminBlazorModule)
    )]
public class CmsKitProAdminBlazorServerModule : JellogModule
{
}
