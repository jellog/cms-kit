using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog.AspNetCore.Mvc;
using DataGap.CmsKit.Admin.Contact;

namespace DataGap.CmsKit.Pro.Admin.Web.Pages.CmsKit.Components.CmsKitProSettingGroup;

public class CmsKitProSettingGroupViewComponent : JellogViewComponent
{
    protected IContactSettingsAppService ContactSettingsAppService { get; }

    public CmsKitProSettingGroupViewComponent(IContactSettingsAppService contactSettingsAppService)
    {
        ContactSettingsAppService = contactSettingsAppService;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await ContactSettingsAppService.GetAsync();

        return View("~/Pages/CmsKit/Components/CmsKitProSettingGroup/Default.cshtml", model);
    }
}
