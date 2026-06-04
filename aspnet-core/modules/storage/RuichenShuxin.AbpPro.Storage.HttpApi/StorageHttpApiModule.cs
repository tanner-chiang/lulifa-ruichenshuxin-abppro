using Localization.Resources.AbpUi;
using RuichenShuxin.AbpPro.Storage.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace RuichenShuxin.AbpPro.Storage;

[DependsOn(
    typeof(StorageApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class StorageHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(StorageHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<StorageResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
