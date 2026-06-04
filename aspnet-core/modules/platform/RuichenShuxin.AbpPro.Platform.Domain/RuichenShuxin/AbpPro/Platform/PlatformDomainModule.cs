using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.OpenIddict;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace RuichenShuxin.AbpPro.Platform;

[DependsOn(
    typeof(AbpEventBusModule),
    typeof(AbpDddDomainModule),
    typeof(PlatformDomainSharedModule),
    typeof(AbpAuditLoggingDomainModule),
    typeof(AbpCachingModule),
    typeof(AbpBackgroundJobsDomainModule),
    typeof(AbpFeatureManagementDomainModule),
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpPermissionManagementDomainOpenIddictModule),
    typeof(AbpSettingManagementDomainModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpOpenIddictDomainModule),
    typeof(AbpTenantManagementDomainModule)
)]
public class PlatformDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<PlatformDomainModule>();

        Configure<DataItemMappingOptions>(options =>
        {
            options.SetDefaultMapping();
        });

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<PlatformDomainMappingProfile>(validate: true);
        });

        Configure<AbpDistributedEntityEventOptions>(options =>
        {
            options.EtoMappings.Add<Layout, LayoutEto>(typeof(PlatformDomainModule));
            options.EtoMappings.Add<Menu, MenuEto>(typeof(PlatformDomainModule));
            options.EtoMappings.Add<UserMenu, UserMenuEto>(typeof(PlatformDomainModule));
            options.EtoMappings.Add<RoleMenu, RoleMenuEto>(typeof(PlatformDomainModule));
        });

        Configure<PermissionManagementOptions>(options =>
        {
            options.ManagementProviders.Add<OrganizationUnitPermissionManagementProvider>();

            options.ProviderPolicies[OrganizationUnitPermissionValueProvider.ProviderName] = "AbpIdentity.OrganizationUnits.ManagePermissions";
        });
    }
}
