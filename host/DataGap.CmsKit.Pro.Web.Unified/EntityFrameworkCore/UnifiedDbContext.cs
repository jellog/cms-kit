using Microsoft.EntityFrameworkCore;
using DataGap.Jellog.AuditLogging.EntityFrameworkCore;
using DataGap.Jellog.BlobStoring.Database.EntityFrameworkCore;
using DataGap.Jellog.EntityFrameworkCore;
using DataGap.Jellog.FeatureManagement.EntityFrameworkCore;
using DataGap.Jellog.Identity.EntityFrameworkCore;
using DataGap.Jellog.PermissionManagement.EntityFrameworkCore;
using DataGap.Jellog.SettingManagement.EntityFrameworkCore;
using DataGap.CmsKit.EntityFrameworkCore;
using DataGap.Saas.EntityFrameworkCore;

namespace DataGap.CmsKit.Pro.EntityFrameworkCore;

public class UnifiedDbContext : JellogDbContext<UnifiedDbContext>
{
    public UnifiedDbContext(DbContextOptions<UnifiedDbContext> options)
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
        modelBuilder.ConfigureSaas();
        modelBuilder.ConfigureCmsKit();
        modelBuilder.ConfigureCmsKitPro();
        modelBuilder.ConfigureBlobStoring();
        modelBuilder.ConfigureFeatureManagement();
    }
}
