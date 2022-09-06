using System;
using JetBrains.Annotations;
using DataGap.Jellog;

namespace DataGap.CmsKit.UrlShorting
{
    [Serializable]
    public class ShortenedUrlAlreadyExistsException : BusinessException
    {
        public ShortenedUrlAlreadyExistsException([NotNull] string source)
        : base(CmsKitProErrorCodes.UrlShorting.ShortenedUrlAlreadyExistsException)
        {
            WithData(nameof(ShortenedUrl.Source), source);
        }
    }
}
