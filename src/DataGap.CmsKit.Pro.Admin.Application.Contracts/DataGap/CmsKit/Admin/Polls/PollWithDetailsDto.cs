using System;
using System.Collections.Generic;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.Auditing;

namespace DataGap.CmsKit.Admin.Polls;

[Serializable]
public class PollWithDetailsDto : EntityDto<Guid>, IHasCreationTime
{
    public string Question { get; set; }
    
    public string Widget { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public bool AllowMultipleVote { get; set; }
    
    public int VoteCount { get; set; }
    
    public bool ShowVoteCount { get; set; }
    
    public bool ShowResultWithoutGivingVote { get; set; }
    
    public bool ShowHoursLeft { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public DateTime? ResultShowingEndDate { get; set; }
    
    public Guid? TenantId { get; set; }
    
    public DateTime CreationTime { get; set; }
    
    public List<PollOptionDto> PollOptions { get; set; } = new();
}