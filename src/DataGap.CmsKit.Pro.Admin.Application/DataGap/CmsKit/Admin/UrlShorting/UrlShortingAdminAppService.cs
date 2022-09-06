using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DataGap.Jellog;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.Application.Services;
using DataGap.Jellog.GlobalFeatures;
using DataGap.CmsKit.Permissions;
using DataGap.CmsKit.GlobalFeatures;
using DataGap.CmsKit.UrlShorting;

namespace DataGap.CmsKit.Admin.UrlShorting
{
    [RequiresGlobalFeature(UrlShortingFeature.Name)]
    [Authorize(CmsKitProAdminPermissions.UrlShorting.Default)]
    public class UrlShortingAdminAppService : CmsKitProAdminAppService, IUrlShortingAdminAppService
    {
        private readonly IShortenedUrlRepository _shortenedUrlRepository;

        public UrlShortingAdminAppService(IShortenedUrlRepository shortenedUrlRepository)
        {
            _shortenedUrlRepository = shortenedUrlRepository;
        }

        public async Task<PagedResultDto<ShortenedUrlDto>> GetListAsync(GetShortenedUrlListInput input)
        {
            var list = await _shortenedUrlRepository.GetListAsync(input.ShortenedUrlFilter, input.Sorting, input.SkipCount, input.MaxResultCount);

            return new PagedResultDto<ShortenedUrlDto>() {
                Items = new List<ShortenedUrlDto>(
                    ObjectMapper.Map<List<ShortenedUrl>, List<ShortenedUrlDto>>(list)
                ),
                TotalCount = await _shortenedUrlRepository.GetCountAsync(input.ShortenedUrlFilter)
            };
        }

        public async Task<ShortenedUrlDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ShortenedUrl, ShortenedUrlDto>(
                    await _shortenedUrlRepository.GetAsync(id)
                );
        }

        [Authorize(CmsKitProAdminPermissions.UrlShorting.Create)]
        public async Task<ShortenedUrlDto> CreateAsync(CreateShortenedUrlDto input)
        {
            input.Source = input.Source.EnsureStartsWith('/');

            await CheckDuplicateShortUrl(input.Source);

            var shortenedUrl = await _shortenedUrlRepository.InsertAsync(
                new ShortenedUrl(
                    GuidGenerator.Create(),
                    input.Source,
                    input.Target,
                    CurrentTenant?.Id)
            );

            return ObjectMapper.Map<ShortenedUrl, ShortenedUrlDto>(shortenedUrl);
        }

        [Authorize(CmsKitProAdminPermissions.UrlShorting.Update)]
        public async Task<ShortenedUrlDto> UpdateAsync(Guid id, UpdateShortenedUrlDto input)
        {
            var shortenedUrl = await _shortenedUrlRepository.GetAsync(id);

            shortenedUrl.SetTarget(input.Target);

            shortenedUrl = await _shortenedUrlRepository.UpdateAsync(shortenedUrl);

            return ObjectMapper.Map<ShortenedUrl, ShortenedUrlDto>(shortenedUrl);
        }

        [Authorize(CmsKitProAdminPermissions.UrlShorting.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _shortenedUrlRepository.DeleteAsync(id);
        }

        private async Task CheckDuplicateShortUrl(string source, Guid? id = null)
        {
            var existingUrl = await _shortenedUrlRepository.FindBySourceUrlAsync(source);
            if (existingUrl != null && (!id.HasValue || existingUrl.Id != id))
            {
                throw new ShortenedUrlAlreadyExistsException(source);
            }
        }
    }
}
