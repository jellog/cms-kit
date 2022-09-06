using System;

namespace DataGap.CmsKit.UrlShorting
{
    public class ShortenedUrlCacheItem
    {
        public Guid Id { get; set; }

        public string Source { get; set; }

        public string Target { get; set; }

        public bool Exists { get; set; } = true;
    }
}
