using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataGap.Jellog.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using DataGap.Jellog.AspNetCore.Mvc.UI.RazorPages;
using DataGap.CmsKit.Admin.Newsletters;

namespace DataGap.CmsKit.Pro.Admin.Web.Pages.CmsKit.Newsletters;

public class IndexModel : JellogPageModel
{
    public SelectList PreferencesSelectList { get; set; }

    [SelectItems(nameof(PreferencesSelectList))]
    public string Preference { get; set; }

    [DisplayName("Source")]
    public string Source { get; set; }

    private readonly INewsletterRecordAdminAppService _newsletterRecordAdminAppService;

    public IndexModel(INewsletterRecordAdminAppService newsletterRecordAdminAppService)
    {
        _newsletterRecordAdminAppService = newsletterRecordAdminAppService;
    }

    public async Task OnGetAsync()
    {
        var newsletterPreferences = await _newsletterRecordAdminAppService.GetNewsletterPreferencesAsync();
        newsletterPreferences.AddFirst("");

        PreferencesSelectList = new SelectList(newsletterPreferences);
    }
}
