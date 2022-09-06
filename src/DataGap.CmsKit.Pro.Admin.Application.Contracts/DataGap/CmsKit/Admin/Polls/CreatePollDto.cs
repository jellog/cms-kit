using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using DataGap.Jellog.Validation;
using DataGap.CmsKit.Polls;

namespace DataGap.CmsKit.Admin.Polls;

[Serializable]
public class CreatePollDto
{
    [Required]
    [DynamicMaxLength(typeof(PollConst), nameof(PollConst.MaxQuestionLength))]
    public string Question { get; set; }
    
    [DynamicMaxLength(typeof(PollConst), nameof(PollConst.MaxWidgetNameLength))]
    public string Widget { get; set; }
    
    public bool AllowMultipleVote { get; set; }
    
    public bool ShowVoteCount { get; set; }
    
    public bool ShowResultWithoutGivingVote { get; set; }
    
    public bool ShowHoursLeft { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public DateTime? ResultShowingEndDate { get; set; }

    public Collection<PollOptionDto> PollOptions { get; set; } = new();



}
