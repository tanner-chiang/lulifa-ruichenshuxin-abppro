namespace RuichenShuxin.AbpPro.OAuth;

public class GitHubAuthHandlerOptionsProvider : OAuthHandlerOptionsProvider<GitHubAuthenticationOptions>
{
    public GitHubAuthHandlerOptionsProvider(ISettingProvider settingProvider) : base(settingProvider)
    {
    }

    public async override Task SetOptionsAsync(GitHubAuthenticationOptions options)
    {
        var clientId = await SettingProvider.GetOrNullAsync(AbpProOAuthSettingNames.GitHub.ClientId);
        var clientSecret = await SettingProvider.GetOrNullAsync(AbpProOAuthSettingNames.GitHub.ClientSecret);

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
