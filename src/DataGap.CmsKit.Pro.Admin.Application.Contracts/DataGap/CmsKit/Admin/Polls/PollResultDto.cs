using System;
using DataGap.Jellog.Application.Dtos;

namespace DataGap.CmsKit.Admin.Polls;

[Serializable]
public class PollResultDto : EntityDto
{
    public string Text { get; set; }
    public int VoteCount { get; set; }
}
