﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DataGap.CmsKit.Pro;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<ProIdentityServerModule>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.InitializeApplication();
    }
}