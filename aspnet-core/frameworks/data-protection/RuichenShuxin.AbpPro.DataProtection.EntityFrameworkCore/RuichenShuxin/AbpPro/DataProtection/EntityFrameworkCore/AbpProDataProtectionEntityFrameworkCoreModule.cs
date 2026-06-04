namespace RuichenShuxin.AbpPro.DataProtection.EntityFrameworkCore;

[DependsOn(
    typeof(AbpProDataProtectionModule),
    typeof(AbpEntityFrameworkCoreModule))]
public class AbpProDataProtectionEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddTransient<IDataAccessStrategyFilterBuilder, EfCoreDataAccessStrategyFilterBuilder>();
    }
}
