using System;
using DataGap.Jellog.Application.Dtos;

namespace DataGap.CmsKit.Admin.Polls;

[Serializable]
public class GetPollListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
