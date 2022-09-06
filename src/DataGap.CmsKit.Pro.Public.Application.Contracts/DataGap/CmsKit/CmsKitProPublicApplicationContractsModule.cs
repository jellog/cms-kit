using DataGap.Jellog.Modularity;
using DataGap.Jellog.Caching;
using DataGap.CmsKit.Public;

namespace DataGap.CmsKit;

[DependsOn(
    typeof(CmsKitProDomainSharedModule),
    typeof(JellogCachingModule),
    typeof(CmsKitPublicApplicationContractsModule)
    )]
public class CmsKitProPublicApplicationContractsModule : JellogModule
{

}
