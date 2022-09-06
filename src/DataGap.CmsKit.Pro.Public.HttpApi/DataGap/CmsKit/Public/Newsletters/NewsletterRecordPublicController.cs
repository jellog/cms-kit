using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog;
using DataGap.Jellog.GlobalFeatures;
using DataGap.CmsKit.GlobalFeatures;

namespace DataGap.CmsKit.Public.Newsletters;

[RequiresGlobalFeature(typeof(NewslettersFeature))]
[RemoteService(Name = CmsKitProPublicRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitProPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/newsletter")]
public class NewsletterRecordPublicController : CmsKitProPublicController, INewsletterRecordPublicAppService
{
    protected INewsletterRecordPublicAppService NewsletterRecordPublicAppService { get; }

    public NewsletterRecordPublicController(INewsletterRecordPublicAppService newsletterRecordPublicAppService)
    {
        NewsletterRecordPublicAppService = newsletterRecordPublicAppService;
    }

    [HttpPost]
    public virtual Task CreateAsync(CreateNewsletterRecordInput input)
    {
        return NewsletterRecordPublicAppService.CreateAsync(input);
    }

    [HttpGet]
    [Route("emailAddress")]
    public virtual Task<List<NewsletterPreferenceDetailsDto>> GetNewsletterPreferencesAsync(string emailAddress)
    {
        return NewsletterRecordPublicAppService.GetNewsletterPreferencesAsync(emailAddress);
    }

    [HttpPut]
    public virtual async Task UpdatePreferencesAsync(UpdatePreferenceRequestInput input)
    {
        await NewsletterRecordPublicAppService.UpdatePreferencesAsync(input);
    }

    [HttpGet]
    [Route("preference-options")]
    public virtual async Task<NewsletterEmailOptionsDto> GetOptionByPreferenceAsync(string preference)
    {
        return await NewsletterRecordPublicAppService.GetOptionByPreferenceAsync(preference);
    }
}
