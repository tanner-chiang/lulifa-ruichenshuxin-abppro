namespace RuichenShuxin.AbpPro.Platform;

[DependsOn(
    typeof(PlatformDomainSharedModule),

    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpTenantManagementApplicationContractsModule)
    )]
public class PlatformApplicationContractsModule : AbpModule
{

}
