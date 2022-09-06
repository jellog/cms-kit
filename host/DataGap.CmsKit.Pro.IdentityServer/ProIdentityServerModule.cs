using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataGap.CmsKit.Pro.MultiTenancy;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using DataGap.Jellog;
using DataGap.Jellog.Account;
using DataGap.Jellog.Account.Web;
using DataGap.Jellog.AspNetCore.Authentication.JwtBearer;
using DataGap.Jellog.AspNetCore.Mvc;
using DataGap.Jellog.AspNetCore.Mvc.UI.MultiTenancy;
using DataGap.Jellog.AspNetCore.Mvc.UI.Theme.Lepton;
using DataGap.Jellog.Auditing;
using DataGap.Jellog.AuditLogging.EntityFrameworkCore;
using DataGap.Jellog.Autofac;
using DataGap.Jellog.BlobStoring.Database.EntityFrameworkCore;
using DataGap.Jellog.Caching;
using DataGap.Jellog.Caching.StackExchangeRedis;
using DataGap.Jellog.Data;
using DataGap.Jellog.EntityFrameworkCore;
using DataGap.Jellog.EntityFrameworkCore.SqlServer;
using DataGap.Jellog.FeatureManagement;
using DataGap.Jellog.FeatureManagement.EntityFrameworkCore;
using DataGap.Jellog.Identity;
using DataGap.Jellog.Identity.EntityFrameworkCore;
using DataGap.Jellog.IdentityServer.EntityFrameworkCore;
using DataGap.Jellog.Localization;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.MultiTenancy;
using DataGap.Jellog.PermissionManagement;
using DataGap.Jellog.PermissionManagement.EntityFrameworkCore;
using DataGap.Jellog.PermissionManagement.HttpApi;
using DataGap.Jellog.PermissionManagement.Identity;
using DataGap.Jellog.SettingManagement.EntityFrameworkCore;
using DataGap.Jellog.Threading;
using DataGap.Jellog.UI.Navigation.Urls;
using DataGap.Saas.EntityFrameworkCore;
using DataGap.Saas.Host;

namespace DataGap.CmsKit.Pro;

[DependsOn(
    typeof(JellogAccountPublicWebIdentityServerModule),
    typeof(JellogAccountPublicApplicationModule),
    typeof(JellogAccountPublicHttpApiModule),
    typeof(JellogAspNetCoreMvcUiMultiTenancyModule),
    typeof(JellogAspNetCoreMvcModule),
    typeof(JellogAspNetCoreMvcUiLeptonThemeModule),
    typeof(JellogAuditLoggingEntityFrameworkCoreModule),
    typeof(JellogAutofacModule),
    typeof(JellogCachingStackExchangeRedisModule),
    typeof(JellogEntityFrameworkCoreSqlServerModule),
    typeof(JellogIdentityProEntityFrameworkCoreModule),
    typeof(JellogIdentityApplicationModule),
    typeof(JellogIdentityHttpApiModule),
    typeof(JellogIdentityServerEntityFrameworkCoreModule),
    typeof(JellogPermissionManagementDomainIdentityModule),
    typeof(JellogPermissionManagementEntityFrameworkCoreModule),
    typeof(JellogPermissionManagementApplicationModule),
    typeof(JellogPermissionManagementHttpApiModule),
    typeof(JellogSettingManagementEntityFrameworkCoreModule),
    typeof(SaasEntityFrameworkCoreModule),
    typeof(SaasHostApplicationModule),
    typeof(SaasHostHttpApiModule),
    typeof(JellogAspNetCoreAuthenticationJwtBearerModule),
    typeof(CmsKitProApplicationContractsModule),
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule),
    typeof(JellogFeatureManagementApplicationModule),
    typeof(JellogFeatureManagementEntityFrameworkCoreModule)
    )]
public class ProIdentityServerModule : JellogModule
{
    private const string DefaultCorsPolicyName = "Default";

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        FeatureConfigurer.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        Configure<JellogDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });

        context.Services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Pro API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

        Configure<JellogLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية", "ae"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština", flagIcon: "cz"));
            options.Languages.Add(new LanguageInfo("en", "en", "English", flagIcon: "gb"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish", "fi"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français", "fr"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português (Brasil)", flagIcon: "br"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak", flagIcon: "sk"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe", flagIcon: "tr"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "Chinese", flagIcon: "cn"));
        });

        Configure<JellogAuditingOptions>(options =>
        {
                //options.IsEnabledForGetRequests = true;
                options.ApplicationName = "AuthServer";
        });

        Configure<JellogMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });

        context.Services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = false;
                options.Audience = configuration["AuthServer:ApiName"];
            });

        Configure<JellogDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "Pro:";
        });

        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            context.Services
                .AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "Pro-Protection-Keys");
        }

        context.Services.AddCors(options =>
        {
            options.AddPolicy(DefaultCorsPolicyName, builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithJellogExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        if (!context.GetEnvironment().IsDevelopment())
        {
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors(DefaultCorsPolicyName);
        app.UseAuthentication();
        app.UseJwtTokenMiddleware();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseJellogRequestLocalization();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pro API");
        });
        app.UseAuditing();
        app.UseConfiguredEndpoints();

        SeedData(context);
    }

    private void SeedData(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(async () =>
        {
            using (var scope = context.ServiceProvider.CreateScope())
            {
                await scope.ServiceProvider
                    .GetRequiredService<IDataSeeder>()
                    .SeedAsync();
            }
        });
    }
}
