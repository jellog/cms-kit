using System;
using DataGap.Jellog.Domain.Repositories;

namespace DataGap.CmsKit.Polls;
public interface IPollUserVoteRepository : IBasicRepository<PollUserVote, Guid>
{
}
