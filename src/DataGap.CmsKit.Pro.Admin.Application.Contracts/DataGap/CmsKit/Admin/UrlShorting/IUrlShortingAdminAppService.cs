using System;
using DataGap.Jellog.Application.Services;

namespace DataGap.CmsKit.Admin.UrlShorting
{
    public interface IUrlShortingAdminAppService
        : ICrudAppService<
            ShortenedUrlDto,
            ShortenedUrlDto,
            Guid,
            GetShortenedUrlListInput,
            CreateShortenedUrlDto,
            UpdateShortenedUrlDto
        >
    {
    }
}
