namespace RuichenShuxin.AbpPro.DataProtectionManagement;

[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(AbpDddDomainModule),
    typeof(AbpProDataProtectionModule),
    typeof(DataProtectionManagementDomainSharedModule)
 )]
public class DataProtectionManagementDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<DataProtectionManagementDomainModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<DataProtectionManagementDomainMappingProfile>(validate: true);
        });

        Configure<AbpDistributedEntityEventOptions>(options =>
        {
            options.EtoMappings.Add<EntityTypeInfo, EntityTypeInfoEto>();
            options.EtoMappings.Add<RoleEntityRule, RoleEntityRuleEto>();
            options.EtoMappings.Add<OrganizationUnitEntityRule, OrganizationUnitEntityRuleEto>();

            options.AutoEventSelectors.Add<EntityTypeInfo>();
            options.AutoEventSelectors.Add<RoleEntityRule>();
            options.AutoEventSelectors.Add<OrganizationUnitEntityRule>();
        });

        context.Services.AddHostedService<ProtectedEntitiesSaverService>();
    }
}
