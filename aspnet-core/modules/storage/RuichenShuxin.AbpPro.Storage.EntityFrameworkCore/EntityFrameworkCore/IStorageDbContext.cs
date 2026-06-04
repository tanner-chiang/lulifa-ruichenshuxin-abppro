using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace RuichenShuxin.AbpPro.Storage.EntityFrameworkCore;

[ConnectionStringName(StorageDbProperties.ConnectionStringName)]
public interface IStorageDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
