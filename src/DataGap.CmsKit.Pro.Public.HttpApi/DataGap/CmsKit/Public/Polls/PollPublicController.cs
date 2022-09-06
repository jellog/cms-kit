using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog;
using DataGap.Jellog.GlobalFeatures;
using DataGap.CmsKit.GlobalFeatures;

namespace DataGap.CmsKit.Public.Polls;

[RequiresGlobalFeature(typeof(PollsFeature))]
[RemoteService(Name = CmsKitProPublicRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitProPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/poll")]
public class PollPublicController : CmsKitProPublicController, IPollPublicAppService
{
    protected IPollPublicAppService PollPublicAppService { get; }

    public PollPublicController(IPollPublicAppService pollPublicAppService)
    {
        PollPublicAppService = pollPublicAppService;
    }

    [HttpGet]
    [Route("getpoll")]
    public async Task<PollWithDetailsDto> GetPollAsync(GetPollInput input)
    {
        return await PollPublicAppService.GetPollAsync(input);
    }

    [HttpGet]
    [Route("showresult")]
    public async Task<GetResultDto> GetResultAsync(Guid id)
    {
        return await PollPublicAppService.GetResultAsync(id);
    }

    [HttpPost]
    [Route("{id}")]
    public async Task SubmitVoteAsync(Guid id, SubmitPollInput submitPollInput)
    {
        await PollPublicAppService.SubmitVoteAsync(id, submitPollInput);
    }
}
