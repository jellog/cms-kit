using System;
using System.Threading.Tasks;
using DataGap.Jellog.Application.Services;

namespace DataGap.CmsKit.Public.Polls;
public interface IPollPublicAppService : IApplicationService
{
    Task<PollWithDetailsDto> GetPollAsync(GetPollInput input);
    Task<GetResultDto> GetResultAsync(Guid id);
    Task SubmitVoteAsync(Guid id, SubmitPollInput submitPollInput);
}
