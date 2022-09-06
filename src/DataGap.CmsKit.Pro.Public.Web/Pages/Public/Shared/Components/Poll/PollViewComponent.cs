using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using DataGap.Jellog.AspNetCore.Mvc;
using DataGap.Jellog.AspNetCore.Mvc.UI;
using DataGap.Jellog.AspNetCore.Mvc.UI.Widgets;
using DataGap.CmsKit.Localization;
using DataGap.CmsKit.Public.Polls;

namespace DataGap.CmsKit.Pro.Public.Web.Pages.Public.Shared.Components.Poll;

[Widget(
    ScriptTypes = new[] { typeof(PollWidgetScriptContributor) },
    RefreshUrl = "/CmsKitProPublicWidgets/Poll",
    AutoInitialize = true
)]
[ViewComponent(Name = "CmsPoll")]
public class PollViewComponent : JellogViewComponent
{
    protected IPollPublicAppService PollPublicAppService { get; }
    protected JellogMvcUiOptions JellogMvcUiOptions { get; }

    public PollViewComponent(IPollPublicAppService pollPublicAppService, IOptions<JellogMvcUiOptions> options)
    {
        PollPublicAppService = pollPublicAppService;
        JellogMvcUiOptions = options.Value;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync(string widgetName)
    {
        var poll = await PollPublicAppService.GetPollAsync(new GetPollInput {
            WidgetName = widgetName
        });

        if (poll == null || !CheckDateIntervals(poll))
        {
            return View("~/Pages/Public/Shared/Components/Poll/Default.cshtml", new PollViewModel {
                Id = null
            });
        }
        
        var pollVote = await PollPublicAppService.GetResultAsync(poll.Id);
        var isVoted = pollVote.PollResultDetails.Any(p => p.IsSelectedForCurrentUser);
        var loginUrl = $"{JellogMvcUiOptions.LoginUrl}?returnUrl={HttpContext.Request.Path.ToString()}";

        var viewModel = new PollViewModel()
        {
            Id = poll.Id,
            WidgetName = widgetName,
            Question = poll.Question,
            ShowResultWithoutGivingVote = poll.ShowResultWithoutGivingVote,
            AllowMultipleVote = poll.AllowMultipleVote,
            ShowHoursLeft = poll.ShowHoursLeft,
            ShowVoteCount = poll.ShowVoteCount,
            VoteCount = poll.VoteCount,
            Texts = poll.PollOptions.Select(p => p.Text).ToList(),
            OptionIds = poll.PollOptions.Select(p => p.Id).ToList(),
            IsVoted = isVoted,
            EndDate = poll.EndDate,
            ResultShowingEndDate = poll.ResultShowingEndDate,
            PollVoteCount = pollVote.PollVoteCount,
            PollResultDetails = pollVote.PollResultDetails,
            LoginUrl = loginUrl
        };

        return View("~/Pages/Public/Shared/Components/Poll/Default.cshtml", viewModel);
    }

    private bool CheckDateIntervals(PollWithDetailsDto poll)
    {
        var now = DateTime.Now;
        if (poll.StartDate > now)
        {
            return false;
        }

        if (poll.ResultShowingEndDate.HasValue && poll.ResultShowingEndDate.Value < now)
        {
            return false;
        }
        
        return true;
    }
}

public class PollViewModel
{
    public Guid? Id { get; set; }
    
    [Required]
    [Display(Name = "Question")]
    public string Question { get; set; }
    
    public bool AllowMultipleVote { get; set; }
    
    public int VoteCount { get; set; }
    
    public bool ShowVoteCount { get; set; }
    
    public bool ShowResultWithoutGivingVote { get; set; }
    
    public bool ShowHoursLeft { get; set; }

    public TimeSpan? TimeLeft => GetTimeLeft();

    public bool IsOutdated => CalculateIsOutdated();
    
    public DateTime? EndDate { get; set; }
    
    public DateTime? ResultShowingEndDate { get; set; }
    
    public DateTime CreationTime { get; set; }

    public List<string> Texts { get; set; } = new();
    
    public List<Guid> OptionIds { get; set; } = new();

    //Result
    public int PollVoteCount { get; set; }
    
    public List<PollResultDto> PollResultDetails { get; set; }

    public bool IsVoted { get; set; }
    
    public string WidgetName { get; set; }
    
    public string LoginUrl { get; set; }

    public string GetTimeLeftAsText(IHtmlLocalizer l)
    {
        if (TimeLeft == null || !ShowHoursLeft)
        {
            return string.Empty;
        }

        if (TimeLeft.Value.TotalDays > 365)
        {
            return string.Empty;
        }

        if (TimeLeft.Value.TotalDays > 30)
        {
            var months = (int) (TimeLeft.Value.TotalDays / 30);
            var localizationKey = months == 1 ? "MonthLeft" : "MonthsLeft";
            return l.GetString(localizationKey, months);
        }

        if (TimeLeft.Value.TotalDays > 1)
        {
            var days = (int) TimeLeft.Value.TotalDays;
            var localizationKey = days == 1 ? "DayLeft" : "DaysLeft";
            return l.GetString(localizationKey, days);
        }

        if (TimeLeft.Value.TotalHours > 1)
        {
            var hours = (int) TimeLeft.Value.TotalHours;
            var localizationKey = hours == 1 ? "HourLeft" : "HoursLeft";
            return l.GetString(localizationKey, hours);
        }

        if (TimeLeft.Value.TotalMinutes > 1)
        {
            var minutes = (int) TimeLeft.Value.TotalMinutes;
            var localizationKey = minutes == 1 ? "MinuteLeft" : "MinutesLeft";
            return l.GetString(localizationKey, minutes);
        }
        else
        {
            var seconds = (int) TimeLeft.Value.TotalSeconds;
            var localizationKey = seconds == 1 ? "SecondLeft" : "SecondsLeft";
            return l.GetString(localizationKey, seconds);
        }
    }
    public string GetVoteCountText(IHtmlLocalizer l)
    {
        if (!ShowVoteCount)
        {
            return string.Empty;
        }
        
        if (VoteCount == 0)
        {
            return l["NoVotes"].Value;
        }
        
        if (VoteCount == 1)
        {
            return l.GetString("SingleVoteCount");
        }
        
        return l.GetString("VoteCount{0}", VoteCount);
    }

    private bool CalculateIsOutdated()
    {
        if (TimeLeft == null)
        {
            return false;
        }
        
        return TimeLeft.Value.TotalSeconds < 1;
    }

    public TimeSpan? GetTimeLeft()
    {
        var now = DateTime.Now;
        
        if (!EndDate.HasValue)
        {
            return null;
        }

        if (EndDate.Value <= now)
        {
            return null;
        }

        return EndDate - now;
    }

}
