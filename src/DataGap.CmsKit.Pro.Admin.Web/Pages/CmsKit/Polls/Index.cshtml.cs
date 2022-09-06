using System.ComponentModel;
using System.Threading.Tasks;
using DataGap.Jellog.AspNetCore.Mvc.UI.RazorPages;

namespace DataGap.CmsKit.Pro.Admin.Web.Pages.CmsKit.Polls;

public class IndexModel : JellogPageModel
{
    [DisplayName("Filter")]
    public string Filter { get; set; }

    public IndexModel()
    {

    }

    public async Task OnGetAsync()
    {

    }
}
