namespace RuichenShuxin.AbpPro.OAuth;

public class AbpProOAuthSettingDefinitionProvider : Volo.Abp.Settings.SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(GetGitHubSettings());
        context.Add(GetGiteeSettings());
        context.Add(GetBilibiliSettings());
    }

    private SettingDefinition[] GetGitHubSettings()
    {
        return
        [
            new SettingDefinition(
                AbpProOAuthSettingNames.GitHub.ClientId,
                displayName: L("Settings:GitHubClientId"),
                description: L("Settings:GitHubClientIdDesc"),
                isVisibleToClients: false,
                isEncrypted: true)
            .WithProviders(
                DefaultValueSettingValueProvider.ProviderName,
                ConfigurationSettingValueProvider.ProviderName,
                GlobalSettingValueProvider.ProviderName,
                TenantSettingValueProvider.ProviderName),
            new SettingDefinition(
                AbpProOAuthSettingNames.GitHub.ClientSecret,
                displayName: L("Settings:GitHubClientSecret"),
                description: L("Settings:GitHubClientSecretDesc"),
                isVisibleToClients: false,
                isEncrypted: true)
            .WithProviders(
                DefaultValueSettingValueProvider.ProviderName,
                ConfigurationSettingValueProvider.ProviderName,
                GlobalSettingValueProvider.ProviderName,
                TenantSettingValueProvider.ProviderName),
        ];
    }

    private SettingDefinition[] GetGiteeSettings()
    {
        return
        [
            new SettingDefinition(
                AbpProOAuthSettingNames.Gitee.ClientId,
                displayName: L("Settings:GiteeClientId"),
                description: L("Settings:GiteeClientIdDesc"),
                isVisibleToClients: false,
                isEncrypted: true)
            .WithProviders(
                DefaultValueSettingValueProvider.ProviderName,
                ConfigurationSettingValueProvider.ProviderName,
                GlobalSettingValueProvider.ProviderName,
                TenantSettingValueProvider.ProviderName),
            new SettingDefinition(
                AbpProOAuthSettingNames.Gitee.ClientSecret,
                displayName: L("Settings:GiteeClientSecret"),
                description: L("Settings:GiteeClientSecretDesc"),
                isVisibleToClients: false,
                isEncrypted: true)
            .WithProviders(
                DefaultValueSettingValueProvider.ProviderName,
                ConfigurationSettingValueProvider.ProviderName,
                GlobalSettingValueProvider.ProviderName,
                TenantSettingValueProvider.ProviderName),
        ];
    }

    private SettingDefinition[] GetBilibiliSettings()
    {
        return
        [
            new SettingDefinition(
                AbpProOAuthSettingNames.Bilibili.ClientId,
                displayName: L("Settings:BilibiliClientId"),
                description: L("Settings:BilibiliClientIdDesc"),
                isVisibleToClients: false,
                isEncrypted: true)
            .WithProviders(
                DefaultValueSettingValueProvider.ProviderName,
                ConfigurationSettingValueProvider.ProviderName,
                GlobalSettingValueProvider.ProviderName,
                TenantSettingValueProvider.ProviderName),
            new SettingDefinition(
                AbpProOAuthSettingNames.Bilibili.ClientSecret,
                displayName: L("Settings:BilibiliClientSecret"),
                description: L("Settings:BilibiliClientSecretDesc"),
                isVisibleToClients: false,
                isEncrypted: true)
            .WithProviders(
                DefaultValueSettingValueProvider.ProviderName,
                ConfigurationSettingValueProvider.ProviderName,
                GlobalSettingValueProvider.ProviderName,
                TenantSettingValueProvider.ProviderName),
        ];
    }

    protected ILocalizableString L(string name)
    {
        return LocalizableString.Create<AbpProOAuthResource>(name);
    }

}
