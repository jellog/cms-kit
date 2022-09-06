using Microsoft.EntityFrameworkCore;
using DataGap.Jellog;
using DataGap.Jellog.EntityFrameworkCore.Modeling;
using DataGap.Jellog.GlobalFeatures;
using DataGap.CmsKit.GlobalFeatures;
using DataGap.CmsKit.Newsletters;
using DataGap.CmsKit.Polls;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.EntityFrameworkCore;

public static class CmsKitProDbContextModelCreatingExtensions
{
    public static void ConfigureCmsKitPro(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ConfigureCmsKit();

        if (GlobalFeatureManager.Instance.IsEnabled<NewslettersFeature>())
        {
            builder.Entity<NewsletterRecord>(b =>
            {
                b.ToTable(CmsKitDbProperties.DbTablePrefix + "NewsletterRecords", CmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(n => n.EmailAddress).HasMaxLength(NewsletterRecordConst.MaxEmailAddressLength).IsRequired().HasColumnName(nameof(NewsletterRecord.EmailAddress));

                b.HasIndex(n => new { n.TenantId, n.EmailAddress });

                b.HasMany(n => n.Preferences).WithOne().HasForeignKey(x => x.NewsletterRecordId).IsRequired();

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<NewsletterPreference>(b =>
            {
                b.ToTable(CmsKitDbProperties.DbTablePrefix + "NewsletterPreferences", CmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(n => n.Preference).HasMaxLength(NewsletterPreferenceConst.MaxPreferenceLength).IsRequired().HasColumnName(nameof(NewsletterPreference.Preference));
                b.Property(n => n.Source).HasMaxLength(NewsletterPreferenceConst.MaxSourceLength).IsRequired().HasColumnName(nameof(NewsletterPreference.Source));
                b.Property(n => n.SourceUrl).HasMaxLength(NewsletterPreferenceConst.MaxSourceUrlLength).IsRequired().HasColumnName(nameof(NewsletterPreference.SourceUrl));

                b.HasIndex(n => new { n.TenantId, n.Preference, n.Source });

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<ShortenedUrl>(b =>
            {
                b.ToTable(CmsKitDbProperties.DbTablePrefix + "ShortenedUrls", CmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.Property(n => n.Source).HasMaxLength(ShortenedUrlConst.MaxSourceLength).IsRequired().HasColumnName(nameof(ShortenedUrl.Source));
                b.Property(n => n.Target).HasMaxLength(ShortenedUrlConst.MaxTargetLength).IsRequired().HasColumnName(nameof(ShortenedUrl.Target));

                b.HasIndex(n => new { n.TenantId, n.Source });

                b.ApplyObjectExtensionMappings();
            });
        }

        if (GlobalFeatureManager.Instance.IsEnabled<PollsFeature>())
        {
            builder.Entity<Poll>(b =>
            {
                b.ToTable(CmsKitDbProperties.DbTablePrefix + "Polls", CmsKitDbProperties.DbSchema);

                b.Property(n => n.Question).HasMaxLength(PollConst.MaxQuestionLength).IsRequired().HasColumnName(nameof(Poll.Question));
                b.Property(n => n.Widget).HasMaxLength(PollConst.MaxWidgetNameLength).HasColumnName(nameof(Poll.Widget));

                b.ConfigureByConvention();

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<PollUserVote>(b =>
            {
                b.ToTable(CmsKitDbProperties.DbTablePrefix + "PollUserVotes", CmsKitDbProperties.DbSchema);

                b.ConfigureByConvention();

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<PollOption>(b =>
            {
                b.ToTable(CmsKitDbProperties.DbTablePrefix + "PollOptions", CmsKitDbProperties.DbSchema);

                b.Property(n => n.Text).HasMaxLength(PollConst.MaxTextLength).IsRequired().HasColumnName(nameof(PollOption.Text));

                b.ConfigureByConvention();

                b.ApplyObjectExtensionMappings();
            });
        }

        builder.TryConfigureObjectExtensions<CmsKitProDbContext>();
    }
}
