using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using DataGap.Jellog.EntityFrameworkCore;
using DataGap.Jellog.EntityFrameworkCore.Sqlite;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.SettingManagement.EntityFrameworkCore;
using DataGap.CmsKit.EntityFrameworkCore;

namespace DataGap.CmsKit.Pro.EntityFrameworkCore;

[DependsOn(
    typeof(CmsKitProTestBaseModule),
    typeof(CmsKitProEntityFrameworkCoreModule),
    typeof(JellogSettingManagementEntityFrameworkCoreModule),
    typeof(JellogEntityFrameworkCoreSqliteModule)
    )]
public class CmsKitProEntityFrameworkCoreTestModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var sqliteConnection = CreateDatabaseAndGetConnection();

        Configure<JellogDbContextOptions>(options =>
        {
            options.Configure(jellogDbContextConfigurationContext =>
            {
                jellogDbContextConfigurationContext.DbContextOptions.UseSqlite(sqliteConnection);
            });
        });
    }

    private static SqliteConnection CreateDatabaseAndGetConnection()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        new CmsKitProDbContext(
            new DbContextOptionsBuilder<CmsKitProDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        new SettingManagementDbContext(
            new DbContextOptionsBuilder<SettingManagementDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        return connection;
    }
}
