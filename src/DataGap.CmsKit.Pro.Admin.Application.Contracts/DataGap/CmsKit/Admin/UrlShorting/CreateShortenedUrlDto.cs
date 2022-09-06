using System.ComponentModel.DataAnnotations;
using DataGap.Jellog.Validation;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.Admin.UrlShorting
{
    public class CreateShortenedUrlDto
    {
        [Required]
        [DynamicMaxLength(typeof(ShortenedUrlConst), nameof(ShortenedUrlConst.MaxSourceLength))]
        public string Source { get; set; }

        [Required]
        [DynamicMaxLength(typeof(ShortenedUrlConst), nameof(ShortenedUrlConst.MaxTargetLength))]
        public string Target { get; set; }
    }
}
