using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace RuichenShuxin.AbpPro.Storage;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(StorageDomainSharedModule)
)]
public class StorageDomainModule : AbpModule
{

}
