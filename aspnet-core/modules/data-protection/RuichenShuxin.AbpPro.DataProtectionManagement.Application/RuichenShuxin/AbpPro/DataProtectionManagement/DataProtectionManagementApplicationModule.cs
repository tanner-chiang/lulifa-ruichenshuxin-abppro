namespace RuichenShuxin.AbpPro.DataProtectionManagement;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(DataProtectionManagementApplicationContractsModule),
    typeof(DataProtectionManagementDomainModule))]
public class DataProtectionManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<DataProtectionManagementApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<DataProtectionManagementApplicationMappingProfile>(validate: true);
        });
    }
}
