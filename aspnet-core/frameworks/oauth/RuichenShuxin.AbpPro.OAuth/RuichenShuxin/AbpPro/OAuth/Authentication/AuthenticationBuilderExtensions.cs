namespace RuichenShuxin.AbpPro.OAuth;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder UseSettingProvider<TOptions, THandler, TOptionsProvider>(
        [NotNull] this AuthenticationBuilder authenticationBuilder)
        where TOptions : RemoteAuthenticationOptions, new()
        where THandler : RemoteAuthenticationHandler<TOptions>
        where TOptionsProvider : IOAuthHandlerOptionsProvider<TOptions>
    {
        Check.NotNull(authenticationBuilder, nameof(authenticationBuilder));

        var handler = authenticationBuilder.Services.LastOrDefault(x => x.ServiceType == typeof(THandler));
        authenticationBuilder.Services.Replace(new ServiceDescriptor(
            typeof(THandler),
            provider => new AuthenticationRequestHandler<TOptions, THandler>(
                (THandler)ActivatorUtilities.CreateInstance(provider, typeof(THandler)),
                provider.GetRequiredService<TOptionsProvider>()),
            handler?.Lifetime ?? ServiceLifetime.Transient));

        return authenticationBuilder;
    }

}
