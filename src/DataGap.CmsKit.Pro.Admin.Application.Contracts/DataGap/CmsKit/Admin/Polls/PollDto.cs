using System;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.Auditing;

namespace DataGap.CmsKit.Admin.Polls;

[Serializable]
public class PollDto : EntityDto<Guid>, IHasCreationTime
{
    public string Question { get; set; }
    
    public string Widget { get; set; }
    
    public bool AllowMultipleVote { get; set; }
    
    public int VoteCount { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public DateTime CreationTime { get; set; }
}
