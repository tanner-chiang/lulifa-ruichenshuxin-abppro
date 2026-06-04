namespace RuichenShuxin.AbpPro;

[DependsOn(
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule),
    typeof(AbpProDataProtectionAbstractionsModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpProDomainSharedModule))]
public class AbpProApplicationContractsModule : AbpModule
{
}
