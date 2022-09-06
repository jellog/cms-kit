using System;
using DataGap.Jellog.Application.Dtos;

namespace DataGap.CmsKit.Public.Polls;

[Serializable]
public class PollResultDto : EntityDto
{
    public bool IsSelectedForCurrentUser { get; set; }
    public string Text { get; set; }
    public int VoteCount { get; set; }
}
