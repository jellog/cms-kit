using DataGap.Jellog;
using DataGap.Jellog.Modularity;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitApplicationModule),
    typeof(CmsKitProApplicationContractsModule),
    typeof(CmsKitProPublicApplicationModule),
    typeof(CmsKitProAdminApplicationModule)
    )]
public class CmsKitProApplicationModule : JellogModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
    }
}
