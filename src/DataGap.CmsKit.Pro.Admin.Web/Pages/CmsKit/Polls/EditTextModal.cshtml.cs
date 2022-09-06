using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataGap.Jellog.AspNetCore.Mvc.UI.RazorPages;

namespace DataGap.CmsKit.Pro.Admin.Web.Pages.CmsKit.Polls;

public class EditTextModal : JellogPageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Text { get; set; }


    public EditTextModal()
    {
    }

    public async Task OnGetAsync()
    {

    }

    public async Task OnPostAsync()
    {

    }
}
