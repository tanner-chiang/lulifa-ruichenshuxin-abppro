namespace RuichenShuxin.AbpPro.Platform;

[DependsOn(
    typeof(AbpValidationModule),
    typeof(AbpDddDomainSharedModule),
    typeof(AbpAuditLoggingDomainSharedModule),
    typeof(AbpBackgroundJobsDomainSharedModule),
    typeof(AbpFeatureManagementDomainSharedModule),
    typeof(AbpPermissionManagementDomainSharedModule),
    typeof(AbpSettingManagementDomainSharedModule),
    typeof(AbpIdentityDomainSharedModule),
    typeof(AbpOpenIddictDomainSharedModule),
    typeof(AbpTenantManagementDomainSharedModule),
    typeof(AbpProCoreModule)
)]
public class PlatformDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PlatformDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<PlatformResource>(AbpProCoreConsts.Languages.ZhHans)
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/RuichenShuxin/AbpPro/Platform/Localization/Resources");

            options.Resources
                   .Get<IdentityResource>()
                   .AddVirtualJson("/RuichenShuxin/AbpPro/Platform/Localization/Identity");

            options.Resources
                   .Add<AbpSaasResource>(AbpProCoreConsts.Languages.ZhHans)
                   .AddVirtualJson("/RuichenShuxin/AbpPro/Platform/Localization/Saas");

            options.Resources
                   .Get<AbpOpenIddictResource>()
                   .AddVirtualJson("/RuichenShuxin/AbpPro/Platform/Localization/OpenIddict");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace(PlatformErrorCodes.Namespace, typeof(PlatformResource));
        });
    }
}
