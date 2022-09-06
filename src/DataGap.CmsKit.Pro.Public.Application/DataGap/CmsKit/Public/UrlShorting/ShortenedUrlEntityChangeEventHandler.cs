using System.Threading.Tasks;
using DataGap.Jellog.Caching;
using DataGap.Jellog.DependencyInjection;
using DataGap.CmsKit.UrlShorting;
using DataGap.Jellog.Domain.Entities.Events;
using DataGap.Jellog.EventBus;

namespace DataGap.CmsKit.Public.UrlShorting
{
    public class ShortenedUrlEntityChangeEventHandler :
        ILocalEventHandler<EntityUpdatedEventData<ShortenedUrl>>,
        ILocalEventHandler<EntityDeletedEventData<ShortenedUrl>>,
        ITransientDependency
    {
        private readonly IDistributedCache<ShortenedUrlCacheItem, string> _shortenedUrlCache;

        public ShortenedUrlEntityChangeEventHandler(IDistributedCache<ShortenedUrlCacheItem, string> shortenedUrlCache)
        {
            _shortenedUrlCache = shortenedUrlCache;
        }

        public async Task HandleEventAsync(EntityUpdatedEventData<ShortenedUrl> eventData)
        {
            await RemoveFromCache(eventData.Entity);
        }

        public async Task HandleEventAsync(EntityDeletedEventData<ShortenedUrl> eventData)
        {
            await RemoveFromCache(eventData.Entity);
        }

        private async Task RemoveFromCache(ShortenedUrl shortenedUrl)
        {
            await _shortenedUrlCache.RemoveAsync(
                shortenedUrl.Source
                );
        }
    }
}
