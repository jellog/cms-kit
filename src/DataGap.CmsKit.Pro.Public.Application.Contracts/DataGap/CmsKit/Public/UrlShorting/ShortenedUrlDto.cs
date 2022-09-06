using System;
using DataGap.Jellog.Application.Dtos;

namespace DataGap.CmsKit.Public.UrlShorting
{
    public class ShortenedUrlDto
    {
        public Guid Id { get; set; }

        public string Source { get; set; }

        public string Target { get; set; }
    }
}
