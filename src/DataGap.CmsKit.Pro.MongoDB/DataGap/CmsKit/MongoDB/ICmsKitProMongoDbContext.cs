using MongoDB.Driver;
using DataGap.Jellog.Data;
using DataGap.Jellog.MongoDB;
using DataGap.CmsKit.Newsletters;
using DataGap.CmsKit.Polls;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.MongoDB;

[ConnectionStringName(CmsKitDbProperties.ConnectionStringName)]
public interface ICmsKitProMongoDbContext : IJellogMongoDbContext
{
    IMongoCollection<NewsletterRecord> NewsletterRecords { get; }
    IMongoCollection<ShortenedUrl> ShortenedUrls { get; }
    IMongoCollection<Poll> Polls { get; }
    IMongoCollection<PollUserVote> PollUserVotes { get; }
}
