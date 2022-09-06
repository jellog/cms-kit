using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using DataGap.Jellog.Domain.Repositories.EntityFrameworkCore;
using DataGap.Jellog.EntityFrameworkCore;
using DataGap.CmsKit.EntityFrameworkCore;

namespace DataGap.CmsKit.Polls;
public class EfCorePollRepository : EfCoreRepository<CmsKitProDbContext, Poll, Guid>, IPollRepository
{
    public EfCorePollRepository(IDbContextProvider<CmsKitProDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Poll>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .IncludeDetails();
    }

    public async Task<Dictionary<Poll, List<PollUserVote>>> GetPollWithPollUserVotesAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();

        var query = from p in await WithDetailsAsync()
                    join puv in dbContext.PollUserVotes on p.Id equals puv.PollId into iL2
                    from iL3 in iL2.DefaultIfEmpty()
                    where p.Id == id
                    select new {
                        poll = p,
                        pollUserVotes = iL3
                    };

        var result = await query.ToListAsync();

        var map = new Dictionary<Poll, List<PollUserVote>>();

        foreach (var row in result)
        {
            if (!map.ContainsKey(row.poll))
            {
                map.Add(row.poll, new List<PollUserVote>());
            }

            if (row.pollUserVotes != null)
            {
                map[row.poll].Add(row.pollUserVotes);
            }
        }

        return map;
    }

    public async Task<List<Poll>> GetListByWidgetAsync(string widget, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(widget))
        {
            return new List<Poll>();
        }

        return await (await GetDbSetAsync())
            .Where(p => p.Widget == widget).
            ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<Guid?> GetIdByWidgetAsync(string widget, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(widget))
        {
            return null;
        }

        return (await (await GetDbSetAsync())
            .FirstOrDefaultAsync(p => p.Widget == widget, GetCancellationToken(cancellationToken)))?.Id;
    }

    public async Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
                .WhereIf(!filter.IsNullOrWhiteSpace(), t => t.Question.Contains(filter))
                .LongCountAsync(cancellationToken: GetCancellationToken(cancellationToken));
    }

    public async Task<List<Poll>> GetListAsync(string filter = null, string sorting = null, int skipCount = 0, int maxResultCount = int.MaxValue, CancellationToken cancellationToken = default)
    {
        var query = (await GetDbSetAsync())
                .WhereIf(!filter.IsNullOrWhiteSpace(), t => t.Question.Contains(filter));

        query = query.OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(Poll.CreationTime)} desc" : sorting);

        return await query.PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}
