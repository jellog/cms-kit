using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog.AspNetCore.Mvc;

namespace DataGap.CmsKit.Pro.Controllers;

public class HomeController : JellogController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
