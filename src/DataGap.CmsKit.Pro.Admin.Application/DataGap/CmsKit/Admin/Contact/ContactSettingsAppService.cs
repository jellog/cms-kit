using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DataGap.Jellog.GlobalFeatures;
using DataGap.Jellog.SettingManagement;
using DataGap.CmsKit.GlobalFeatures;
using DataGap.CmsKit.Permissions;

namespace DataGap.CmsKit.Admin.Contact;

[RequiresGlobalFeature(ContactFeature.Name)]
[Authorize(CmsKitProAdminPermissions.Contact.SettingManagement)]
public class ContactSettingsAppService : CmsKitProAdminAppService, IContactSettingsAppService
{
    protected ISettingManager SettingManager { get; }

    public ContactSettingsAppService(ISettingManager settingManager)
    {
        SettingManager = settingManager;
    }

    public virtual async Task<CmsKitContactSettingDto> GetAsync()
    {
        var receiverEmailAddress = await SettingManager.GetOrNullForCurrentTenantAsync(CmsKitProSettingNames.Contact.ReceiverEmailAddress);

        return new CmsKitContactSettingDto
        {
            ReceiverEmailAddress = receiverEmailAddress
        };
    }

    public virtual async Task UpdateAsync(UpdateCmsKitContactSettingDto input)
    {
        await SettingManager.SetForCurrentTenantAsync(CmsKitProSettingNames.Contact.ReceiverEmailAddress, input.ReceiverEmailAddress);
    }
}
