namespace RuichenShuxin.AbpPro;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpProDataProtectionModule),
    typeof(AbpProDomainModule),
    typeof(AbpProApplicationContractsModule)
    )]
public class AbpProApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpProApplicationModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AbpProApplicationModule>();
        });
    }
}
