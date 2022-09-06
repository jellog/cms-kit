using System.ComponentModel.DataAnnotations;
using DataGap.Jellog.Validation;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.Admin.UrlShorting
{
    public class UpdateShortenedUrlDto
    {
        [Required]
        [DynamicMaxLength(typeof(ShortenedUrlConst), nameof(ShortenedUrlConst.MaxTargetLength))]
        public string Target { get; set; }
    }
}
