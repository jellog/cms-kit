using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataGap.CmsKit.Admin.Polls;

namespace DataGap.CmsKit.Pro.Admin.Web.Pages.CmsKit.Polls;

public class ResultModal : AdminPageModel
{
    protected IPollAdminAppService PollAdminAppService { get; }

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public PollResultViewModel ViewModel { get; set; }

    public ResultModal(IPollAdminAppService pollAdminAppService )
    {
        PollAdminAppService = pollAdminAppService;
    }

    public async Task OnGetAsync()
    {
        var pollVote = await PollAdminAppService.GetResultAsync(Id);

        ViewModel = new PollResultViewModel()
        {
            Question = pollVote.Question,
            PollVoteCount = pollVote.PollVoteCount,
            PollResultDetails = pollVote.PollResultDetails
        };
    }

    public class PollResultViewModel
    {
        public string Question { get; set; }
        public int PollVoteCount { get; set; }
        public List<PollResultDto> PollResultDetails { get; set; } = new();
    }

    
}
