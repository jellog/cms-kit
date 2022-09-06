using Microsoft.EntityFrameworkCore;
using DataGap.Jellog.AuditLogging.EntityFrameworkCore;
using DataGap.Jellog.BlobStoring.Database.EntityFrameworkCore;
using DataGap.Jellog.EntityFrameworkCore;
using DataGap.Jellog.FeatureManagement.EntityFrameworkCore;
using DataGap.Jellog.Identity.EntityFrameworkCore;
using DataGap.Jellog.IdentityServer.EntityFrameworkCore;
using DataGap.Jellog.PermissionManagement.EntityFrameworkCore;
using DataGap.Jellog.SettingManagement.EntityFrameworkCore;
using DataGap.Saas.EntityFrameworkCore;

namespace DataGap.CmsKit.Pro.EntityFrameworkCore;

public class IdentityServerHostMigrationsDbContext : JellogDbContext<IdentityServerHostMigrationsDbContext>
{
    public IdentityServerHostMigrationsDbContext(DbContextOptions<IdentityServerHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentityPro();
        modelBuilder.ConfigureIdentityServer();
        modelBuilder.ConfigureSaas();
        modelBuilder.ConfigureBlobStoring();
        modelBuilder.ConfigureFeatureManagement();
    }
}
