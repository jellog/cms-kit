using System;
using DataGap.Jellog;

namespace DataGap.CmsKit.Polls;

[Serializable]
public class PollAllowSingleVoteException : BusinessException
{
    public PollAllowSingleVoteException(bool allowMultipleVote)
    : base(CmsKitProErrorCodes.Poll.PollAllowSingleVoteException)
    {
        WithData(nameof(Poll.AllowMultipleVote), allowMultipleVote);
    }
}
