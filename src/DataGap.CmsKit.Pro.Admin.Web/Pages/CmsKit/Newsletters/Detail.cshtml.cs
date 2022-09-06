using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog.AspNetCore.Mvc.UI.RazorPages;
using DataGap.CmsKit.Admin.Newsletters;

namespace DataGap.CmsKit.Pro.Admin.Web.Pages.CmsKit.Newsletters;

public class DetailModel : JellogPageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public NewsletterRecordWithDetailsDto NewsletterRecordWithDetailsDto { get; set; }

    private readonly INewsletterRecordAdminAppService _newsletterRecordAdminAppService;

    public DetailModel(INewsletterRecordAdminAppService newsletterRecordAdminAppService)
    {
        _newsletterRecordAdminAppService = newsletterRecordAdminAppService;
    }

    public async Task OnGetAsync()
    {
        NewsletterRecordWithDetailsDto = await _newsletterRecordAdminAppService.GetAsync(Id);
    }
}
