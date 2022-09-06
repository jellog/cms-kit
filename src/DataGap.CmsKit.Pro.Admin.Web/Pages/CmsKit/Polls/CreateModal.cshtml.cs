using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataGap.Jellog.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using DataGap.Jellog.Validation;
using DataGap.CmsKit.Admin.Polls;
using DataGap.CmsKit.Polls;

namespace DataGap.CmsKit.Pro.Admin.Web.Pages.CmsKit.Polls;

public class CreateModalModel : AdminPageModel
{
    protected IPollAdminAppService PollAdminAppService { get; }

    [BindProperty]
    public CreatePollViewModel ViewModel { get; set; }

    public string NewOption { get; set; }

    public List<SelectListItem> Widgets { get; set; } = new ();

    public CreateModalModel(IPollAdminAppService pollAdminAppService)
    {
        PollAdminAppService = pollAdminAppService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var pollDto = ObjectMapper.Map<CreatePollViewModel, CreatePollDto>(ViewModel);
        var pollOptions = pollDto.PollOptions.Where(o => o.Text != null).ToList();
        pollDto.PollOptions = new Collection<PollOptionDto>(pollOptions);

        var created = await PollAdminAppService.CreateAsync(pollDto);

        return new OkObjectResult(created);
    }

    public async Task OnGetAsync()
    {
        ViewModel = new CreatePollViewModel();
        
        Widgets = new List<SelectListItem>(){new ("", "")};
        Widgets.AddRange((await PollAdminAppService.GetWidgetsAsync())
            .Items
            .Select(w => new SelectListItem(L[$"DisplayName:{w.Name}"].Value, w.Name))
            .ToList());
    }

    [AutoMap(typeof(CreatePollDto), ReverseMap = true)]
    public class CreatePollViewModel
    {
        [Required]
        [DynamicMaxLength(typeof(PollConst), nameof(PollConst.MaxQuestionLength))]
        public string Question { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow.Date;
        
        public DateTime? EndDate { get; set; }
        
        public DateTime? ResultShowingEndDate { get; set; }
        
        public bool AllowMultipleVote { get; set; }
        
        public bool ShowVoteCount { get; set; }
        
        public bool ShowResultWithoutGivingVote { get; set; }
        
        public bool ShowHoursLeft { get; set; }
        
        [SelectItems(nameof(Widgets))]
        public string Widget { get; set; }
        
        public PollOptionDto[] PollOptions { get; set; } = Array.Empty<PollOptionDto>();
    }
}
