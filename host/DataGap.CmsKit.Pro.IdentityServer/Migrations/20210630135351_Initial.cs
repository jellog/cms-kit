using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataGap.CmsKit.Pro.Migrations;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "JellogAuditLogs",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ApplicationName = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                TenantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ImpersonatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ImpersonatorTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                ExecutionDuration = table.Column<int>(type: "int", nullable: false),
                ClientIpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                ClientName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                ClientId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                CorrelationId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                BrowserInfo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                HttpMethod = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                Url = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Exceptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Comments = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                HttpStatusCode = table.Column<int>(type: "int", nullable: true),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogAuditLogs", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "JellogBlobContainers",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogBlobContainers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "JellogClaimTypes",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                Required = table.Column<bool>(type: "bit", nullable: false),
                IsStatic = table.Column<bool>(type: "bit", nullable: false),
                Regex = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                RegexDescription = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                ValueType = table.Column<int>(type: "int", nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogClaimTypes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "JellogFeatureValues",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Value = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                ProviderName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                ProviderKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogFeatureValues", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "JellogLinkUsers",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SourceUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SourceTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                TargetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TargetTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogLinkUsers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "JellogOrganizationUnits",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Code = table.Column<string>(type: "nvarchar(95)", maxLength: 95, nullable: false),
                DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogOrganizationUnits", x => x.Id);
                table.ForeignKey(
                    name: "FK_JellogOrganizationUnits_JellogOrganizationUnits_ParentId",
                    column: x => x.ParentId,
                    principalTable: "JellogOrganizationUnits",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "JellogPermissionGrants",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                ProviderName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                ProviderKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogPermissionGrants", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "JellogRoles",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                IsDefault = table.Column<bool>(type: "bit", nullable: false),
                IsStatic = table.Column<bool>(type: "bit", nullable: false),
                IsPublic = table.Column<bool>(type: "bit", nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogRoles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "JellogSecurityLogs",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ApplicationName = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                Identity = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                Action = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                TenantName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                ClientId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                CorrelationId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                ClientIpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                BrowserInfo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogSecurityLogs", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "JellogSettings",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Value = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                ProviderName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                ProviderKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogSettings", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "JellogUsers",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                Surname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                SecurityStamp = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                IsExternal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogUsers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerApiResources",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                Enabled = table.Column<bool>(type: "bit", nullable: false),
                AllowedAccessTokenSigningAlgorithms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ShowInDiscoveryDocument = table.Column<bool>(type: "bit", nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerApiResources", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerApiScopes",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Enabled = table.Column<bool>(type: "bit", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                Required = table.Column<bool>(type: "bit", nullable: false),
                Emphasize = table.Column<bool>(type: "bit", nullable: false),
                ShowInDiscoveryDocument = table.Column<bool>(type: "bit", nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerApiScopes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerClients",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                ClientName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                ClientUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                LogoUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                Enabled = table.Column<bool>(type: "bit", nullable: false),
                ProtocolType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                RequireClientSecret = table.Column<bool>(type: "bit", nullable: false),
                RequireConsent = table.Column<bool>(type: "bit", nullable: false),
                AllowRememberConsent = table.Column<bool>(type: "bit", nullable: false),
                AlwaysIncludeUserClaimsInIdToken = table.Column<bool>(type: "bit", nullable: false),
                RequirePkce = table.Column<bool>(type: "bit", nullable: false),
                AllowPlainTextPkce = table.Column<bool>(type: "bit", nullable: false),
                RequireRequestObject = table.Column<bool>(type: "bit", nullable: false),
                AllowAccessTokensViaBrowser = table.Column<bool>(type: "bit", nullable: false),
                FrontChannelLogoutUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                FrontChannelLogoutSessionRequired = table.Column<bool>(type: "bit", nullable: false),
                BackChannelLogoutUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                BackChannelLogoutSessionRequired = table.Column<bool>(type: "bit", nullable: false),
                AllowOfflineAccess = table.Column<bool>(type: "bit", nullable: false),
                IdentityTokenLifetime = table.Column<int>(type: "int", nullable: false),
                AllowedIdentityTokenSigningAlgorithms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                AccessTokenLifetime = table.Column<int>(type: "int", nullable: false),
                AuthorizationCodeLifetime = table.Column<int>(type: "int", nullable: false),
                ConsentLifetime = table.Column<int>(type: "int", nullable: true),
                AbsoluteRefreshTokenLifetime = table.Column<int>(type: "int", nullable: false),
                SlidingRefreshTokenLifetime = table.Column<int>(type: "int", nullable: false),
                RefreshTokenUsage = table.Column<int>(type: "int", nullable: false),
                UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(type: "bit", nullable: false),
                RefreshTokenExpiration = table.Column<int>(type: "int", nullable: false),
                AccessTokenType = table.Column<int>(type: "int", nullable: false),
                EnableLocalLogin = table.Column<bool>(type: "bit", nullable: false),
                IncludeJwtId = table.Column<bool>(type: "bit", nullable: false),
                AlwaysSendClientClaims = table.Column<bool>(type: "bit", nullable: false),
                ClientClaimsPrefix = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                PairWiseSubjectSalt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                UserSsoLifetime = table.Column<int>(type: "int", nullable: true),
                UserCodeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                DeviceCodeLifetime = table.Column<int>(type: "int", nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerClients", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerDeviceFlowCodes",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerDeviceFlowCodes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerIdentityResources",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                Enabled = table.Column<bool>(type: "bit", nullable: false),
                Required = table.Column<bool>(type: "bit", nullable: false),
                Emphasize = table.Column<bool>(type: "bit", nullable: false),
                ShowInDiscoveryDocument = table.Column<bool>(type: "bit", nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerIdentityResources", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerPersistedGrants",
            columns: table => new {
                Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false),
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerPersistedGrants", x => x.Key);
            });

        migrationBuilder.CreateTable(
            name: "PayPaymentRequests",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                State = table.Column<int>(type: "int", nullable: false),
                Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Gateway = table.Column<string>(type: "nvarchar(max)", nullable: true),
                FailReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                EmailSendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                ExternalSubscriptionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PayPaymentRequests", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PayPlans",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PayPlans", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "SaasEditions",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SaasEditions", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "SaasTenants",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                EditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                EditionEndDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                ActivationState = table.Column<byte>(type: "tinyint", nullable: false),
                ActivationEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SaasTenants", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "JellogAuditLogActions",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                AuditLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ServiceName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                MethodName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                Parameters = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                ExecutionDuration = table.Column<int>(type: "int", nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogAuditLogActions", x => x.Id);
                table.ForeignKey(
                    name: "FK_JellogAuditLogActions_JellogAuditLogs_AuditLogId",
                    column: x => x.AuditLogId,
                    principalTable: "JellogAuditLogs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "JellogEntityChanges",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AuditLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ChangeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                ChangeType = table.Column<byte>(type: "tinyint", nullable: false),
                EntityTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                EntityId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                EntityTypeFullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogEntityChanges", x => x.Id);
                table.ForeignKey(
                    name: "FK_JellogEntityChanges_JellogAuditLogs_AuditLogId",
                    column: x => x.AuditLogId,
                    principalTable: "JellogAuditLogs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "JellogBlobs",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                Content = table.Column<byte[]>(type: "varbinary(max)", maxLength: 2147483647, nullable: true),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogBlobs", x => x.Id);
                table.ForeignKey(
                    name: "FK_JellogBlobs_JellogBlobContainers_ContainerId",
                    column: x => x.ContainerId,
                    principalTable: "JellogBlobContainers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "JellogOrganizationUnitRoles",
            columns: table => new {
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogOrganizationUnitRoles", x => new { x.OrganizationUnitId, x.RoleId });
                table.ForeignKey(
                    name: "FK_JellogOrganizationUnitRoles_JellogOrganizationUnits_OrganizationUnitId",
                    column: x => x.OrganizationUnitId,
                    principalTable: "JellogOrganizationUnits",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_JellogOrganizationUnitRoles_JellogRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "JellogRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "JellogRoleClaims",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ClaimType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                ClaimValue = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogRoleClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_JellogRoleClaims_JellogRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "JellogRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "JellogUserClaims",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ClaimType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                ClaimValue = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogUserClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_JellogUserClaims_JellogUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "JellogUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "JellogUserLogins",
            columns: table => new {
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LoginProvider = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ProviderKey = table.Column<string>(type: "nvarchar(196)", maxLength: 196, nullable: false),
                ProviderDisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogUserLogins", x => new { x.UserId, x.LoginProvider });
                table.ForeignKey(
                    name: "FK_JellogUserLogins_JellogUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "JellogUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "JellogUserOrganizationUnits",
            columns: table => new {
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                OrganizationUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogUserOrganizationUnits", x => new { x.OrganizationUnitId, x.UserId });
                table.ForeignKey(
                    name: "FK_JellogUserOrganizationUnits_JellogOrganizationUnits_OrganizationUnitId",
                    column: x => x.OrganizationUnitId,
                    principalTable: "JellogOrganizationUnits",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_JellogUserOrganizationUnits_JellogUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "JellogUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "JellogUserRoles",
            columns: table => new {
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogUserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_JellogUserRoles_JellogRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "JellogRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_JellogUserRoles_JellogUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "JellogUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "JellogUserTokens",
            columns: table => new {
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LoginProvider = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    name: "FK_JellogUserTokens_JellogUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "JellogUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerApiResourceClaims",
            columns: table => new {
                Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                ApiResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerApiResourceClaims", x => new { x.ApiResourceId, x.Type });
                table.ForeignKey(
                    name: "FK_IdentityServerApiResourceClaims_IdentityServerApiResources_ApiResourceId",
                    column: x => x.ApiResourceId,
                    principalTable: "IdentityServerApiResources",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerApiResourceProperties",
            columns: table => new {
                ApiResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerApiResourceProperties", x => new { x.ApiResourceId, x.Key, x.Value });
                table.ForeignKey(
                    name: "FK_IdentityServerApiResourceProperties_IdentityServerApiResources_ApiResourceId",
                    column: x => x.ApiResourceId,
                    principalTable: "IdentityServerApiResources",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerApiResourceScopes",
            columns: table => new {
                ApiResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Scope = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerApiResourceScopes", x => new { x.ApiResourceId, x.Scope });
                table.ForeignKey(
                    name: "FK_IdentityServerApiResourceScopes_IdentityServerApiResources_ApiResourceId",
                    column: x => x.ApiResourceId,
                    principalTable: "IdentityServerApiResources",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerApiResourceSecrets",
            columns: table => new {
                Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                Value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                ApiResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                Expiration = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerApiResourceSecrets", x => new { x.ApiResourceId, x.Type, x.Value });
                table.ForeignKey(
                    name: "FK_IdentityServerApiResourceSecrets_IdentityServerApiResources_ApiResourceId",
                    column: x => x.ApiResourceId,
                    principalTable: "IdentityServerApiResources",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerApiScopeClaims",
            columns: table => new {
                Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                ApiScopeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerApiScopeClaims", x => new { x.ApiScopeId, x.Type });
                table.ForeignKey(
                    name: "FK_IdentityServerApiScopeClaims_IdentityServerApiScopes_ApiScopeId",
                    column: x => x.ApiScopeId,
                    principalTable: "IdentityServerApiScopes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerApiScopeProperties",
            columns: table => new {
                ApiScopeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerApiScopeProperties", x => new { x.ApiScopeId, x.Key, x.Value });
                table.ForeignKey(
                    name: "FK_IdentityServerApiScopeProperties_IdentityServerApiScopes_ApiScopeId",
                    column: x => x.ApiScopeId,
                    principalTable: "IdentityServerApiScopes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerClientClaims",
            columns: table => new {
                ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerClientClaims", x => new { x.ClientId, x.Type, x.Value });
                table.ForeignKey(
                    name: "FK_IdentityServerClientClaims_IdentityServerClients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "IdentityServerClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerClientCorsOrigins",
            columns: table => new {
                ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Origin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerClientCorsOrigins", x => new { x.ClientId, x.Origin });
                table.ForeignKey(
                    name: "FK_IdentityServerClientCorsOrigins_IdentityServerClients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "IdentityServerClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerClientGrantTypes",
            columns: table => new {
                ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                GrantType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerClientGrantTypes", x => new { x.ClientId, x.GrantType });
                table.ForeignKey(
                    name: "FK_IdentityServerClientGrantTypes_IdentityServerClients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "IdentityServerClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerClientIdPRestrictions",
            columns: table => new {
                ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Provider = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerClientIdPRestrictions", x => new { x.ClientId, x.Provider });
                table.ForeignKey(
                    name: "FK_IdentityServerClientIdPRestrictions_IdentityServerClients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "IdentityServerClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerClientPostLogoutRedirectUris",
            columns: table => new {
                ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PostLogoutRedirectUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerClientPostLogoutRedirectUris", x => new { x.ClientId, x.PostLogoutRedirectUri });
                table.ForeignKey(
                    name: "FK_IdentityServerClientPostLogoutRedirectUris_IdentityServerClients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "IdentityServerClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerClientProperties",
            columns: table => new {
                ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerClientProperties", x => new { x.ClientId, x.Key, x.Value });
                table.ForeignKey(
                    name: "FK_IdentityServerClientProperties_IdentityServerClients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "IdentityServerClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerClientRedirectUris",
            columns: table => new {
                ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RedirectUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerClientRedirectUris", x => new { x.ClientId, x.RedirectUri });
                table.ForeignKey(
                    name: "FK_IdentityServerClientRedirectUris_IdentityServerClients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "IdentityServerClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerClientScopes",
            columns: table => new {
                ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Scope = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerClientScopes", x => new { x.ClientId, x.Scope });
                table.ForeignKey(
                    name: "FK_IdentityServerClientScopes_IdentityServerClients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "IdentityServerClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerClientSecrets",
            columns: table => new {
                Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                Value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                Expiration = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerClientSecrets", x => new { x.ClientId, x.Type, x.Value });
                table.ForeignKey(
                    name: "FK_IdentityServerClientSecrets_IdentityServerClients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "IdentityServerClients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerIdentityResourceClaims",
            columns: table => new {
                Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                IdentityResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerIdentityResourceClaims", x => new { x.IdentityResourceId, x.Type });
                table.ForeignKey(
                    name: "FK_IdentityServerIdentityResourceClaims_IdentityServerIdentityResources_IdentityResourceId",
                    column: x => x.IdentityResourceId,
                    principalTable: "IdentityServerIdentityResources",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "IdentityServerIdentityResourceProperties",
            columns: table => new {
                IdentityResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IdentityServerIdentityResourceProperties", x => new { x.IdentityResourceId, x.Key, x.Value });
                table.ForeignKey(
                    name: "FK_IdentityServerIdentityResourceProperties_IdentityServerIdentityResources_IdentityResourceId",
                    column: x => x.IdentityResourceId,
                    principalTable: "IdentityServerIdentityResources",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PayPaymentRequestProducts",
            columns: table => new {
                PaymentRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UnitPrice = table.Column<float>(type: "real", nullable: false),
                Count = table.Column<int>(type: "int", nullable: false),
                TotalPrice = table.Column<float>(type: "real", nullable: false),
                PaymentType = table.Column<byte>(type: "tinyint", nullable: false),
                PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PayPaymentRequestProducts", x => new { x.PaymentRequestId, x.Code });
                table.ForeignKey(
                    name: "FK_PayPaymentRequestProducts_PayPaymentRequests_PaymentRequestId",
                    column: x => x.PaymentRequestId,
                    principalTable: "PayPaymentRequests",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PayGatewayPlans",
            columns: table => new {
                PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Gateway = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PayGatewayPlans", x => new { x.PlanId, x.Gateway });
                table.ForeignKey(
                    name: "FK_PayGatewayPlans_PayPlans_PlanId",
                    column: x => x.PlanId,
                    principalTable: "PayPlans",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "SaasTenantConnectionStrings",
            columns: table => new {
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Value = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SaasTenantConnectionStrings", x => new { x.TenantId, x.Name });
                table.ForeignKey(
                    name: "FK_SaasTenantConnectionStrings_SaasTenants_TenantId",
                    column: x => x.TenantId,
                    principalTable: "SaasTenants",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "JellogEntityPropertyChanges",
            columns: table => new {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                EntityChangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                NewValue = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                OriginalValue = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                PropertyName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                PropertyTypeFullName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_JellogEntityPropertyChanges", x => x.Id);
                table.ForeignKey(
                    name: "FK_JellogEntityPropertyChanges_JellogEntityChanges_EntityChangeId",
                    column: x => x.EntityChangeId,
                    principalTable: "JellogEntityChanges",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_JellogAuditLogActions_AuditLogId",
            table: "JellogAuditLogActions",
            column: "AuditLogId");

        migrationBuilder.CreateIndex(
            name: "IX_JellogAuditLogActions_TenantId_ServiceName_MethodName_ExecutionTime",
            table: "JellogAuditLogActions",
            columns: new[] { "TenantId", "ServiceName", "MethodName", "ExecutionTime" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogAuditLogs_TenantId_ExecutionTime",
            table: "JellogAuditLogs",
            columns: new[] { "TenantId", "ExecutionTime" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogAuditLogs_TenantId_UserId_ExecutionTime",
            table: "JellogAuditLogs",
            columns: new[] { "TenantId", "UserId", "ExecutionTime" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogBlobContainers_TenantId_Name",
            table: "JellogBlobContainers",
            columns: new[] { "TenantId", "Name" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogBlobs_ContainerId",
            table: "JellogBlobs",
            column: "ContainerId");

        migrationBuilder.CreateIndex(
            name: "IX_JellogBlobs_TenantId_ContainerId_Name",
            table: "JellogBlobs",
            columns: new[] { "TenantId", "ContainerId", "Name" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogEntityChanges_AuditLogId",
            table: "JellogEntityChanges",
            column: "AuditLogId");

        migrationBuilder.CreateIndex(
            name: "IX_JellogEntityChanges_TenantId_EntityTypeFullName_EntityId",
            table: "JellogEntityChanges",
            columns: new[] { "TenantId", "EntityTypeFullName", "EntityId" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogEntityPropertyChanges_EntityChangeId",
            table: "JellogEntityPropertyChanges",
            column: "EntityChangeId");

        migrationBuilder.CreateIndex(
            name: "IX_JellogFeatureValues_Name_ProviderName_ProviderKey",
            table: "JellogFeatureValues",
            columns: new[] { "Name", "ProviderName", "ProviderKey" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogLinkUsers_SourceUserId_SourceTenantId_TargetUserId_TargetTenantId",
            table: "JellogLinkUsers",
            columns: new[] { "SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId" },
            unique: true,
            filter: "[SourceTenantId] IS NOT NULL AND [TargetTenantId] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_JellogOrganizationUnitRoles_RoleId_OrganizationUnitId",
            table: "JellogOrganizationUnitRoles",
            columns: new[] { "RoleId", "OrganizationUnitId" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogOrganizationUnits_Code",
            table: "JellogOrganizationUnits",
            column: "Code");

        migrationBuilder.CreateIndex(
            name: "IX_JellogOrganizationUnits_ParentId",
            table: "JellogOrganizationUnits",
            column: "ParentId");

        migrationBuilder.CreateIndex(
            name: "IX_JellogPermissionGrants_Name_ProviderName_ProviderKey",
            table: "JellogPermissionGrants",
            columns: new[] { "Name", "ProviderName", "ProviderKey" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogRoleClaims_RoleId",
            table: "JellogRoleClaims",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_JellogRoles_NormalizedName",
            table: "JellogRoles",
            column: "NormalizedName");

        migrationBuilder.CreateIndex(
            name: "IX_JellogSecurityLogs_TenantId_Action",
            table: "JellogSecurityLogs",
            columns: new[] { "TenantId", "Action" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogSecurityLogs_TenantId_ApplicationName",
            table: "JellogSecurityLogs",
            columns: new[] { "TenantId", "ApplicationName" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogSecurityLogs_TenantId_Identity",
            table: "JellogSecurityLogs",
            columns: new[] { "TenantId", "Identity" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogSecurityLogs_TenantId_UserId",
            table: "JellogSecurityLogs",
            columns: new[] { "TenantId", "UserId" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogSettings_Name_ProviderName_ProviderKey",
            table: "JellogSettings",
            columns: new[] { "Name", "ProviderName", "ProviderKey" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogUserClaims_UserId",
            table: "JellogUserClaims",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_JellogUserLogins_LoginProvider_ProviderKey",
            table: "JellogUserLogins",
            columns: new[] { "LoginProvider", "ProviderKey" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogUserOrganizationUnits_UserId_OrganizationUnitId",
            table: "JellogUserOrganizationUnits",
            columns: new[] { "UserId", "OrganizationUnitId" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogUserRoles_RoleId_UserId",
            table: "JellogUserRoles",
            columns: new[] { "RoleId", "UserId" });

        migrationBuilder.CreateIndex(
            name: "IX_JellogUsers_Email",
            table: "JellogUsers",
            column: "Email");

        migrationBuilder.CreateIndex(
            name: "IX_JellogUsers_NormalizedEmail",
            table: "JellogUsers",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "IX_JellogUsers_NormalizedUserName",
            table: "JellogUsers",
            column: "NormalizedUserName");

        migrationBuilder.CreateIndex(
            name: "IX_JellogUsers_UserName",
            table: "JellogUsers",
            column: "UserName");

        migrationBuilder.CreateIndex(
            name: "IX_IdentityServerClients_ClientId",
            table: "IdentityServerClients",
            column: "ClientId");

        migrationBuilder.CreateIndex(
            name: "IX_IdentityServerDeviceFlowCodes_DeviceCode",
            table: "IdentityServerDeviceFlowCodes",
            column: "DeviceCode",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_IdentityServerDeviceFlowCodes_Expiration",
            table: "IdentityServerDeviceFlowCodes",
            column: "Expiration");

        migrationBuilder.CreateIndex(
            name: "IX_IdentityServerDeviceFlowCodes_UserCode",
            table: "IdentityServerDeviceFlowCodes",
            column: "UserCode");

        migrationBuilder.CreateIndex(
            name: "IX_IdentityServerPersistedGrants_Expiration",
            table: "IdentityServerPersistedGrants",
            column: "Expiration");

        migrationBuilder.CreateIndex(
            name: "IX_IdentityServerPersistedGrants_SubjectId_ClientId_Type",
            table: "IdentityServerPersistedGrants",
            columns: new[] { "SubjectId", "ClientId", "Type" });

        migrationBuilder.CreateIndex(
            name: "IX_IdentityServerPersistedGrants_SubjectId_SessionId_Type",
            table: "IdentityServerPersistedGrants",
            columns: new[] { "SubjectId", "SessionId", "Type" });

        migrationBuilder.CreateIndex(
            name: "IX_PayPaymentRequests_CreationTime",
            table: "PayPaymentRequests",
            column: "CreationTime");

        migrationBuilder.CreateIndex(
            name: "IX_SaasEditions_DisplayName",
            table: "SaasEditions",
            column: "DisplayName");

        migrationBuilder.CreateIndex(
            name: "IX_SaasTenants_Name",
            table: "SaasTenants",
            column: "Name");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "JellogAuditLogActions");

        migrationBuilder.DropTable(
            name: "JellogBlobs");

        migrationBuilder.DropTable(
            name: "JellogClaimTypes");

        migrationBuilder.DropTable(
            name: "JellogEntityPropertyChanges");

        migrationBuilder.DropTable(
            name: "JellogFeatureValues");

        migrationBuilder.DropTable(
            name: "JellogLinkUsers");

        migrationBuilder.DropTable(
            name: "JellogOrganizationUnitRoles");

        migrationBuilder.DropTable(
            name: "JellogPermissionGrants");

        migrationBuilder.DropTable(
            name: "JellogRoleClaims");

        migrationBuilder.DropTable(
            name: "JellogSecurityLogs");

        migrationBuilder.DropTable(
            name: "JellogSettings");

        migrationBuilder.DropTable(
            name: "JellogUserClaims");

        migrationBuilder.DropTable(
            name: "JellogUserLogins");

        migrationBuilder.DropTable(
            name: "JellogUserOrganizationUnits");

        migrationBuilder.DropTable(
            name: "JellogUserRoles");

        migrationBuilder.DropTable(
            name: "JellogUserTokens");

        migrationBuilder.DropTable(
            name: "IdentityServerApiResourceClaims");

        migrationBuilder.DropTable(
            name: "IdentityServerApiResourceProperties");

        migrationBuilder.DropTable(
            name: "IdentityServerApiResourceScopes");

        migrationBuilder.DropTable(
            name: "IdentityServerApiResourceSecrets");

        migrationBuilder.DropTable(
            name: "IdentityServerApiScopeClaims");

        migrationBuilder.DropTable(
            name: "IdentityServerApiScopeProperties");

        migrationBuilder.DropTable(
            name: "IdentityServerClientClaims");

        migrationBuilder.DropTable(
            name: "IdentityServerClientCorsOrigins");

        migrationBuilder.DropTable(
            name: "IdentityServerClientGrantTypes");

        migrationBuilder.DropTable(
            name: "IdentityServerClientIdPRestrictions");

        migrationBuilder.DropTable(
            name: "IdentityServerClientPostLogoutRedirectUris");

        migrationBuilder.DropTable(
            name: "IdentityServerClientProperties");

        migrationBuilder.DropTable(
            name: "IdentityServerClientRedirectUris");

        migrationBuilder.DropTable(
            name: "IdentityServerClientScopes");

        migrationBuilder.DropTable(
            name: "IdentityServerClientSecrets");

        migrationBuilder.DropTable(
            name: "IdentityServerDeviceFlowCodes");

        migrationBuilder.DropTable(
            name: "IdentityServerIdentityResourceClaims");

        migrationBuilder.DropTable(
            name: "IdentityServerIdentityResourceProperties");

        migrationBuilder.DropTable(
            name: "IdentityServerPersistedGrants");

        migrationBuilder.DropTable(
            name: "PayGatewayPlans");

        migrationBuilder.DropTable(
            name: "PayPaymentRequestProducts");

        migrationBuilder.DropTable(
            name: "SaasEditions");

        migrationBuilder.DropTable(
            name: "SaasTenantConnectionStrings");

        migrationBuilder.DropTable(
            name: "JellogBlobContainers");

        migrationBuilder.DropTable(
            name: "JellogEntityChanges");

        migrationBuilder.DropTable(
            name: "JellogOrganizationUnits");

        migrationBuilder.DropTable(
            name: "JellogRoles");

        migrationBuilder.DropTable(
            name: "JellogUsers");

        migrationBuilder.DropTable(
            name: "IdentityServerApiResources");

        migrationBuilder.DropTable(
            name: "IdentityServerApiScopes");

        migrationBuilder.DropTable(
            name: "IdentityServerClients");

        migrationBuilder.DropTable(
            name: "IdentityServerIdentityResources");

        migrationBuilder.DropTable(
            name: "PayPlans");

        migrationBuilder.DropTable(
            name: "PayPaymentRequests");

        migrationBuilder.DropTable(
            name: "SaasTenants");

        migrationBuilder.DropTable(
            name: "JellogAuditLogs");
    }
}
