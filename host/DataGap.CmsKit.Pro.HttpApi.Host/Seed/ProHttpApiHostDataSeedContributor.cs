﻿using System.Threading.Tasks;
using DataGap.Jellog.Data;
using DataGap.Jellog.DependencyInjection;

namespace DataGap.CmsKit.Pro.Seed;

public class ProHttpApiHostDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ProSampleDataSeeder _proSampleDataSeeder;

    public ProHttpApiHostDataSeedContributor(
        ProSampleDataSeeder proSampleDataSeeder)
    {
        _proSampleDataSeeder = proSampleDataSeeder;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await _proSampleDataSeeder.SeedAsync(context);
    }
}
