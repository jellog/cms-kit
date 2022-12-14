using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog.AspNetCore.Mvc;
using DataGap.Jellog.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using DataGap.Jellog.AspNetCore.Mvc.UI.Widgets;

namespace DataGap.CmsKit.Pro.Public.Web.Pages.Public.Shared.Components.Contact;

[Widget(
    ScriptTypes = new[] { typeof(ContactWidgetScriptContributor) },
    RefreshUrl = "/CmsKitProPublicWidgets/Contact",
    AutoInitialize = true
)]
[ViewComponent(Name = "CmsContact")]
public class ContactViewComponent : JellogViewComponent
{
    public virtual IViewComponentResult Invoke()
    {
        var viewModel = new ContactViewModel();

        return View("~/Pages/Public/Shared/Components/Contact/Default.cshtml", viewModel);
    }
}

public class ContactViewModel
{
    [Required]
    [Display(Name = "Name")]
    [Placeholder("YourFullName")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Subject")]
    [Placeholder("SubjectPlaceholder")]
    public string Subject { get; set; }

    [Required]
    [Display(Name = "EmailAddress")]
    [Placeholder("YourEmailAddress")]
    public string EmailAddress { get; set; }

    [Required]
    [Display(Name = "YourMessage")]
    [Placeholder("HowMayWeHelpYou")]
    [TextArea(Rows = 3)]
    public string Message { get; set; }

    [HiddenInput]
    public string RecaptchaToken { get; set; }
}
