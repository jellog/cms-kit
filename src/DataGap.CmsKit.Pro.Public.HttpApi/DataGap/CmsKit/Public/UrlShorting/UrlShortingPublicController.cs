using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog;
using DataGap.Jellog.GlobalFeatures;
using DataGap.CmsKit.GlobalFeatures;
using DataGap.CmsKit.Public.UrlShorting;

namespace DataGap.CmsKit.Public.UrlShorting;

[RequiresGlobalFeature(typeof(UrlShortingFeature))]
[RemoteService(Name = CmsKitProPublicRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitProPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/url-shorting")]
public class UrlShortingPublicController : CmsKitProPublicController, IUrlShortingPublicAppService
{
    protected IUrlShortingPublicAppService UrlShortingPublicAppService { get; }

    public UrlShortingPublicController(IUrlShortingPublicAppService urlShortingPublicAppService)
    {
        UrlShortingPublicAppService = urlShortingPublicAppService;
    }

    [HttpGet]
    public async Task<ShortenedUrlDto> FindBySourceAsync(string source)
    {
        return await UrlShortingPublicAppService.FindBySourceAsync(source);
    }
}
