using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog;
using DataGap.Jellog.Modularity;
using DataGap.CmsKit.Newsletters;
using DataGap.CmsKit.Polls;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.MongoDB;

[DependsOn(
    typeof(CmsKitProDomainModule),
    typeof(CmsKitMongoDbModule)
    )]
public class CmsKitProMongoDbModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<CmsKitProMongoDbContext>(options =>
        {
            options.AddRepository<NewsletterRecord, MongoNewsletterRecordRepository>();
            options.AddRepository<ShortenedUrl, MongoShortenedUrlRepository>();
            options.AddRepository<Poll, MongoPollRepository>();
            options.AddRepository<PollUserVote, MongoPollUserVoteRepository>();
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
    }
}
