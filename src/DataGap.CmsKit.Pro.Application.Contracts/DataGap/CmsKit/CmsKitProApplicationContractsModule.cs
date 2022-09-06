using DataGap.Jellog.Modularity;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitApplicationContractsModule),
    typeof(CmsKitProPublicApplicationContractsModule),
    typeof(CmsKitProAdminApplicationContractsModule)
    )]
public class CmsKitProApplicationContractsModule : JellogModule
{

}
