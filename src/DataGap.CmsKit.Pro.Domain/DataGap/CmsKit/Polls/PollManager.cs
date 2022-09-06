using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGap.Jellog.Domain.Entities;

namespace DataGap.CmsKit.Polls;
public class PollManager : CmsKitProDomainServiceBase
{
    protected IPollRepository PollRepository { get; }
    protected IPollUserVoteRepository PollUserVoteRepository { get; }


    public PollManager(IPollRepository pollRepository, IPollUserVoteRepository pollUserVoteRepository)
    {
        PollRepository = pollRepository;
        PollUserVoteRepository = pollUserVoteRepository;
    }

    public async Task SubmitVoteAsync(Guid id, Guid userId, Guid[] pollOptionIds)
    {
        var pollWithVotes = (await PollRepository.GetPollWithPollUserVotesAsync(id)).First();

        var poll = pollWithVotes.Key;
        var votedList = pollWithVotes.Value;

        var votes = new List<PollUserVote>();

        if (poll.AllowMultipleVote)
        {
            foreach (var pollOptionId in pollOptionIds)
            {
                AddPollUserVote(userId, poll, votes, pollOptionId, votedList);
            }
        }
        else
        {
            if (pollOptionIds.Length != 1)
            {
                throw new PollAllowSingleVoteException(poll.AllowMultipleVote);
            }

            var pollOptionId = pollOptionIds.First();
            AddPollUserVote(userId, poll, votes, pollOptionId, votedList);
        }
        poll.Increase();

        await PollUserVoteRepository.InsertManyAsync(votes);
    }

    private void AddPollUserVote(Guid userId, Poll poll, List<PollUserVote> votes, Guid pollOptionId, List<PollUserVote> votedList)
    {
        if (!poll.PollOptions.Any(p => p.Id == pollOptionId))
        {
            throw new EntityNotFoundException(typeof(Guid), pollOptionId);
        }

        bool isVotedBySameUser = votedList.Any(p => p.UserId == userId && p.PollId == poll.Id && p.PollOptionId == pollOptionId);

        if (isVotedBySameUser)
        {
            throw new PollUserVoteVotedBySameUserException(userId, poll.Id, pollOptionId);
        }

        votes.Add(new PollUserVote(GuidGenerator.Create(), poll.Id, userId, pollOptionId, poll.TenantId));
        
        poll.PollOptions.First(p => p.Id == pollOptionId).Increase();
    }
}
