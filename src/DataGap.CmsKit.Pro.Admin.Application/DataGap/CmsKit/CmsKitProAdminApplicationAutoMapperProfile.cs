using AutoMapper;
using DataGap.Jellog.AutoMapper;
using DataGap.CmsKit.Admin.Newsletters;
using DataGap.CmsKit.Admin.Polls;
using DataGap.CmsKit.Admin.Polls;
using DataGap.CmsKit.Admin.Tags;
using DataGap.CmsKit.Admin.UrlShorting;
using DataGap.CmsKit.Newsletters;
using DataGap.CmsKit.Polls;
using DataGap.CmsKit.Tags;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit;

public class CmsKitProAdminApplicationAutoMapperProfile : Profile
{
    public CmsKitProAdminApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<NewsletterRecord, NewsletterRecordWithDetailsDto>()
            .ForMember(s => s.Preferences, c => c.MapFrom(m => m.Preferences));

        CreateMap<NewsletterPreference, NewsletterPreferenceDto>();

        CreateMap<NewsletterSummaryQueryResultItem, NewsletterRecordDto>();

        CreateMap<NewsletterSummaryQueryResultItem, NewsletterRecordCsvDto>()
            .Ignore(x => x.SecurityCode);

        CreateMap<TagDto, TagCreateDto>();

        CreateMap<TagDto, TagUpdateDto>();

        CreateMap<TagCreateDto, Tag>(MemberList.Source)
            .Ignore(x => x.Id);

        CreateMap<TagUpdateDto, Tag>(MemberList.Source);

        CreateMap<ShortenedUrl, ShortenedUrlDto>();

        CreateMap<Poll, PollDto>();
        CreateMap<Poll, PollWithDetailsDto>();

        CreateMap<PollOption, PollOptionDto>();
    }
}
