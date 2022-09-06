using System;
using DataGap.Jellog;

namespace DataGap.CmsKit.Polls;

[Serializable]
public class PollUserVoteVotedBySameUserException : BusinessException
{
    public PollUserVoteVotedBySameUserException(Guid userId, Guid pollId, Guid pollOptionId)
    : base(CmsKitProErrorCodes.Poll.PollUserVoteVotedBySameUser)
    {
        WithData(nameof(PollUserVote.UserId), userId);
        WithData(nameof(PollUserVote.PollId), pollId);
        WithData(nameof(PollUserVote.PollOptionId), pollOptionId);
    }
}
