using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.GlobalFeatures;
using DataGap.CmsKit.GlobalFeatures;
using DataGap.CmsKit.Permissions;
using DataGap.CmsKit.Polls;

namespace DataGap.CmsKit.Admin.Polls;

[RequiresGlobalFeature(PollsFeature.Name)]
[Authorize(CmsKitProAdminPermissions.Polls.Default)]
public class PollAdminAppService : CmsKitProAdminAppService, IPollAdminAppService
{
    private readonly CmsKitPollingOptions _cmsKitPollingOptions;
    protected IPollRepository PollRepository { get; }

    public PollAdminAppService(IPollRepository pollRepository, IOptions<CmsKitPollingOptions> pollingOptions)
    {
        _cmsKitPollingOptions = pollingOptions.Value;
        PollRepository = pollRepository;
    }

    public async Task<PollWithDetailsDto> GetAsync(Guid id)
    {
        var poll = await PollRepository.GetAsync(id);
        poll.OrderPollOptions();
        return ObjectMapper.Map<Poll, PollWithDetailsDto>(poll);
    }

    public async Task<PagedResultDto<PollDto>> GetListAsync(GetPollListInput input)
    {
        var list = await PollRepository.GetListAsync(input.Filter, input.Sorting, input.SkipCount, input.MaxResultCount);

        return new PagedResultDto<PollDto>()
        {
            Items = new List<PollDto>(
                ObjectMapper.Map<List<Poll>, List<PollDto>>(list)
            ),
            TotalCount = await PollRepository.GetCountAsync(input.Filter)
        };
    }

    [Authorize(CmsKitProAdminPermissions.Polls.Create)]
    public async Task<PollWithDetailsDto> CreateAsync(CreatePollDto input)
    {
        if (!input.Widget.IsNullOrWhiteSpace())
        {
            await ClearWidgetAsync(input.Widget);
        }

        var poll = new Poll(
                GuidGenerator.Create(),
                input.Question,
                input.Widget,
                input.StartDate,
                input.AllowMultipleVote,
                input.ShowVoteCount,
                input.ShowResultWithoutGivingVote,
                input.ShowHoursLeft,
                input.EndDate,
                input.ResultShowingEndDate,
                CurrentTenant?.Id);

        foreach (var item in input.PollOptions)
        {
            poll.AddPollOption(GuidGenerator.Create(), item.Text, item.Order, CurrentTenant?.Id);
        }

        poll = await PollRepository.InsertAsync(poll);

        return ObjectMapper.Map<Poll, PollWithDetailsDto>(poll);
    }

    [Authorize(CmsKitProAdminPermissions.Polls.Update)]
    public async Task<PollWithDetailsDto> UpdateAsync(Guid id, UpdatePollDto input)
    {
        var poll = await PollRepository.GetAsync(id);

        if (!input.Widget.IsNullOrWhiteSpace() && poll.Widget != input.Widget)
        {
            await ClearWidgetAsync(input.Widget);
        }

        poll.SetQuestion(input.Question);
        poll.ShowVoteCount = input.ShowVoteCount;
        poll.ShowResultWithoutGivingVote = input.ShowResultWithoutGivingVote;
        poll.ShowHoursLeft = input.ShowHoursLeft;
        poll.Widget = input.Widget;
        poll.SetDates(input.StartDate, input.EndDate, input.ResultShowingEndDate);

        //TODO may make refactor and do these steps in the domain layer
        var removedPollOptionIds = poll.PollOptions.Select(p => p.Id).Except(input.PollOptions.Select(p => p.Id)).ToList();
        foreach (var removedPollOptionId in removedPollOptionIds)
        {
            poll.RemovePollOption(removedPollOptionId);
        }
        foreach (var item in input.PollOptions)
        {
            poll.UpdatePollOption(item.Id, item.Text, item.Order, poll.TenantId);
        }

        await PollRepository.UpdateAsync(poll);

        return ObjectMapper.Map<Poll, PollWithDetailsDto>(poll);
    }

    [Authorize(CmsKitProAdminPermissions.Polls.Delete)]
    public Task DeleteAsync(Guid id)
    {
        return PollRepository.DeleteAsync(id);
    }

    public Task<ListResultDto<PollWidgetDto>> GetWidgetsAsync()
    {
        return Task.FromResult(new ListResultDto<PollWidgetDto>()
        {
            Items = _cmsKitPollingOptions.WidgetNames
                .Select(n =>
                    new PollWidgetDto
                    {
                        Name = n
                    }).ToList()
        });
    }

    public async Task<GetResultDto> GetResultAsync(Guid id)
    {
        var poll = await PollRepository.GetAsync(id);
        poll.OrderPollOptions();

        var resultDetails = new List<PollResultDto>();

        foreach (var pollOption in poll.PollOptions)
        {
            resultDetails.Add(new PollResultDto()
            {
                Text = pollOption.Text,
                VoteCount = poll.PollOptions.First(p => p.Id == pollOption.Id).VoteCount,
            });
        }

        return new GetResultDto
        {
            PollVoteCount = poll.VoteCount,
            Question = poll.Question,
            PollResultDetails = resultDetails
        };
    }

    private async Task ClearWidgetAsync(string widget)
    {
        var pollsWithSameWidget = await PollRepository.GetListByWidgetAsync(widget);
        foreach (var pollWithSameWidget in pollsWithSameWidget)
        {
            pollWithSameWidget.Widget = null;
            await PollRepository.UpdateAsync(pollWithSameWidget);
        }
    }
}
