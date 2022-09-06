using AutoMapper;
using DataGap.CmsKit.Admin.Polls;
using DataGap.CmsKit.Admin.UrlShorting;
using DataGap.CmsKit.Pro.Admin.Web.Pages.CmsKit.UrlShorting;
using static DataGap.CmsKit.Pro.Admin.Web.Pages.CmsKit.Polls.EditModalModel;

namespace DataGap.CmsKit.Pro.Admin.Web;

public class CmsKitProAdminWebAutoMapperProfile : Profile
{
    public CmsKitProAdminWebAutoMapperProfile()
    {
        CreateMap<ShortenedUrlDto, EditModalModel.ShortenedUrlEditViewModel>();
        CreateMap<EditModalModel.ShortenedUrlEditViewModel, UpdateShortenedUrlDto>();
        CreateMap<CreateModalModel.CreateShortenedUrlViewModel, CreateShortenedUrlDto>();

        CreateMap<PollWithDetailsDto, PollEditViewModel>();
        CreateMap<PollEditViewModel, UpdatePollDto>();

    }
}
