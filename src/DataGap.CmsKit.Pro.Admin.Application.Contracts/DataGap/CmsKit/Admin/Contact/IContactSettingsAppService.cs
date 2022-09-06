using System.Threading.Tasks;
using DataGap.Jellog.Application.Services;

namespace DataGap.CmsKit.Admin.Contact;

public interface IContactSettingsAppService : IApplicationService
{
    Task<CmsKitContactSettingDto> GetAsync();

    Task UpdateAsync(UpdateCmsKitContactSettingDto input);
}
