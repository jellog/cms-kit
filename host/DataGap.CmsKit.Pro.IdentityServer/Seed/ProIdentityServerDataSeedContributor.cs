using System.Threading.Tasks;
using DataGap.Jellog.Data;
using DataGap.Jellog.DependencyInjection;

namespace DataGap.CmsKit.Pro.Seed;

public class ProIdentityServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ProSampleIdentityDataSeeder _proSampleIdentityDataSeeder;
    private readonly ProIdentityServerDataSeeder _proIdentityServerDataSeeder;

    public ProIdentityServerDataSeedContributor(
        ProIdentityServerDataSeeder proIdentityServerDataSeeder,
        ProSampleIdentityDataSeeder proSampleIdentityDataSeeder)
    {
        _proIdentityServerDataSeeder = proIdentityServerDataSeeder;
        _proSampleIdentityDataSeeder = proSampleIdentityDataSeeder;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await _proSampleIdentityDataSeeder.SeedAsync(context);
        await _proIdentityServerDataSeeder.SeedAsync(context);
    }
}
