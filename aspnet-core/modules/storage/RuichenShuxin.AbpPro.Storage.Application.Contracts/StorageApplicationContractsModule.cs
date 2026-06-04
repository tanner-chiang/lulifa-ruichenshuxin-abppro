using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace RuichenShuxin.AbpPro.Storage;

[DependsOn(
    typeof(StorageDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class StorageApplicationContractsModule : AbpModule
{

}
