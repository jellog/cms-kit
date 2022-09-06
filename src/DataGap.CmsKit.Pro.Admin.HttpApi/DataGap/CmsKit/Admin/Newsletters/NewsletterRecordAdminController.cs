using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.Content;
using DataGap.Jellog.GlobalFeatures;
using DataGap.CmsKit.GlobalFeatures;
using DataGap.CmsKit.Permissions;

namespace DataGap.CmsKit.Admin.Newsletters;

[RequiresGlobalFeature(typeof(NewslettersFeature))]
[RemoteService(Name = CmsKitAdminRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitProAdminRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-admin/newsletter")]
[Authorize(CmsKitProAdminPermissions.Newsletters.Default)]
public class NewsletterRecordAdminController : CmsKitProAdminController, INewsletterRecordAdminAppService
{
    protected INewsletterRecordAdminAppService NewsletterRecordAdminAppService { get; }

    public NewsletterRecordAdminController(INewsletterRecordAdminAppService newsletterRecordAdminAppService)
    {
        NewsletterRecordAdminAppService = newsletterRecordAdminAppService;
    }

    [HttpGet]
    public Task<PagedResultDto<NewsletterRecordDto>> GetListAsync(GetNewsletterRecordsRequestInput input)
    {
        return NewsletterRecordAdminAppService.GetListAsync(input);
    }

    [HttpGet]
    [Route("{id}")]
    public Task<NewsletterRecordWithDetailsDto> GetAsync(Guid id)
    {
        return NewsletterRecordAdminAppService.GetAsync(id);
    }

    [HttpGet]
    [Route("csv-detail")]
    public Task<List<NewsletterRecordCsvDto>> GetNewsletterRecordsCsvDetailAsync(GetNewsletterRecordsCsvRequestInput input)
    {
        return NewsletterRecordAdminAppService.GetNewsletterRecordsCsvDetailAsync(input);
    }

    [HttpGet]
    [Route("preferences")]
    public Task<List<string>> GetNewsletterPreferencesAsync()
    {
        return NewsletterRecordAdminAppService.GetNewsletterPreferencesAsync();
    }

    [HttpGet]
    [Route("export-csv")]
    public async Task<IRemoteStreamContent> GetCsvResponsesAsync(GetNewsletterRecordsCsvRequestInput input)
    {
        return await NewsletterRecordAdminAppService.GetCsvResponsesAsync(input);
    }
}
