using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.Content;
using DataGap.CmsKit.Newsletters;
using DataGap.CmsKit.Newsletters.Helpers;
using DataGap.CmsKit.Permissions;

namespace DataGap.CmsKit.Admin.Newsletters;

[Authorize(CmsKitProAdminPermissions.Newsletters.Default)]
public class NewsletterRecordAdminAppService : CmsKitProAdminAppService, INewsletterRecordAdminAppService
{
    protected INewsletterRecordRepository NewsletterRecordsRepository { get; }

    protected NewsletterRecordManager NewsletterRecordManager { get; }

    protected SecurityCodeProvider SecurityCodeProvider { get; }

    public NewsletterRecordAdminAppService(
        INewsletterRecordRepository newsletterRecordsRepository,
        NewsletterRecordManager newsletterRecordManager,
        SecurityCodeProvider securityCodeProvider)
    {
        NewsletterRecordsRepository = newsletterRecordsRepository;
        NewsletterRecordManager = newsletterRecordManager;
        SecurityCodeProvider = securityCodeProvider;
    }

    public async Task<PagedResultDto<NewsletterRecordDto>> GetListAsync(GetNewsletterRecordsRequestInput input)
    {
        var count = await NewsletterRecordsRepository.GetCountByFilterAsync(input.Preference, input.Source);

        var newsletterSummaries = await NewsletterRecordsRepository.GetListAsync(
            input.Preference,
            input.Source,
            input.SkipCount,
            input.MaxResultCount
        );

        return new PagedResultDto<NewsletterRecordDto>(
            count,
            ObjectMapper.Map<List<NewsletterSummaryQueryResultItem>, List<NewsletterRecordDto>>(newsletterSummaries)
        );
    }

    public async Task<NewsletterRecordWithDetailsDto> GetAsync(Guid id)
    {
        var newsletterRecord = await NewsletterRecordsRepository.GetAsync(id);

        return ObjectMapper.Map<NewsletterRecord, NewsletterRecordWithDetailsDto>(newsletterRecord);
    }

    public async Task<List<NewsletterRecordCsvDto>> GetNewsletterRecordsCsvDetailAsync(GetNewsletterRecordsCsvRequestInput input)
    {
        var newsletters = await NewsletterRecordsRepository.GetListAsync(input.Preference, input.Source, 0, int.MaxValue);

        var newsletterRecordsCsvDto = ObjectMapper.Map<List<NewsletterSummaryQueryResultItem>, List<NewsletterRecordCsvDto>>(newsletters);

        foreach (var newsletterRecord in newsletterRecordsCsvDto)
        {
            newsletterRecord.SecurityCode = SecurityCodeProvider.GetSecurityCode(newsletterRecord.EmailAddress);
        }

        return newsletterRecordsCsvDto;
    }

    public async Task<List<string>> GetNewsletterPreferencesAsync()
    {
        var newsletterPreferences = await NewsletterRecordManager.GetNewsletterPreferencesAsync();

        return newsletterPreferences.Select(newsletterPreference => newsletterPreference.Preference).ToList();
    }

    public async Task<IRemoteStreamContent> GetCsvResponsesAsync(GetNewsletterRecordsCsvRequestInput input)
    {
        var newsletters = await GetNewsletterRecordsCsvDetailAsync(input);
        
        input.Preference = input.Preference?.Insert(0, "-");
        input.Source = input.Source?.Insert(0, "-");

        var csvConfiguration = new CsvConfiguration(new CultureInfo(CultureInfo.CurrentUICulture.Name));
        using (var memoryStream = new MemoryStream())
        {
            using (var streamWriter = new StreamWriter(stream: memoryStream, encoding: new UTF8Encoding(true)))
            {
                using (var csvWriter = new CsvWriter(streamWriter, csvConfiguration))
                {
                    await csvWriter.WriteRecordsAsync(newsletters);

                    await streamWriter.FlushAsync();
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    var ms = new MemoryStream();
                    await memoryStream.CopyToAsync(ms);
                    ms.Seek(0, SeekOrigin.Begin);

                    return new RemoteStreamContent(
                        ms,
                        fileName: $"newsletter-emails-{DateTime.Now.ToString("yyyyMMddHHmmss")}{input.Preference}{input.Source}.csv",
                        contentType: "text/csv");
                }
            }
        }
    }
}
