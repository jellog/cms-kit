using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.Application.Services;
using DataGap.Jellog.Content;

namespace DataGap.CmsKit.Admin.Newsletters;

public interface INewsletterRecordAdminAppService : IApplicationService
{
    Task<PagedResultDto<NewsletterRecordDto>> GetListAsync(GetNewsletterRecordsRequestInput input);

    Task<NewsletterRecordWithDetailsDto> GetAsync(Guid id);

    Task<List<NewsletterRecordCsvDto>> GetNewsletterRecordsCsvDetailAsync(GetNewsletterRecordsCsvRequestInput input);

    Task<List<string>> GetNewsletterPreferencesAsync();

    Task<IRemoteStreamContent> GetCsvResponsesAsync(GetNewsletterRecordsCsvRequestInput input);
}
