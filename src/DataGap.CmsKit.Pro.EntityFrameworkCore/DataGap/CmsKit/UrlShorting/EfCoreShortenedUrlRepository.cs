using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataGap.Jellog.Domain.Repositories.EntityFrameworkCore;
using DataGap.CmsKit.EntityFrameworkCore;
using DataGap.Jellog.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace DataGap.CmsKit.UrlShorting
{
    public class EfCoreShortenedUrlRepository : EfCoreRepository<CmsKitProDbContext, ShortenedUrl, Guid>, IShortenedUrlRepository
    {
        public EfCoreShortenedUrlRepository(IDbContextProvider<CmsKitProDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<ShortenedUrl>> GetListAsync(
            string filter = null,
            string sorting = null,
            int skipCount = 0,
            int maxResultCount = 2147483647,
            CancellationToken cancellationToken = default)
        {
            var query = (await GetDbSetAsync())
                .WhereIf(!filter.IsNullOrWhiteSpace(), t => t.Source.Contains(filter) || t.Target.Contains(filter));

            query = query.OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(ShortenedUrl.CreationTime)} desc" : sorting);

            return await query.PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!filter.IsNullOrWhiteSpace(), t => t.Source.Contains(filter) || t.Target.Contains(filter))
                .LongCountAsync(cancellationToken: GetCancellationToken(cancellationToken));
        }

        public async Task<ShortenedUrl> FindBySourceUrlAsync(
            string sourceUrl,
            CancellationToken cancellationToken = default)
        {
            return await base.FindAsync(x => x.Source == sourceUrl, cancellationToken: cancellationToken);
        }
    }
}
