// This file is automatically generated by JELLOG framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using DataGap.Jellog.Application.Dtos;
using DataGap.Jellog.Http.Client;
using DataGap.Jellog.Http.Modeling;
using DataGap.Jellog.DependencyInjection;
using DataGap.Jellog.Http.Client.ClientProxying;
using DataGap.CmsKit.Public.Polls;

// ReSharper disable once CheckNamespace
namespace DataGap.CmsKit.Public.Polls.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IPollPublicAppService), typeof(PollPublicClientProxy))]
public partial class PollPublicClientProxy : ClientProxyBase<IPollPublicAppService>, IPollPublicAppService
{
    public virtual async Task<PollWithDetailsDto> GetPollAsync(GetPollInput input)
    {
        return await RequestAsync<PollWithDetailsDto>(nameof(GetPollAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetPollInput), input }
        });
    }

    public virtual async Task<GetResultDto> GetResultAsync(Guid id)
    {
        return await RequestAsync<GetResultDto>(nameof(GetResultAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task SubmitVoteAsync(Guid id, SubmitPollInput submitPollInput)
    {
        await RequestAsync(nameof(SubmitVoteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(SubmitPollInput), submitPollInput }
        });
    }
}
