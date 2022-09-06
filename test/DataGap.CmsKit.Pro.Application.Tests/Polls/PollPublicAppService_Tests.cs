using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using DataGap.CmsKit.Polls;
using DataGap.CmsKit.Public.Polls;
using Xunit;

namespace DataGap.CmsKit.Pro.Polls;
public class PollPublicAppService_Tests : CmsKitProApplicationTestBase
{
    private readonly IPollPublicAppService _pollPublicAppService;
    private readonly CmsKitProTestData _cmsKitProTestData;

    public PollPublicAppService_Tests()
    {
        _pollPublicAppService = GetRequiredService<IPollPublicAppService>();
        _cmsKitProTestData = GetRequiredService<CmsKitProTestData>();
    }

    [Fact]
    public async Task GetPollAsync()
    {
        var poll = await _pollPublicAppService.GetPollAsync(new GetPollInput() { WidgetName = _cmsKitProTestData.Widget });

        poll.ShouldNotBeNull();
        poll.Id.ShouldBe(_cmsKitProTestData.PollId);
    }

    [Fact]
    public async Task SubmitVoteAsync()
    {
        await _pollPublicAppService.SubmitVoteAsync(
            _cmsKitProTestData.PollId,
            new SubmitPollInput()
            {
                PollOptionIds = new[] { _cmsKitProTestData.PollOptionId }
            });

        UsingDbContext(context =>
        {
            var pollUserVotes = context.Set<PollUserVote>().ToList();

            pollUserVotes
                .Any(c => c.PollId == _cmsKitProTestData.PollId)
                .ShouldBeTrue();
        });
    }

    [Fact]
    public async Task GetResultAsync()
    {
        var url = await _pollPublicAppService.GetResultAsync(_cmsKitProTestData.PollId);

        url.ShouldNotBeNull();
        url.Question.ShouldBe(_cmsKitProTestData.Question);
    }

}
