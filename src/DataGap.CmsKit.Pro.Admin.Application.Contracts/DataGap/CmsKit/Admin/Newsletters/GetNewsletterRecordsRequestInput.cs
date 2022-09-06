using DataGap.Jellog.Application.Dtos;

namespace DataGap.CmsKit.Admin.Newsletters;

public class GetNewsletterRecordsRequestInput : PagedResultRequestDto
{
    public string Preference { get; set; }

    public string Source { get; set; }
}
