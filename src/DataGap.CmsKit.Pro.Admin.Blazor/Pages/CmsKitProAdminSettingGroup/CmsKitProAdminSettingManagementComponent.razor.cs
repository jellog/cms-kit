using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using DataGap.Jellog.AspNetCore.Components.Messages;
using DataGap.Jellog.AspNetCore.Components.Web.Configuration;
using DataGap.Jellog.GlobalFeatures;
using DataGap.CmsKit.Admin.Contact;
using DataGap.CmsKit.GlobalFeatures;

namespace DataGap.CmsKit.Pro.Admin.Blazor.Pages.CmsKitProAdminSettingGroup;

public partial class CmsKitProAdminSettingManagementComponent
{
    [Inject]
    protected IContactSettingsAppService ContactSettingsAppService { get; set; }

    [Inject]
    protected IUiMessageService UiMessageService { get; set; }

    [Inject]
    private ICurrentApplicationConfigurationCacheResetService CurrentApplicationConfigurationCacheResetService { get; set; }

    protected CmsKitProSettingsModel Settings;

    protected string SelectedTab = "Contact";

    protected Validations ContactSettingValidation;

    protected override async Task OnInitializedAsync()
    {
        if (!GlobalFeatureManager.Instance.IsEnabled<ContactFeature>())
        {
            return;
        }

        Settings = new CmsKitProSettingsModel
        {
            ContactSetting = await ContactSettingsAppService.GetAsync()
        };
    }

    protected virtual async Task UpdateContactSettingsAsync()
    {
        if (!await ContactSettingValidation.ValidateAll())
        {
            return;
        }

        await ContactSettingsAppService.UpdateAsync(new UpdateCmsKitContactSettingDto
        {
            ReceiverEmailAddress = Settings.ContactSetting.ReceiverEmailAddress
        });

        await CurrentApplicationConfigurationCacheResetService.ResetAsync();

        await UiMessageService.Success(L["SuccessfullySaved"]);
    }
}

public class CmsKitProSettingsModel
{
    public CmsKitContactSettingDto ContactSetting { get; set; }
}
