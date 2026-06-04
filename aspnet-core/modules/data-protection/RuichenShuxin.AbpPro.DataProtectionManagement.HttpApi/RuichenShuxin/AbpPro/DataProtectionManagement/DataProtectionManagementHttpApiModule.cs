namespace RuichenShuxin.AbpPro.DataProtectionManagement;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(DataProtectionManagementApplicationContractsModule))]
public class DataProtectionManagementHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DataProtectionManagementHttpApiModule).Assembly);
        });

        PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(DataProtectionResource),
                typeof(DataProtectionManagementApplicationContractsModule).Assembly);
        });
    }
}
