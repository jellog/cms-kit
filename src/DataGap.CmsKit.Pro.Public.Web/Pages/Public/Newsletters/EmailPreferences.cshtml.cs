using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog.AspNetCore.Mvc.UI.RazorPages;
using DataGap.CmsKit.Newsletters.Helpers;
using DataGap.CmsKit.Public.Newsletters;

namespace DataGap.CmsKit.Pro.Public.Web.Pages.Public.Newsletters;

public class EmailPreferencesModel : JellogPageModel
{
    protected INewsletterRecordPublicAppService NewsletterRecordPublicAppService { get; }

    protected SecurityCodeProvider SecurityCodeProvider { get; }

    public List<NewsletterPreferenceDetailsViewModel> NewsletterPreferenceDetailsViewModels { get; set; }

    public string EmailAddress { get; set; }

    public EmailPreferencesModel(
        INewsletterRecordPublicAppService newsletterRecordPublicAppService,
        SecurityCodeProvider securityCodeProvider)
    {
        NewsletterRecordPublicAppService = newsletterRecordPublicAppService;
        SecurityCodeProvider = securityCodeProvider;

        NewsletterPreferenceDetailsViewModels = new List<NewsletterPreferenceDetailsViewModel>();
    }

    public async Task<IActionResult> OnGetAsync(string emailAddress, string securityCode)
    {
        var hashSecurityCode = SecurityCodeProvider.GetSecurityCode(emailAddress);

        if (securityCode != hashSecurityCode)
        {
            return Unauthorized();
        }

        EmailAddress = emailAddress;

        var newsletterPreferenceDetailsDto = await NewsletterRecordPublicAppService.GetNewsletterPreferencesAsync(emailAddress);

        foreach (var newsletterPreference in newsletterPreferenceDetailsDto)
        {
            NewsletterPreferenceDetailsViewModels.Add(new NewsletterPreferenceDetailsViewModel
            {
                Preference = newsletterPreference.Preference,
                DisplayPreference = newsletterPreference.DisplayPreference,
                DisplayDefinition = newsletterPreference.Definition,
                IsSelectedByEmailAddress = newsletterPreference.IsSelectedByEmailAddress
            });
        }

        return Page();
    }
}

public class NewsletterPreferenceDetailsViewModel
{
    public string Preference { get; set; }

    public string DisplayPreference { get; set; }

    public string DisplayDefinition { get; set; }

    public bool IsSelectedByEmailAddress { get; set; }
}
