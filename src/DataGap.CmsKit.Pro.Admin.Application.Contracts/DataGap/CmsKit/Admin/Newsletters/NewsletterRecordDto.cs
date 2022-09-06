using System;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.Auditing;

namespace DataGap.CmsKit.Admin.Newsletters;

public class NewsletterRecordDto : EntityDto<Guid>, IHasCreationTime
{
    public string EmailAddress { get; set; }

    public DateTime CreationTime { get; set; }
}
