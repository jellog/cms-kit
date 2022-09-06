using DataGap.Jellog;
using DataGap.Jellog.MongoDB;
using DataGap.CmsKit.Newsletters;
using DataGap.CmsKit.Polls;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.MongoDB;

public static class CmsKitProMongoDbContextExtensions
{
    public static void ConfigureCmsKitPro(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ConfigureCmsKit();

        builder.Entity<NewsletterRecord>(x =>
        {
            x.CollectionName = CmsKitDbProperties.DbTablePrefix + "NewsletterRecords";
        });

        builder.Entity<ShortenedUrl>(x =>
        {
            x.CollectionName = CmsKitDbProperties.DbTablePrefix + "ShortenedUrls";
        });

        builder.Entity<Poll>(x =>
        {
            x.CollectionName = CmsKitDbProperties.DbTablePrefix + "Polls";
        });
    }
}
