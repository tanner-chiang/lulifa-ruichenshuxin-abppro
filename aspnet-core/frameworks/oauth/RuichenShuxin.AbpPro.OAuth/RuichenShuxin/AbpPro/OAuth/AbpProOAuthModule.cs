namespace RuichenShuxin.AbpPro.OAuth;

[DependsOn(
    typeof(AbpProCoreModule),
    typeof(AbpFeaturesModule),
    typeof(AbpSettingsModule))]
public class AbpProOAuthModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpProOAuthModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AbpProOAuthResource>()
                .AddVirtualJson("/RuichenShuxin/AbpPro/OAuth/Localization/Resources");
        });

        context.Services.AddOAuthProviders();
    }
}
