using System;
using System.Threading.Tasks;
using DataGap.Jellog.Data;
using DataGap.Jellog.DependencyInjection;
using DataGap.Jellog.Guids;
using DataGap.CmsKit.Newsletters;
using DataGap.CmsKit.Polls;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.Pro;

public class CmsKitProDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly CmsKitProTestData _cmsKitProTestData;
    private readonly INewsletterRecordRepository _newsletterRecordRepository;
    private readonly IShortenedUrlRepository _shortenedUrlRepository;
    private readonly IPollRepository _pollRepository;

    public CmsKitProDataSeedContributor(
        IGuidGenerator guidGenerator,
        CmsKitProTestData cmsKitProTestData,
        INewsletterRecordRepository newsletterRecordRepository,
        IShortenedUrlRepository shortenedUrlRepository,
        IPollRepository pollRepository)
    {
        _guidGenerator = guidGenerator;
        _cmsKitProTestData = cmsKitProTestData;
        _newsletterRecordRepository = newsletterRecordRepository;
        _shortenedUrlRepository = shortenedUrlRepository;
        _pollRepository = pollRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedNewsletterRecordAsync();
        await SeedShortenedUrlAsync();
        await SeedPollAsync();
    }

    private async Task SeedNewsletterRecordAsync()
    {
        var newsletterRecord = new NewsletterRecord(_cmsKitProTestData.NewsletterEmailId, _cmsKitProTestData.Email);

        newsletterRecord.AddPreferences(new NewsletterPreference(_guidGenerator.Create(), newsletterRecord.Id,
            _cmsKitProTestData.Preference, _cmsKitProTestData.Source, "sourceUrl")
        );

        await _newsletterRecordRepository.InsertAsync(newsletterRecord);

        var newsletterRecord2 = new NewsletterRecord(_guidGenerator.Create(), "info@dataGapsoft.io");

        newsletterRecord2.AddPreferences(new NewsletterPreference(_guidGenerator.Create(), newsletterRecord2.Id,
            "preference2", "source2", "sourceUrl2")
        );

        await _newsletterRecordRepository.InsertAsync(newsletterRecord2);

        var newsletterRecord3 = new NewsletterRecord(_guidGenerator.Create(), "info@aspnetzero.io");

        newsletterRecord3.AddPreferences(new NewsletterPreference(_guidGenerator.Create(), newsletterRecord3.Id,
            "preference3", "source3", "sourceUrl3")
        );

        await _newsletterRecordRepository.InsertAsync(newsletterRecord3);
    }

    private async Task SeedShortenedUrlAsync()
    {
        var shortenedUrl = new ShortenedUrl(_cmsKitProTestData.ShortenedUrlId1, _cmsKitProTestData.ShortenedUrlSource1, _cmsKitProTestData.ShortenedUrlTarget1);

        await _shortenedUrlRepository.InsertAsync(shortenedUrl);

        var shortenedUrl2 = new ShortenedUrl(_cmsKitProTestData.ShortenedUrlId2, _cmsKitProTestData.ShortenedUrlSource2, _cmsKitProTestData.ShortenedUrlTarget2);

        await _shortenedUrlRepository.InsertAsync(shortenedUrl2);
    }

    private async Task SeedPollAsync()
    {
        var poll = new Poll(
            _cmsKitProTestData.PollId,
            _cmsKitProTestData.Question,
            _cmsKitProTestData.Widget,
            DateTime.UtcNow,
            endDate: DateTime.UtcNow.AddYears(1)
            );
        poll.AddPollOption(_cmsKitProTestData.PollOptionId, "yes", 0, null);
        await _pollRepository.InsertAsync(poll);
    }
}
