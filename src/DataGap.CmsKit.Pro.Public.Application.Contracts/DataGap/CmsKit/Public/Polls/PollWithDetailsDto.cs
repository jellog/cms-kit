using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.Auditing;

namespace DataGap.CmsKit.Public.Polls;

[Serializable]
public class PollWithDetailsDto : EntityDto<Guid>, IHasCreationTime
{
    public List<PollOptionDto> PollOptions { get; set; } = new();
    public string Question { get; set; }
    
    public DateTime StartDate { get; set; }
    public bool AllowMultipleVote { get; set; }
    public int VoteCount { get; set; }

    public bool ShowVoteCount { get; set; }
    
    public bool ShowResultWithoutGivingVote { get; set; }
    
    public bool ShowHoursLeft { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public DateTime? ResultShowingEndDate { get; set; }
    
    public DateTime CreationTime { get; set; }
}
