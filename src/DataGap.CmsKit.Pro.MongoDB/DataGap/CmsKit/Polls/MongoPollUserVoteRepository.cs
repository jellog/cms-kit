using System;
using DataGap.Jellog.Domain.Repositories.MongoDB;
using DataGap.Jellog.MongoDB;
using DataGap.CmsKit.MongoDB;

namespace DataGap.CmsKit.Polls;
public class MongoPollUserVoteRepository : MongoDbRepository<ICmsKitProMongoDbContext, PollUserVote, Guid>, IPollUserVoteRepository
{
    public MongoPollUserVoteRepository(IMongoDbContextProvider<ICmsKitProMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

}
