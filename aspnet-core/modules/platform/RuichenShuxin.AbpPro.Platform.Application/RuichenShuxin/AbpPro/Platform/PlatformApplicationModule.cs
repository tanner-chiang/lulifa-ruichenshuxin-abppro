namespace RuichenShuxin.AbpPro.Platform;

[DependsOn(
    typeof(PlatformDomainModule),
    typeof(PlatformApplicationContractsModule),

    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule)
    )]
public class PlatformApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<PlatformApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<PlatformApplicationModule>(validate: true);
        });
    }
}
