using DataGap.Jellog.Application.Dtos;

namespace DataGap.CmsKit.Admin.UrlShorting
{
    public class GetShortenedUrlListInput : PagedAndSortedResultRequestDto
    {
        public string ShortenedUrlFilter { get; set; }
    }
}
