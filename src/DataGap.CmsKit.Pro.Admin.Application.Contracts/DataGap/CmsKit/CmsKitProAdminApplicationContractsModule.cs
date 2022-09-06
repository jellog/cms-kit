using DataGap.Jellog.Modularity;
using DataGap.CmsKit.Admin;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitProDomainSharedModule),
    typeof(CmsKitAdminApplicationContractsModule)
    )]
public class CmsKitProAdminApplicationContractsModule : JellogModule
{

}
