using Microsoft.EntityFrameworkCore;
using DataGap.Jellog.EntityFrameworkCore;
using DataGap.CmsKit.EntityFrameworkCore;

namespace DataGap.CmsKit.Pro.EntityFrameworkCore;

public class MyProjectHttpApiHostMigrationsDbContext : JellogDbContext<MyProjectHttpApiHostMigrationsDbContext>
{
    public MyProjectHttpApiHostMigrationsDbContext(DbContextOptions<MyProjectHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureCmsKit();
        modelBuilder.ConfigureCmsKitPro();
    }
}
