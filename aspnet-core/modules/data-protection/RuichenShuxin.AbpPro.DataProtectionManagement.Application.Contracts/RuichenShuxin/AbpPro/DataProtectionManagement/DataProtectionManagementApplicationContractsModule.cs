namespace RuichenShuxin.AbpPro.DataProtectionManagement;

[DependsOn(
    typeof(AbpAuthorizationModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(DataProtectionManagementDomainSharedModule))]
public class DataProtectionManagementApplicationContractsModule : AbpModule
{

}
