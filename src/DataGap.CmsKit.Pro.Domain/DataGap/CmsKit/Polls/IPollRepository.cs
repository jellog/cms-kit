using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataGap.Jellog.Domain.Repositories;

namespace DataGap.CmsKit.Polls;
public interface IPollRepository : IBasicRepository<Poll, Guid>
{
    Task<List<Poll>> GetListAsync(
        string filter = null,
        string sorting = null,
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        CancellationToken cancellationToken = default);
    
    Task<List<Poll>> GetListByWidgetAsync(
        string widget,
        CancellationToken cancellationToken = default);

    Task<Guid?> GetIdByWidgetAsync(
        string widget,
        CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(
        string filter = null,
        CancellationToken cancellationToken = default);

    Task<IQueryable<Poll>> WithDetailsAsync();

    Task<Dictionary<Poll, List<PollUserVote>>> GetPollWithPollUserVotesAsync(Guid id);
}
