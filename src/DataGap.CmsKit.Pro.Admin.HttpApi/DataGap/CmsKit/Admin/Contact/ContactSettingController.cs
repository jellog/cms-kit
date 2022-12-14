using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog;
using DataGap.Jellog.GlobalFeatures;
using DataGap.CmsKit.GlobalFeatures;
using DataGap.CmsKit.Permissions;

namespace DataGap.CmsKit.Admin.Contact;

[RequiresGlobalFeature(typeof(ContactFeature))]
[RemoteService(Name = CmsKitAdminRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitProAdminRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-admin/contact/settings")]
[Authorize(CmsKitProAdminPermissions.Contact.SettingManagement)]
public class ContactSettingController : CmsKitProAdminController, IContactSettingsAppService
{
    protected IContactSettingsAppService ContactSettingsAppService { get; }

    public ContactSettingController(IContactSettingsAppService contactSettingsAppService)
    {
        ContactSettingsAppService = contactSettingsAppService;
    }

    [HttpGet]
    public virtual Task<CmsKitContactSettingDto> GetAsync()
    {
        return ContactSettingsAppService.GetAsync();
    }

    [HttpPost]
    public virtual Task UpdateAsync(UpdateCmsKitContactSettingDto input)
    {
        return ContactSettingsAppService.UpdateAsync(input);
    }
}
