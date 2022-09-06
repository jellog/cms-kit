using DataGap.Jellog.Modularity;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitHttpApiClientModule),
    typeof(CmsKitProApplicationContractsModule),
    typeof(CmsKitProAdminHttpApiClientModule),
    typeof(CmsKitProPublicHttpApiClientModule)
    )]
public class CmsKitProHttpApiClientModule : JellogModule
{

}
