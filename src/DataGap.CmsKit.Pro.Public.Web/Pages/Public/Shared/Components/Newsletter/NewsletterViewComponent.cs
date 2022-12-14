using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using DataGap.Jellog;
using DataGap.Jellog.AspNetCore.Mvc;
using DataGap.Jellog.AspNetCore.Mvc.UI.Widgets;
using DataGap.Jellog.Localization;
using DataGap.CmsKit.Public.Newsletters;

namespace DataGap.CmsKit.Pro.Public.Web.Pages.Public.Shared.Components.Newsletter;

[Widget(
    StyleFiles = new[] { "/Pages/Public/Shared/Components/Newsletter/Default.css" },
    ScriptTypes = new[] { typeof(NewsletterWidgetScriptContributor) },
    AutoInitialize = true
)]
[ViewComponent(Name = "CmsNewsletter")]
public class NewsletterViewComponent : JellogViewComponent
{
    protected INewsletterRecordPublicAppService NewsletterRecordPublicAppService { get; }

    protected IStringLocalizerFactory StringLocalizerFactory { get; }

    public NewsletterViewComponent(
        INewsletterRecordPublicAppService newsletterRecordPublicAppService,
        IStringLocalizerFactory stringLocalizerFactory)
    {
        NewsletterRecordPublicAppService = newsletterRecordPublicAppService;
        StringLocalizerFactory = stringLocalizerFactory;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync(
        [NotNull] string preference,
        [NotNull] string source,
        bool requestAdditionalPreferencesLater,
        [CanBeNull] ILocalizableString privacyPolicyConfirmation)
    {
        Check.NotNullOrWhiteSpace(preference, nameof(preference));
        Check.NotNullOrWhiteSpace(source, nameof(source));

        var newsletterEmailOptionsDto = await NewsletterRecordPublicAppService.GetOptionByPreferenceAsync(preference);

        var localizedString = privacyPolicyConfirmation == null
            ? newsletterEmailOptionsDto.PrivacyPolicyConfirmation
            : privacyPolicyConfirmation.Localize(StringLocalizerFactory).Value;

        var viewModel = new NewsletterViewModel
        {
            Preference = preference,
            Source = source,
            NormalizedSource = source.Replace('.', '_'),
            PrivacyPolicyConfirmation = localizedString,
            RequestAdditionalPreferencesLater = requestAdditionalPreferencesLater,
            AdditionalPreferences = newsletterEmailOptionsDto.AdditionalPreferences,
            DisplayAdditionalPreferences = newsletterEmailOptionsDto.DisplayAdditionalPreferences
        };

        newsletterEmailOptionsDto.WidgetViewPath ??= "~/Pages/Public/Shared/Components/Newsletter/Default.cshtml";

        return View(newsletterEmailOptionsDto.WidgetViewPath, viewModel);
    }
}

public class NewsletterViewModel
{
    public string Preference { get; set; }

    public string Source { get; set; }

    public string NormalizedSource { get; set; }

    [CanBeNull]
    public string PrivacyPolicyConfirmation { get; set; }

    public bool RequestAdditionalPreferencesLater { get; set; }

    public List<string> AdditionalPreferences { get; set; }

    public List<string> DisplayAdditionalPreferences { get; set; }

}
