using System;
using DataGap.Jellog.Domain.Repositories.EntityFrameworkCore;
using DataGap.Jellog.EntityFrameworkCore;
using DataGap.CmsKit.EntityFrameworkCore;

namespace DataGap.CmsKit.Polls;
public class EfCorePollUserVoteRepository : EfCoreRepository<CmsKitProDbContext, PollUserVote, Guid>, IPollUserVoteRepository
{
    public EfCorePollUserVoteRepository(IDbContextProvider<CmsKitProDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
