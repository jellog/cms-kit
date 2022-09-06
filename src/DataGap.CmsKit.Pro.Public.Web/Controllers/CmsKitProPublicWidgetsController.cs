using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog.AspNetCore.Mvc;
using DataGap.CmsKit.Pro.Public.Web.Pages.Public.Shared.Components.Contact;
using DataGap.CmsKit.Pro.Public.Web.Pages.Public.Shared.Components.Poll;

namespace DataGap.CmsKit.Pro.Public.Web.Controllers;

public class CmsKitProPublicWidgetsController : JellogController
{
    public IActionResult Contact()
    {
        return ViewComponent(typeof(ContactViewComponent));
    }

    public Task<IActionResult> Poll(string widgetName)
    {
        return Task.FromResult((IActionResult)ViewComponent(typeof(PollViewComponent), new { widgetName }));
    }
}
