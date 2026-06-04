namespace RuichenShuxin.AbpPro.Core;

public static class AbpProApplicationExtensions
{

    public static IApplicationBuilder UseAbpProRequestLocalization(this IApplicationBuilder app)
    {
        return app.UseAbpRequestLocalization(options =>
        {
            options.RequestCultureProviders.RemoveAll(provider => provider is AcceptLanguageHeaderRequestCultureProvider);

            options.RequestCultureProviders.Add(new AbpProCultureProvider());

        });
    }

    public static IApplicationBuilder UseAbpProSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        var authOptions = configuration.GetOptions<AuthServerOptions>();

        app.UseSwagger();

        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint(AbpProCoreConsts.Swagger.JsonEndpoint, AbpProCoreConsts.Swagger.ApiTitle);

            options.OAuthClientId(authOptions.SwaggerClientId);

            options.OAuthScopes(authOptions.Scopes);
        });

        return app;
    }

    public static IApplicationBuilder UseAbpProMultiTenancy(this IApplicationBuilder app, IConfiguration configuration)
    {
        var multiTenancyOptions = configuration.GetOptions<MultiTenancyOptions>();

        if (multiTenancyOptions.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        return app;

    }



    #region 配置拓展方法


    public static IServiceCollection ConfigureOptions<T>(this IServiceCollection services)
        where T : class
    {
        var configuration = services.GetConfiguration();
        services.Configure<T>(options => configuration.BindOptions(options));
        return services;
    }

    public static void BindOptions<T>(this IConfiguration configuration, T options, string sectionName = null) where T : class
    {
        sectionName ??= typeof(T).Name.Replace("Options", "");
        configuration.GetSection(sectionName).Bind(options);
    }


    public static T GetOptions<T>(this IConfiguration configuration, string sectionName = null) where T : new()
    {
        sectionName ??= typeof(T).Name.Replace("Options", "");

        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }


    #endregion

}
