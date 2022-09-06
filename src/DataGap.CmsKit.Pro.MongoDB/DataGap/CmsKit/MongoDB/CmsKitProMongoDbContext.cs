using MongoDB.Driver;
using DataGap.Jellog.Data;
using DataGap.Jellog.MongoDB;
using DataGap.CmsKit.Newsletters;
using DataGap.CmsKit.Polls;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.MongoDB;

[ConnectionStringName(CmsKitDbProperties.ConnectionStringName)]
public class CmsKitProMongoDbContext : JellogMongoDbContext, ICmsKitProMongoDbContext
{
    public IMongoCollection<NewsletterRecord> NewsletterRecords => Collection<NewsletterRecord>();
    public IMongoCollection<ShortenedUrl> ShortenedUrls => Collection<ShortenedUrl>();
    public IMongoCollection<Poll> Polls => Collection<Poll>();
    public IMongoCollection<PollUserVote> PollUserVotes => Collection<PollUserVote>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureCmsKitPro();
    }
}
