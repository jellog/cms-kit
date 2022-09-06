using System.Threading.Tasks;
using DataGap.Jellog.Application.Services;

namespace DataGap.CmsKit.Public.UrlShorting
{
    public interface IUrlShortingPublicAppService : IApplicationService
    {
        Task<ShortenedUrlDto> FindBySourceAsync(string source);
    }
}
