namespace RuichenShuxin.AbpPro.Authorization.OrganizationUnits;

[DependsOn(typeof(AbpAuthorizationModule))]
public class AbpProAuthorizationOrganizationUnitsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpPermissionOptions>(options =>
        {
            options.ValueProviders.Add<OrganizationUnitPermissionValueProvider>();
        });
    }

}
