using DataGap.CmsKit.Pro.EntityFrameworkCore;
using DataGap.Jellog.Modularity;

namespace DataGap.CmsKit.Pro;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(CmsKitProEntityFrameworkCoreTestModule)
    )]
public class CmsKitProDomainTestModule : JellogModule
{

}
