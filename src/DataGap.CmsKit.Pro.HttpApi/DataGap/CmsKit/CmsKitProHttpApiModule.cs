using DataGap.Jellog;
using DataGap.Jellog.Modularity;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitHttpApiModule),
    typeof(CmsKitProApplicationContractsModule),
    typeof(CmsKitProPublicHttpApiModule),
    typeof(CmsKitProAdminHttpApiModule)
    )]
public class CmsKitProHttpApiModule : JellogModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
    }
}
