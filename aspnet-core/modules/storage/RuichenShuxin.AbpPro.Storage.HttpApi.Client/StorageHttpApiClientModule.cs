using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace RuichenShuxin.AbpPro.Storage;

[DependsOn(
    typeof(StorageApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class StorageHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(StorageApplicationContractsModule).Assembly,
            StorageRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<StorageHttpApiClientModule>();
        });

    }
}
