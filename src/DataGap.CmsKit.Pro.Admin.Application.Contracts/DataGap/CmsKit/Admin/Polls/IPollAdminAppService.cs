using System;
using System.Threading.Tasks;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.Application.Services;

namespace DataGap.CmsKit.Admin.Polls;

public interface IPollAdminAppService
    : ICrudAppService<
        PollWithDetailsDto,
        PollDto,
        Guid,
        GetPollListInput,
        CreatePollDto,
        UpdatePollDto
    >
{
    Task<ListResultDto<PollWidgetDto>> GetWidgetsAsync();
    Task<GetResultDto> GetResultAsync(Guid id);
}