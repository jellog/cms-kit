using System;
using DataGap.Jellog.Application.Dtos;

namespace DataGap.CmsKit.Public.Polls;

[Serializable]
public class PollOptionDto : EntityDto<Guid>
{
    public string Text { get; set; }
}