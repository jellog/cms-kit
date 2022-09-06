using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using DataGap.Jellog.Modularity;
using DataGap.CmsKit.Polls;
using Xunit;

namespace DataGap.CmsKit.Pro.Polls;
public abstract class PollRepository_Tests<TStartupModule> : CmsKitProTestBase<TStartupModule>
        where TStartupModule : IJellogModule
{
    private readonly CmsKitProTestData _cmsKitProTestData;
    private readonly IPollRepository _pollRepository;

    protected PollRepository_Tests()
    {
        _cmsKitProTestData = GetRequiredService<CmsKitProTestData>();
        _pollRepository = GetRequiredService<IPollRepository>();
    }
    [Fact]
    public async Task GetListAsync()
    {
        var polls = await _pollRepository.GetListAsync();

        polls.ShouldNotBeNull();
        polls.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GetCountAsync()
    {
        var count = await _pollRepository.GetCountAsync(null);

        count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GetPollWithPollUserVotesAsync()
    {
        var poll = await _pollRepository.GetPollWithPollUserVotesAsync(
            _cmsKitProTestData.PollId);

        poll.Keys.First().Id.ShouldBe(_cmsKitProTestData.PollId);
    }

    [Fact]
    public async Task WithDetailsAsync()
    {
        var pollWithDetails = await _pollRepository.WithDetailsAsync();
        pollWithDetails.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetListByWidgetAsync()
    {
        var polls = await _pollRepository.GetListByWidgetAsync(_cmsKitProTestData.Widget);
        
        polls.ShouldNotBeNull();
        polls.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GetIdByWidgetAsync()
    {
        var poll = await _pollRepository.GetIdByWidgetAsync(_cmsKitProTestData.Widget);
        poll.ShouldBe(_cmsKitProTestData.PollId);
    }
}
