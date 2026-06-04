namespace RuichenShuxin.AbpPro.OAuth;

public class GiteeAuthHandlerOptionsProvider : OAuthHandlerOptionsProvider<GiteeAuthenticationOptions>
{
    public GiteeAuthHandlerOptionsProvider(ISettingProvider settingProvider) : base(settingProvider)
    {
    }

    public async override Task SetOptionsAsync(GiteeAuthenticationOptions options)
    {
        var clientId = await SettingProvider.GetOrNullAsync(AbpProOAuthSettingNames.Gitee.ClientId);
        var clientSecret = await SettingProvider.GetOrNullAsync(AbpProOAuthSettingNames.Gitee.ClientSecret);

        if (!clientId.IsNullOrWhiteSpace())
        {
            options.ClientId = clientId;
        }
        if (!clientSecret.IsNullOrWhiteSpace())
        {
            options.ClientSecret = clientSecret;
        }

        await base.SetOptionsAsync(options);
    }
}
