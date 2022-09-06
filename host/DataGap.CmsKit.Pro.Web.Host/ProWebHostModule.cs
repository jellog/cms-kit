using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using DataGap.CmsKit.Pro.Menus;
using DataGap.CmsKit.Pro.MultiTenancy;
using DataGap.CmsKit.Pro.Web;
using StackExchange.Redis;
using DataGap.Jellog;
using DataGap.Jellog.AspNetCore.Authentication.OAuth;
using DataGap.Jellog.AspNetCore.Mvc.Client;
using DataGap.Jellog.AspNetCore.Mvc.Localization;
using DataGap.Jellog.AspNetCore.Mvc.UI.Theme.Lepton;
using DataGap.Jellog.AspNetCore.Mvc.UI.Theme.Shared;
using DataGap.Jellog.Autofac;
using DataGap.Jellog.AutoMapper;
using DataGap.Jellog.Caching;
using DataGap.Jellog.Http.Client.IdentityModel.Web;
using DataGap.Jellog.Identity;
using DataGap.Jellog.Identity.Web;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.MultiTenancy;
using DataGap.Jellog.PermissionManagement;
using DataGap.Jellog.Security.Claims;
using DataGap.Jellog.UI.Navigation.Urls;
using DataGap.Jellog.VirtualFileSystem;
using DataGap.Saas.Host;
using DataGap.Jellog.Account.Admin.Web;
using DataGap.Jellog.Caching.StackExchangeRedis;
using DataGap.Jellog.UI.Navigation;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit.Pro;

[DependsOn(
    typeof(CmsKitProWebModule),
    typeof(CmsKitProHttpApiClientModule),
    typeof(JellogAspNetCoreAuthenticationOAuthModule),
    typeof(JellogAspNetCoreMvcClientModule),
    typeof(JellogAspNetCoreMvcUiLeptonThemeModule),
    typeof(JellogAutofacModule),
    typeof(JellogCachingStackExchangeRedisModule),
    typeof(JellogHttpClientIdentityModelWebModule),
    typeof(JellogIdentityWebModule),
    typeof(JellogIdentityHttpApiClientModule),
    typeof(SaasHostWebModule),
    typeof(SaasHostHttpApiClientModule),
    typeof(JellogPermissionManagementHttpApiClientModule),
    typeof(JellogAccountAdminWebModule)
    )]
public class ProWebHostModule : JellogModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        FeatureConfigurer.Configure();

        context.Services.PreConfigure<JellogMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(CmsKitResource),
                typeof(CmsKitProDomainSharedModule).Assembly,
                typeof(CmsKitProApplicationContractsModule).Assembly,
                typeof(ProWebHostModule).Assembly
            );
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        ConfigureCache(configuration);
        ConfigureUrls(configuration);
        ConfigureAuthentication(context, configuration);
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureSwaggerServices(context.Services);
        ConfigureMultiTenancy();
        ConfigureRedis(context, configuration, hostingEnvironment);
        ConfigureMenus(configuration);
    }

    private void ConfigureMenus(IConfiguration configuration)
    {
        Configure<JellogNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new ProWebHostMenuContributor(configuration));
        });
    }

    private void ConfigureCache(IConfiguration configuration)
    {
        Configure<JellogDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "Pro:";
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureMultiTenancy()
    {
        Configure<JellogMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies", options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(365);
            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = false;
                options.ResponseType = OpenIdConnectResponseType.CodeIdToken;

                options.ClientId = configuration["AuthServer:ClientId"];
                options.ClientSecret = configuration["AuthServer:ClientSecret"];

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;

                options.Scope.Add("role");
                options.Scope.Add("email");
                options.Scope.Add("phone");
                options.Scope.Add("CmsKit");

                options.ClaimActions.MapJsonKey(JellogClaimTypes.UserName, "name");
                options.ClaimActions.DeleteClaim("name");
            });
    }

    private void ConfigureAutoMapper()
    {
        Configure<JellogAutoMapperOptions>(options =>
        {
            options.AddMaps<ProWebHostModule>();
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<JellogVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Web", Path.DirectorySeparatorChar)));
            });
        }
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Pro API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    private void ConfigureRedis(
        ServiceConfigurationContext context,
        IConfiguration configuration,
        IWebHostEnvironment hostingEnvironment)
    {
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            context.Services
                .AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "Pro-Protection-Keys");
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseErrorPage();
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseJellogRequestLocalization();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pro API");
        });

        app.UseAuditing();

        app.UseConfiguredEndpoints();
    }
}
