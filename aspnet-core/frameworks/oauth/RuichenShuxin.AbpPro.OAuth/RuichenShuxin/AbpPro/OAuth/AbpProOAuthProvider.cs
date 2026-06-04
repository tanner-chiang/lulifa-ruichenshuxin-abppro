namespace RuichenShuxin.AbpPro.OAuth;

public static class AbpProOAuthProvider
{

    public static IServiceCollection AddOAuthProviders(this IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Transient<LoginModel, AbpProLoginModel>());

        services.AddAuthentication().AddGitHubProvider()
                                    .AddGiteeProvider()
                                    .AddBilibiliProvider();

        return services;
    }

    public static AuthenticationBuilder AddGitHubProvider(this AuthenticationBuilder builder)
    {
        return builder.AddGitHub(options =>
        {
            options.ClientId = "ClientId";
            options.ClientSecret = "ClientSecret";
            options.Scope.Add("user:email");
        })
        .UseSettingProvider<
            GitHubAuthenticationOptions,
            GitHubAuthenticationHandler,
            GitHubAuthHandlerOptionsProvider>();
    }

    public static AuthenticationBuilder AddGiteeProvider(this AuthenticationBuilder builder)
    {
        return builder.AddGitee(options =>
        {
            options.ClientId = "ClientId";
            options.ClientSecret = "ClientSecret";
        })
        .UseSettingProvider<
            GiteeAuthenticationOptions,
            GiteeAuthenticationHandler,
            GiteeAuthHandlerOptionsProvider>();
    }

    public static AuthenticationBuilder AddBilibiliProvider(this AuthenticationBuilder builder)
    {
        return builder.AddBilibili(options =>
        {
            options.ClientId = "ClientId";
            options.ClientSecret = "ClientSecret";
        })
        .UseSettingProvider<
            BilibiliAuthenticationOptions,
            BilibiliAuthenticationHandler,
            BilibiliAuthHandlerOptionsProvider>();
    }

}
