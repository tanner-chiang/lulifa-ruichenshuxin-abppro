namespace RuichenShuxin.AbpPro.DataProtectionManagement.EntityFrameworkCore;

[DependsOn(
    typeof(DataProtectionManagementDomainModule),
    typeof(AbpProDataProtectionEntityFrameworkCoreModule)
)]
public class DataProtectionManagementEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<DataProtectionManagementDbContext>(options =>
        {
            options.AddRepository<EntityTypeInfo, EfCoreEntityTypeInfoRepository>();

            options.AddRepository<RoleEntityRule, EfCoreRoleEntityRuleRepository>();
            options.AddRepository<OrganizationUnitEntityRule, EfCoreOrganizationUnitEntityRuleRepository>();

            options.AddRepository<SubjectStrategy, EfCoreSubjectStrategyRepository>();

            options.AddDefaultRepositories();
        });
    }
}
