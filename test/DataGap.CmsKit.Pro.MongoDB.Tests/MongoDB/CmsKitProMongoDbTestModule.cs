using System;
using Mongo2Go;
using DataGap.Jellog;
using DataGap.Jellog.Data;
using DataGap.Jellog.Modularity;
using DataGap.CmsKit.MongoDB;
using DataGap.Jellog.Uow;

namespace DataGap.CmsKit.Pro.MongoDB;

[DependsOn(
    typeof(CmsKitProTestBaseModule),
    typeof(CmsKitProMongoDbModule)
    )]
public class CmsKitProMongoDbTestModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var stringArray = MongoDbFixture.ConnectionString.Split('?');
        var connectionString = stringArray[0].EnsureEndsWith('/') +
                                               "Db_" +
                                           Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

        Configure<JellogDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connectionString;
        });

        Configure<JellogUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });
    }
}
