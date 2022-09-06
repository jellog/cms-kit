using System;
using DataGap.Jellog.Application.Dtos;

namespace DataGap.CmsKit.Admin.UrlShorting
{
    public class ShortenedUrlDto : CreationAuditedEntityDto<Guid>
    {
        public string Source { get; set; }

        public string Target { get; set; }
    }
}
