using AutoMapper;
using DataGap.Jellog.AutoMapper;
using DataGap.CmsKit.Newsletters;
using DataGap.CmsKit.Polls;
using DataGap.CmsKit.Public.Newsletters;
using DataGap.CmsKit.Public.Polls;
using DataGap.CmsKit.Public.UrlShorting;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit;

public class CmsKitProPublicApplicationAutoMapperProfile : Profile
{
    public CmsKitProPublicApplicationAutoMapperProfile()
    {
        CreateMap<NewsletterPreferenceDefinition, NewsletterPreferenceDetailsDto>()
            .Ignore(x => x.DisplayPreference)
            .Ignore(x => x.Definition)
            .Ignore(x => x.IsSelectedByEmailAddress);

        CreateMap<NewsletterPreferenceDefinition, NewsletterEmailOptionsDto>()
            .Ignore(x => x.PrivacyPolicyConfirmation)
            .Ignore(x => x.DisplayAdditionalPreferences)
            .Ignore(x => x.AdditionalPreferences);

        CreateMap<ShortenedUrl, ShortenedUrlDto>();
        CreateMap<ShortenedUrlCacheItem, ShortenedUrlDto>();

        CreateMap<Poll, PollWithDetailsDto>();
        CreateMap<PollOption, PollOptionDto>();
    }
}
