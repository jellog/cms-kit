using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog;
using DataGap.Jellog.Modularity;
using DataGap.CmsKit.Newsletters;
using DataGap.CmsKit.Polls;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.EntityFrameworkCore;

[DependsOn(
    typeof(CmsKitProDomainModule),
    typeof(CmsKitEntityFrameworkCoreModule)
)]
public class CmsKitProEntityFrameworkCoreModule : JellogModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddJellogDbContext<CmsKitProDbContext>(options =>
        {
            options.AddRepository<NewsletterRecord, EfCoreNewsletterRecordRepository>();
            options.AddRepository<ShortenedUrl, EfCoreShortenedUrlRepository>();
            options.AddRepository<Poll, EfCorePollRepository>();
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        
    }
}
