using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataGap.Jellog.Domain.Repositories;

namespace DataGap.CmsKit.UrlShorting
{
    public interface IShortenedUrlRepository: IBasicRepository<ShortenedUrl, Guid>
    {
        Task<List<ShortenedUrl>> GetListAsync(
            string filter = null,
            string Sorting = null,
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string filter = null,
            CancellationToken cancellationToken = default);

        Task<ShortenedUrl> FindBySourceUrlAsync(
            string sourceUrl,
            CancellationToken cancellationToken = default);
    }
}
