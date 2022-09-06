using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DataGap.Jellog.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using DataGap.CmsKit.Public.UrlShorting;

namespace DataGap.CmsKit.Pro.Public.Web.Middlewares;

public class UrlSortingMiddleware : IMiddleware, ITransientDependency
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);

        if (context.Response.StatusCode == (int) HttpStatusCode.NotFound)
        {
            var _urlShortingPublicAppService = context.RequestServices.GetRequiredService<IUrlShortingPublicAppService>();
            var sourceUrl = context.Request.Path.ToString();

            var shortenedUrl = await _urlShortingPublicAppService.FindBySourceAsync(sourceUrl);

            if (shortenedUrl != null)
            {
                context.Response.Redirect(shortenedUrl.Target, true);
            }
        }
    }
}
