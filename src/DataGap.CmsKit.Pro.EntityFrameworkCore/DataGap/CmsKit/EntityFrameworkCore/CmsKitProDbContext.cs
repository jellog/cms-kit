using Microsoft.EntityFrameworkCore;
using DataGap.Jellog.Data;
using DataGap.Jellog.EntityFrameworkCore;
using DataGap.CmsKit.Newsletters;
using DataGap.CmsKit.Polls;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.EntityFrameworkCore;

[ConnectionStringName(CmsKitDbProperties.ConnectionStringName)]
public class CmsKitProDbContext : JellogDbContext<CmsKitProDbContext>, ICmsKitProDbContext
{
    public DbSet<NewsletterRecord> NewsletterRecords { get; set; }
    public DbSet<NewsletterPreference> NewsletterPreferences { get; set; }
    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    public DbSet<Poll> Polls { get; set; }
    public DbSet<PollUserVote> PollUserVotes { get; set; }
    public DbSet<PollOption> PollOptions { get; set; }

    public CmsKitProDbContext(DbContextOptions<CmsKitProDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureCmsKitPro();
    }
}
