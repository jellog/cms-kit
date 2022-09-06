using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using DataGap.Jellog.Domain.Repositories.MongoDB;
using DataGap.Jellog.MongoDB;
using DataGap.CmsKit.MongoDB;

namespace DataGap.CmsKit.Polls;
public class MongoPollRepository : MongoDbRepository<ICmsKitProMongoDbContext, Poll, Guid>, IPollRepository
{
    public MongoPollRepository(IMongoDbContextProvider<ICmsKitProMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Poll> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await (await GetMongoQueryableAsync(cancellationToken)).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
    {
        return await (await GetMongoQueryableAsync(cancellationToken))
            .WhereIf(!filter.IsNullOrWhiteSpace(), t => t.Question.Contains(filter))
            .As<IMongoQueryable<Poll>>()
            .LongCountAsync(cancellationToken: GetCancellationToken(cancellationToken));
    }

    public async Task<List<Poll>> GetListAsync(
            string filter = null,
            string sorting = null,
            int skipCount = 0,
            int maxResultCount = Int32.MaxValue,
            CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = (await GetMongoQueryableAsync(token))
            .WhereIf(!filter.IsNullOrWhiteSpace(), t => t.Question.Contains(filter));

        query = query.OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(Poll.CreationTime)} desc" : sorting);

        return await query.As<IMongoQueryable<Poll>>()
            .PageBy<Poll, IMongoQueryable<Poll>>(skipCount, maxResultCount)
            .ToListAsync(token);
    }

    public async Task<List<Poll>> GetListByWidgetAsync(string widget, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(widget))
        {
            return new List<Poll>();
        }

        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(p => p.Widget == widget).
            ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<Guid?> GetIdByWidgetAsync(string widget, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(widget))
        {
            return null;
        }

        return (await (await GetMongoQueryableAsync(cancellationToken))
            .FirstOrDefaultAsync(p => p.Widget == widget, GetCancellationToken(cancellationToken)))?.Id;
    }

    public async Task<Dictionary<Poll, List<PollUserVote>>> GetPollWithPollUserVotesAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();
        var poll = (await GetMongoQueryableAsync()).Where(x => x.Id == id);

        var query = from p in poll
                    join puv in dbContext.PollUserVotes.AsQueryable() on p.Id equals puv.PollId into iL2
                    select new {
                        poll = p,
                        pollUserVotes = iL2
                    };

        var result = await AsyncExecuter.ToListAsync(query);

        var map = new Dictionary<Poll, List<PollUserVote>>();

        foreach (var row in result)
        {
            if (!map.ContainsKey(row.poll))
            {
                map.Add(row.poll, new List<PollUserVote>());
            }

            if (row.pollUserVotes != null)
            {
                map[row.poll] = row.pollUserVotes.ToList();
            }
        }

        return map;
    }
}
