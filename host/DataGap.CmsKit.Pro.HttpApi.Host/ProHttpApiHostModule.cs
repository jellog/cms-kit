using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataGap.CmsKit.Pro.EntityFrameworkCore;
using DataGap.CmsKit.Pro.MultiTenancy;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using DataGap.Jellog;
using DataGap.Jellog.AspNetCore.Mvc.UI.MultiTenancy;
using DataGap.Jellog.AuditLogging.EntityFrameworkCore;
using DataGap.Jellog.Autofac;
using DataGap.Jellog.Caching;
using DataGap.Jellog.Caching.StackExchangeRedis;
using DataGap.Jellog.Data;
using DataGap.Jellog.EntityFrameworkCore;
using DataGap.Jellog.EntityFrameworkCore.SqlServer;
using DataGap.Jellog.Localization;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.MultiTenancy;
using DataGap.Jellog.PermissionManagement.EntityFrameworkCore;
using DataGap.Jellog.Security.Claims;
using DataGap.Jellog.SettingManagement.EntityFrameworkCore;
using DataGap.Jellog.Threading;
using DataGap.Jellog.VirtualFileSystem;
using DataGap.CmsKit.EntityFrameworkCore;

namespace DataGap.CmsKit.Pro;

[DependsOn(
    typeof(CmsKitProApplicationModule),
    typeof(CmsKitProEntityFrameworkCoreModule),
    typeof(CmsKitProHttpApiModule),
    typeof(JellogAspNetCoreMvcUiMultiTenancyModule),
    typeof(JellogAutofacModule),
    typeof(JellogCachingStackExchangeRedisModule),
    typeof(JellogEntityFrameworkCoreSqlServerModule),
    typeof(JellogAuditLoggingEntityFrameworkCoreModule),
    typeof(JellogPermissionManagementEntityFrameworkCoreModule),
    typeof(JellogSettingManagementEntityFrameworkCoreModule)
    )]
public class ProHttpApiHostModule : JellogModule
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

        Configure<JellogMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<JellogVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitProApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}DataGap.CmsKit.Pro.Application", Path.DirectorySeparatorChar)));
            });
        }

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
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish", "fi"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français", "fr"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
            options.Languages.Add(new LanguageInfo("sl", "sl", "Slovenščina"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak", "sk"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
        });

        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = false;
                options.Audience = "CmsKit";
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
