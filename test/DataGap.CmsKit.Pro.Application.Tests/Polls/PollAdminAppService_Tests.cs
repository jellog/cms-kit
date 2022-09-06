using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using DataGap.CmsKit.Admin.Polls;
using DataGap.CmsKit.Polls;
using Xunit;

namespace DataGap.CmsKit.Pro.Polls;
public class PollAdminAppService_Tests : CmsKitProApplicationTestBase
{
    private readonly CmsKitProTestData _cmsKitProTestData;
    private readonly IPollAdminAppService _pollAdminAppService;

    public PollAdminAppService_Tests()
    {
        _cmsKitProTestData = GetRequiredService<CmsKitProTestData>();
        _pollAdminAppService = GetRequiredService<IPollAdminAppService>();
    }

    [Fact]
    public async Task CreateAsync()
    {
        var newPoll = new CreatePollDto
        {
            Question = "Have you ever used JELLOG framework",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddYears(1),
            PollOptions = new System.Collections.ObjectModel.Collection<PollOptionDto>()
            {
                new PollOptionDto() { Text = "Yes"},
                new PollOptionDto() { Text = "No"}
            }
        };
        await _pollAdminAppService.CreateAsync(newPoll);

        UsingDbContext(context =>
        {
            var polls = context.Set<Poll>().ToList();

            polls
                .Any(c => c.Question == newPoll.Question)
                .ShouldBeTrue();
        });
    }

    [Fact]
    public async Task UpdateAsync()
    {
        var poll = await _pollAdminAppService.GetAsync(_cmsKitProTestData.PollId);

        var newPoll = await _pollAdminAppService.UpdateAsync(poll.Id, new UpdatePollDto
        {
            Question = "Have you ever used JELLOG framework?",
        });

        UsingDbContext(context =>
        {
            var updatedPoll = context.Set<Poll>().FirstOrDefault(u => u.Id == poll.Id);

            updatedPoll.ShouldNotBeNull();
            updatedPoll.Question.ShouldBe(newPoll.Question);
        });
    }

    [Fact]
    public async Task DeleteAsync()
    {
        await _pollAdminAppService.DeleteAsync(_cmsKitProTestData.PollId);

        UsingDbContext(context =>
        {
            var deletedPoll = context.Set<Poll>().FirstOrDefault(u => u.Id == _cmsKitProTestData.PollId);

            deletedPoll.ShouldBeNull();
        });
    }

    [Fact]
    public async Task GetListAsync()
    {
        var result = await _pollAdminAppService.GetListAsync(new GetPollListInput
        {
            Filter = null
        });

        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(1);
        result.Items.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetAsync()
    {
        var poll = await _pollAdminAppService.GetAsync(_cmsKitProTestData.PollId);

        poll.ShouldNotBeNull();
        poll.Id.ShouldBe(_cmsKitProTestData.PollId);
    }

    [Fact]
    public async Task GetWidgetsAsync()
    {
        var result = await _pollAdminAppService.GetWidgetsAsync();
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetResultAsync()
    {
        var result = await _pollAdminAppService.GetResultAsync(_cmsKitProTestData.PollId);

        result.ShouldNotBeNull();
        result.Question.ShouldBe(_cmsKitProTestData.Question);
    }

}
