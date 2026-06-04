namespace RuichenShuxin.AbpPro;

[DependsOn(
   typeof(AbpAspNetCoreMvcModule),
   typeof(AbpProApplicationContractsModule))]
public class AbpProHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpProHttpApiModule).Assembly);
        });
    }
}
