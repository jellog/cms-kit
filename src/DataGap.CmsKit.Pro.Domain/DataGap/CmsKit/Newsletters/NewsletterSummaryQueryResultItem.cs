using System;
using DataGap.Jellog.Auditing;

namespace DataGap.CmsKit.Newsletters;

public class NewsletterSummaryQueryResultItem : IHasCreationTime
{
    public Guid Id { get; set; }

    public string EmailAddress { get; set; }

    public DateTime CreationTime { get; set; }
}
