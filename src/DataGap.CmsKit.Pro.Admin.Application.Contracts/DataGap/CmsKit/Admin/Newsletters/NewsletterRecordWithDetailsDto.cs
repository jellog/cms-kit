using System;
using System.Collections.Generic;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.Auditing;

namespace DataGap.CmsKit.Admin.Newsletters;

public class NewsletterRecordWithDetailsDto : EntityDto<Guid>, IHasCreationTime
{
    public string EmailAddress { get; set; }

    public ICollection<NewsletterPreferenceDto> Preferences { get; set; }

    public DateTime CreationTime { get; set; }
}
