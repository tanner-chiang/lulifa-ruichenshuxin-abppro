namespace RuichenShuxin.AbpPro.OAuth;

public abstract class OAuthHandlerOptionsProvider<TOptions> : IOAuthHandlerOptionsProvider<TOptions>, ITransientDependency
    where TOptions : RemoteAuthenticationOptions, new()
{
    protected ISettingProvider SettingProvider { get; }
    public OAuthHandlerOptionsProvider(ISettingProvider settingProvider)
    {
        SettingProvider = settingProvider;
    }

    public virtual Task SetOptionsAsync(TOptions options)
    {
        options.CorrelationCookie.SameSite = SameSiteMode.Lax;
        options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.CorrelationCookie.HttpOnly = true;

        return Task.CompletedTask;
    }
}
