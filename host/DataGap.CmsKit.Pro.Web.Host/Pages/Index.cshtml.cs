using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace DataGap.CmsKit.Pro.Pages;

public class IndexModel : ProPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
