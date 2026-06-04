using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace RuichenShuxin.AbpPro.Storage.EntityFrameworkCore;

[DependsOn(
    typeof(StorageDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class StorageEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<StorageDbContext>(options =>
        {
            options.AddDefaultRepositories<IStorageDbContext>(includeAllEntities: true);
            
            /* Add custom repositories here. Example:
            * options.AddRepository<Question, EfCoreQuestionRepository>();
            */
        });
    }
}
