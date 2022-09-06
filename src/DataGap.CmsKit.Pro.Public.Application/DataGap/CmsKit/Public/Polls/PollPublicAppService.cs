using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DataGap.Jellog.Users;
using DataGap.CmsKit.Polls;

namespace DataGap.CmsKit.Public.Polls;
public class PollPublicAppService : PublicAppService, IPollPublicAppService
{
    protected IPollRepository PollRepository { get; }
    protected IPollUserVoteRepository PollUserVoteRepository { get; }
    protected PollManager PollManager { get; }

    public PollPublicAppService(
        IPollRepository pollRepository,
        IPollUserVoteRepository pollUserVoteRepository,
        PollManager pollManager)
    {
        PollRepository = pollRepository;
        PollUserVoteRepository = pollUserVoteRepository;
        PollManager = pollManager;
    }

    public async Task<PollWithDetailsDto> GetPollAsync(GetPollInput input)
    {
        var pollId = await PollRepository.GetIdByWidgetAsync(input.WidgetName);

        if (pollId == null)
        {
            return null;
        }
        var poll = await PollRepository.GetAsync(pollId.Value);
        poll.OrderPollOptions();

        return ObjectMapper.Map<Poll, PollWithDetailsDto>(poll);
    }

    public async Task<GetResultDto> GetResultAsync(Guid id)
    {
        var pollWithVotes = (await PollRepository.GetPollWithPollUserVotesAsync(id)).First();

        var resultDetails = new List<PollResultDto>();

        var poll = pollWithVotes.Key;
        var pollUserVotes = pollWithVotes.Value;
        
        var userVotes = CurrentUser.IsAuthenticated
            ? pollUserVotes.Where(v => v.UserId == CurrentUser.GetId()).Select(v=> v.PollOptionId).ToList()
            : new List<Guid>();

        poll.OrderPollOptions();
        foreach (var pollOption in poll.PollOptions)
        {
            resultDetails.Add(new PollResultDto()
            {
                Text = pollOption.Text,
                VoteCount = pollOption.VoteCount,
                IsSelectedForCurrentUser = userVotes.Contains(pollOption.Id)
            });
        }

        return new GetResultDto
        {
            PollVoteCount = poll.VoteCount,
            Question = poll.Question,
            PollResultDetails = resultDetails
        };
    }

    [Authorize]
    public async Task SubmitVoteAsync(Guid id, SubmitPollInput submitPollInput)
    {
        await PollManager.SubmitVoteAsync(id, CurrentUser.GetId(), submitPollInput.PollOptionIds);
    }
}
