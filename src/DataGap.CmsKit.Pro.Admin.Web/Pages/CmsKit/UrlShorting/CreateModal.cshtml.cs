using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DataGap.Jellog.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using DataGap.Jellog.Validation;
using DataGap.CmsKit.Admin.UrlShorting;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.Pro.Admin.Web.Pages.CmsKit.UrlShorting;

public class CreateModalModel : AdminPageModel
{
    protected IUrlShortingAdminAppService UrlShortingAdminAppService { get; }

    [BindProperty]
    public CreateShortenedUrlViewModel ViewModel { get; set; }

    public CreateModalModel(IUrlShortingAdminAppService urlShortingAdminAppService)
    {
        UrlShortingAdminAppService = urlShortingAdminAppService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateShortenedUrlViewModel, CreateShortenedUrlDto>(ViewModel);

        await UrlShortingAdminAppService.CreateAsync(dto);

        return NoContent();
    }

    public class CreateShortenedUrlViewModel
    {
        [Required]
        [DynamicMaxLength(typeof(ShortenedUrlConst), nameof(ShortenedUrlConst.MaxSourceLength))]
        [InputInfoText("UrlForwarding:EnsureTheUrlIsStartingWithSlash")]
        public string Source { get; set; }

        [Required]
        [DynamicMaxLength(typeof(ShortenedUrlConst), nameof(ShortenedUrlConst.MaxTargetLength))]
        [InputInfoText("UrlForwarding:EnsureTheUrlIsStartingWithSlashIfSameDomain")]
        public string Target { get; set; }
    }
}
