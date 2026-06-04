namespace RuichenShuxin.AbpPro.OAuth;

public interface IOAuthHandlerOptionsProvider<TOptions>
    where TOptions : RemoteAuthenticationOptions, new()
{
    Task SetOptionsAsync(TOptions options);
}
