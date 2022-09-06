using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using DataGap.Jellog;
using DataGap.Jellog.Domain.Repositories.MongoDB;
using DataGap.Jellog.MongoDB;
using DataGap.CmsKit.MongoDB;

namespace DataGap.CmsKit.Newsletters;

public class MongoNewsletterRecordRepository : MongoDbRepository<CmsKitProMongoDbContext, NewsletterRecord, Guid>,
    INewsletterRecordRepository
{
    public MongoNewsletterRecordRepository(IMongoDbContextProvider<CmsKitProMongoDbContext> dbContextProvider) :
        base(dbContextProvider)
    {
    }

    public async Task<List<NewsletterSummaryQueryResultItem>> GetListAsync(
        string preference = null,
        string source = null,
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = (await GetMongoQueryableAsync(token))
            .WhereIf(!preference.IsNullOrWhiteSpace(), q => q.Preferences.Any(x => x.Preference == preference))
            .WhereIf(!source.IsNullOrWhiteSpace(), q => q.Preferences.Any(x => x.Source.Contains(source)))
            .Select(t => new NewsletterSummaryQueryResultItem
            {
                Id = t.Id,
                EmailAddress = t.EmailAddress,
                CreationTime = t.CreationTime
            })
            .OrderByDescending(x => x.CreationTime);

        return await query.As<IMongoQueryable<NewsletterSummaryQueryResultItem>>()
            .PageBy<NewsletterSummaryQueryResultItem, IMongoQueryable<NewsletterSummaryQueryResultItem>>(skipCount, maxResultCount)
            .ToListAsync(token);
    }

    public async Task<NewsletterRecord> FindByEmailAddressAsync(
        string emailAddress,
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        Check.NotNullOrWhiteSpace(emailAddress, nameof(emailAddress));

        var token = GetCancellationToken(cancellationToken);

        return await (await GetMongoQueryableAsync(token))
            .Where(x => x.EmailAddress == emailAddress)
            .FirstOrDefaultAsync(token);
    }

    public async Task<int> GetCountByFilterAsync(
        string preference = null,
        string source = null,
        CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = (await GetMongoQueryableAsync(token))
            .WhereIf(!preference.IsNullOrWhiteSpace(), q => q.Preferences.Any(x => x.Preference == preference))
            .WhereIf(!preference.IsNullOrWhiteSpace(), q => q.Preferences.Any(x => x.Source == source));

        return await query.As<IMongoQueryable<NewsletterRecord>>().CountAsync(token);
    }
}
