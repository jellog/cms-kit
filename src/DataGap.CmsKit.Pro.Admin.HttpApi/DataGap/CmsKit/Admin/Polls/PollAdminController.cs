using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.GlobalFeatures;
using DataGap.CmsKit.GlobalFeatures;
using DataGap.CmsKit.Permissions;

namespace DataGap.CmsKit.Admin.Polls;

[RequiresGlobalFeature(typeof(PollsFeature))]
[RemoteService(Name = CmsKitAdminRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitProAdminRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-admin/poll")]
[Authorize(CmsKitProAdminPermissions.Polls.Default)]
public class PollAdminController : CmsKitProAdminController, IPollAdminAppService
{
    private readonly IPollAdminAppService _pollAdminAppService;

    public PollAdminController(IPollAdminAppService polingAdminAppService)
    {
        _pollAdminAppService = polingAdminAppService;
    }

    [HttpGet]
    public Task<PagedResultDto<PollDto>> GetListAsync(GetPollListInput input)
    {
        return _pollAdminAppService.GetListAsync(input);
    }

    [HttpGet]
    [Route("{id}")]
    public Task<PollWithDetailsDto> GetAsync(Guid id)
    {
        return _pollAdminAppService.GetAsync(id);
    }

    [HttpPost]
    [Authorize(CmsKitProAdminPermissions.Polls.Create)]
    public Task<PollWithDetailsDto> CreateAsync(CreatePollDto input)
    {
        return _pollAdminAppService.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(CmsKitProAdminPermissions.Polls.Update)]
    public Task<PollWithDetailsDto> UpdateAsync(Guid id, UpdatePollDto input)
    {
        return _pollAdminAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(CmsKitProAdminPermissions.Polls.Delete)]
    public Task DeleteAsync(Guid id)
    {
        return _pollAdminAppService.DeleteAsync(id);
    }

    [HttpGet]
    [Route("widgets")]
    public Task<ListResultDto<PollWidgetDto>> GetWidgetsAsync()
    {
        return _pollAdminAppService.GetWidgetsAsync();
    }

    [HttpGet]
    [Route("result")]
    public Task<GetResultDto> GetResultAsync(Guid id)
    {
        return _pollAdminAppService.GetResultAsync(id);
    }
}
