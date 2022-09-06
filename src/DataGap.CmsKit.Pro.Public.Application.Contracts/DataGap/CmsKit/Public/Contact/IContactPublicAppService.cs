using System.Threading.Tasks;
using DataGap.Jellog.Application.Services;

namespace DataGap.CmsKit.Public.Contact;

public interface IContactPublicAppService : IApplicationService
{
    Task SendMessageAsync(ContactCreateInput input);
}
