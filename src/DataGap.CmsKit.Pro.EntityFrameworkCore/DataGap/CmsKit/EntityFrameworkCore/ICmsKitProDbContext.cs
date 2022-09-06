using Microsoft.EntityFrameworkCore;
using DataGap.Jellog.Data;
using DataGap.Jellog.EntityFrameworkCore;
using DataGap.CmsKit.Polls;

namespace DataGap.CmsKit.EntityFrameworkCore;

[ConnectionStringName(CmsKitDbProperties.ConnectionStringName)]
public interface ICmsKitProDbContext : IEfCoreDbContext
{
    DbSet<Poll> Polls { get; }
    DbSet<PollUserVote> PollUserVotes { get; }
}
