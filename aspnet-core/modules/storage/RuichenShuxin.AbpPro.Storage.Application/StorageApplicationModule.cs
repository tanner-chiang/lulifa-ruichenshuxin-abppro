using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace RuichenShuxin.AbpPro.Storage;

[DependsOn(
    typeof(StorageDomainModule),
    typeof(StorageApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class StorageApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<StorageApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<StorageApplicationModule>(validate: true);
        });
    }
}
