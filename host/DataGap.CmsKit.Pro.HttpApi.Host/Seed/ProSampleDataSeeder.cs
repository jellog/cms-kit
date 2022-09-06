using System.Threading.Tasks;
using DataGap.Jellog.Data;
using DataGap.Jellog.DependencyInjection;

namespace DataGap.CmsKit.Pro.Seed;

/* You can use this file to seed some sample data
 * to test your module easier.
 *
 * This class is shared among these projects:
 * - DataGap.CmsKit.Pro.IdentityServer
 * - DataGap.CmsKit.Pro.Web.Unified (used as linked file)
 */
public class ProSampleDataSeeder : ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {

    }
}
