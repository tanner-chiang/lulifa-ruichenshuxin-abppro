namespace RuichenShuxin.AbpPro;

public partial class AbpProHttpApiHostModule
{
    /// <summary>
    /// 预配置OpenIddict
    /// </summary>
    private void PreConfigureOpenIddict(IConfiguration configuration, IWebHostEnvironment environment)
    {
        var authOptions = configuration.GetOptions<AuthServerOptions>();

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences(authOptions.Scopes);
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });

        if (!environment.IsDevelopment())
        {
            PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
            {
                options.AddDevelopmentEncryptionAndSigningCertificate = false;
            });

            PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
            {
                serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx", authOptions.CertificatePassPhrase);
                serverBuilder.SetIssuer(new Uri(authOptions.Authority));
            });
        }
    }




    /// <summary>
    /// 配置统一响应包装器
    /// </summary>
    private void ConfigureWrapper()
    {
        Configure<AbpProWrapperOptions>(options =>
        {
            options.IsEnabled = true;
        });
    }


    /// <summary>
    /// 配置安全相关选项
    /// </summary>
    private void ConfigureSecurity(IConfiguration configuration)
    {
        var appOptions = configuration.GetOptions<AppOptions>();

        var authOptions = configuration.GetOptions<AuthServerOptions>();

        // PII 配置
        if (!appOptions.DisablePII)
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.LogCompleteSecurityArtifact = true;
        }

        // HTTPS 配置
        if (!authOptions.RequireHttpsMetadata)
        {
            Configure<OpenIddictServerAspNetCoreOptions>(options =>
            {
                options.DisableTransportSecurityRequirement = true;
            });

            Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
            });
        }
    }

    /// <summary>
    /// 配置认证相关
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });

        // CSRF/XSRF https://abp.io/docs/latest/framework/infrastructure/csrf-anti-forgery
        services.Configure<AbpAntiForgeryOptions>(options =>
        {
            options.AutoValidate = true;
        });

        services.AddSameSiteCookiePolicy();

    }

    /// <summary>
    /// 配置Urls相关
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private void ConfigureUrls(IConfiguration configuration)
    {
        var appOptions = configuration.GetOptions<AppOptions>();

        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = appOptions.SelfUrl;
            options.Applications["Vue"].RootUrl = appOptions.VueUrl;
            options.Applications["Vue"].Urls[AbpProCoreConsts.Urls.PasswordReset] = "account/reset-password";
            options.RedirectAllowedUrls.AddRange(appOptions.RedirectAllowedUrls?.Split(',') ?? Array.Empty<string>());
        });

    }

    /// <summary>
    /// Theme相关
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );

            options.ScriptBundles.Configure(
                LeptonXLiteThemeBundles.Scripts.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-scripts.js");
                    bundle.AddContributors(typeof(AbpProGlobalScriptContributor));
                }
            );
        });
    }

    /// <summary>
    /// Swagger相关配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private void ConfigureSwagger(IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration.GetOptions<AuthServerOptions>();

        services.AddAbpSwaggerGenWithOidc(
            authOptions.Authority,
            authOptions.Scopes,
            [AbpSwaggerOidcFlows.AuthorizationCode],
            null,
            options =>
            {
                options.SwaggerDoc(AbpProCoreConsts.Swagger.Version, new OpenApiInfo
                {
                    Title = AbpProCoreConsts.Swagger.ApiTitle,
                    Version = AbpProCoreConsts.Swagger.Version
                });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);

                options.DocumentFilter<AbpProHideDefaultApiFilter>();
                options.OperationFilter<AbpProOperationFilter>();

                // 自动扫描所有 XML 注释
                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                foreach (var xmlPath in xmlFiles)
                {
                    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                }

            });
    }

    /// <summary>
    /// 跨域相关配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private void ConfigureCors(IServiceCollection services, IConfiguration configuration)
    {
        var appOptions = services.GetConfiguration().GetOptions<AppOptions>();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        appOptions.CorsOrigins?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.Trim().RemovePostFix("/"))
                            .ToArray() ?? Array.Empty<string>()
                    )
                    .WithAbpExposedHeaders()
                    .WithAbpProWrapExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    /// <summary>
    /// 健康检查相关配置
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private void ConfigureHealthChecks(IServiceCollection services)
    {

        services.AddAbpProHealthChecks();

    }

    /// <summary>
    /// 多租户相关配置
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private void ConfigureMultiTenancy(IConfiguration configuration)
    {
        var multiTenancyOptions = configuration.GetOptions<MultiTenancyOptions>();

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = multiTenancyOptions.IsEnabled;
        });

    }

    /// <summary>
    /// 种子数据相关配置
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private void ConfigureDataSeed(IServiceCollection services)
    {

        services.AddHostedService<AbpProDataSeedWorker>();

    }

    /// <summary>
    /// 多语言相关配置
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private void ConfigureLocalization(IConfiguration configuration)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));

        });

    }

    /// <summary>
    /// 分布式缓存相关配置
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private void ConfigureCache(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var redisOptions = configuration.GetOptions<RedisOptions>();

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = $"{AbpProCoreConsts.ApplicationName}:";
        });

        var dataProtectionBuilder = services.AddDataProtection().SetApplicationName(AbpProCoreConsts.ApplicationName);

        if (redisOptions.IsEnabled)
        {
            Configure<RedisCacheOptions>(options =>
            {
                options.Configuration = redisOptions.Configuration;
                options.InstanceName = redisOptions.InstanceName;
            });

            var redis = ConnectionMultiplexer.Connect(redisOptions.Configuration);

            services.AddSingleton<IDistributedLockProvider>(sp =>
            {
                return new RedisDistributedSynchronizationProvider(redis.GetDatabase());
            });

            if (!environment.IsDevelopment())
            {
                dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, $"{AbpProCoreConsts.ApplicationName}-Protection-Keys");
            }
        }

    }


}
