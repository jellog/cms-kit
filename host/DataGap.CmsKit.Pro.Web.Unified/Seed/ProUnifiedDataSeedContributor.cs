using System.Threading.Tasks;
using DataGap.Jellog.Data;
using DataGap.Jellog.DependencyInjection;
using DataGap.Jellog.Uow;

namespace DataGap.CmsKit.Pro.Seed;

public class ProUnifiedDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ProSampleIdentityDataSeeder _sampleIdentityDataSeeder;
    private readonly ProSampleDataSeeder _proSampleDataSeeder;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public ProUnifiedDataSeedContributor(
        ProSampleIdentityDataSeeder sampleIdentityDataSeeder,
        IUnitOfWorkManager unitOfWorkManager,
        ProSampleDataSeeder proSampleDataSeeder)
    {
        _sampleIdentityDataSeeder = sampleIdentityDataSeeder;
        _unitOfWorkManager = unitOfWorkManager;
        _proSampleDataSeeder = proSampleDataSeeder;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await _unitOfWorkManager.Current.SaveChangesAsync();
        await _sampleIdentityDataSeeder.SeedAsync(context);
        await _unitOfWorkManager.Current.SaveChangesAsync();
        await _proSampleDataSeeder.SeedAsync(context);
    }
}
