// This file is automatically generated by JELLOG framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.Http.Client;
using DataGap.Jellog.Http.Modeling;
using DataGap.Jellog.DependencyInjection;
using DataGap.Jellog.Http.Client.ClientProxying;
using DataGap.CmsKit.Public.Newsletters;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DataGap.CmsKit.Public.Newsletters.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(INewsletterRecordPublicAppService), typeof(NewsletterRecordPublicClientProxy))]
public partial class NewsletterRecordPublicClientProxy : ClientProxyBase<INewsletterRecordPublicAppService>, INewsletterRecordPublicAppService
{
    public virtual async Task CreateAsync(CreateNewsletterRecordInput input)
    {
        await RequestAsync(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CreateNewsletterRecordInput), input }
        });
    }

    public virtual async Task<List<NewsletterPreferenceDetailsDto>> GetNewsletterPreferencesAsync(string emailAddress)
    {
        return await RequestAsync<List<NewsletterPreferenceDetailsDto>>(nameof(GetNewsletterPreferencesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), emailAddress }
        });
    }

    public virtual async Task UpdatePreferencesAsync(UpdatePreferenceRequestInput input)
    {
        await RequestAsync(nameof(UpdatePreferencesAsync), new ClientProxyRequestTypeValue
        {
            { typeof(UpdatePreferenceRequestInput), input }
        });
    }

    public virtual async Task<NewsletterEmailOptionsDto> GetOptionByPreferenceAsync(string preference)
    {
        return await RequestAsync<NewsletterEmailOptionsDto>(nameof(GetOptionByPreferenceAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), preference }
        });
    }
}
