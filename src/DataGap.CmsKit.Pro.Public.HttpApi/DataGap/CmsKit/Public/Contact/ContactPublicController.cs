using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Owl.reCAPTCHA;
using Owl.reCAPTCHA.v3;
using DataGap.Jellog;
using DataGap.Jellog.GlobalFeatures;
using DataGap.CmsKit.GlobalFeatures;

namespace DataGap.CmsKit.Public.Contact;

[RequiresGlobalFeature(typeof(ContactFeature))]
[RemoteService(Name = CmsKitProPublicRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitProPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/contacts")]
public class ContactPublicController : CmsKitProPublicController, IContactPublicAppService
{
    protected IContactPublicAppService ContactPublicAppService { get; }

    protected IreCAPTCHASiteVerifyV3 SiteVerify { get; }

    public ContactPublicController(IContactPublicAppService contactPublicAppService, IreCAPTCHASiteVerifyV3 siteVerify)
    {
        ContactPublicAppService = contactPublicAppService;
        SiteVerify = siteVerify;
    }

    [HttpPost]
    public virtual async Task SendMessageAsync(ContactCreateInput input)
    {
        var response = await SiteVerify.Verify(new reCAPTCHASiteVerifyRequest
        {
            Response = input.RecaptchaToken,
            RemoteIp = HttpContext.Connection.RemoteIpAddress.ToString()
        });

        if (response.Success && response.Score > 0.5)
        {
            await ContactPublicAppService.SendMessageAsync(input);
        }
        else
        {
            throw new UserFriendlyException(L["RecaptchaError"]);
        }
    }
}
