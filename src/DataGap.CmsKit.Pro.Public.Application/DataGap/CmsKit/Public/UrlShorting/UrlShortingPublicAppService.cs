using System.Threading.Tasks;
using DataGap.Jellog.Application.Services;
using DataGap.Jellog.Caching;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.Public.UrlShorting
{
    public class UrlShortingPublicAppService : ApplicationService, IUrlShortingPublicAppService
    {
        private readonly IShortenedUrlRepository _shortenedUrlRepository;
        private readonly IDistributedCache<ShortenedUrlCacheItem, string> _shortenedUrlCache;

        public UrlShortingPublicAppService(
            IShortenedUrlRepository shortenedUrlRepository,
            IDistributedCache<ShortenedUrlCacheItem, string> shortenedUrlCache)
        {
            _shortenedUrlRepository = shortenedUrlRepository;
            _shortenedUrlCache = shortenedUrlCache;
        }

        public async Task<ShortenedUrlDto> FindBySourceAsync(string source)
        {
            var shortenedUrlInCache = await _shortenedUrlCache.GetAsync(source);

            if (shortenedUrlInCache != null)
            {
                if (!shortenedUrlInCache.Exists)
                {
                    return null;
                }

                return ObjectMapper.Map<ShortenedUrlCacheItem, ShortenedUrlDto>(
                    shortenedUrlInCache
                );
            }

            var shortenedUrl = await _shortenedUrlRepository.FindBySourceUrlAsync(source);

            if (shortenedUrl == null)
            {
                await _shortenedUrlCache.SetAsync(source, new ShortenedUrlCacheItem { Exists = false });

                return null;
            }

            await _shortenedUrlCache.SetAsync(source, new ShortenedUrlCacheItem {
                Id = shortenedUrl.Id,
                Source = shortenedUrl.Source,
                Target = shortenedUrl.Target,
                Exists = true
            });

            return ObjectMapper.Map<ShortenedUrl, ShortenedUrlDto>(
                shortenedUrl
            );
        }
    }
}
