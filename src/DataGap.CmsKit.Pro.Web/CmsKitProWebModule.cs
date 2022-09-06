using DataGap.Jellog;
using DataGap.Jellog.Modularity;
using DataGap.CmsKit.Pro.Admin.Web;
using DataGap.CmsKit.Pro.Public.Web;
using DataGap.CmsKit.Web;

namespace DataGap.CmsKit.Pro.Web;

[DependsOn(
    typeof(CmsKitWebModule),
    typeof(CmsKitProApplicationContractsModule),
    typeof(CmsKitProPublicWebModule),
    typeof(CmsKitProAdminWebModule)
    )]
public class CmsKitProWebModule : JellogModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
    }
}
