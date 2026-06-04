using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace RuichenShuxin.AbpPro.Storage.EntityFrameworkCore;

[ConnectionStringName(StorageDbProperties.ConnectionStringName)]
public class StorageDbContext : AbpDbContext<StorageDbContext>, IStorageDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public StorageDbContext(DbContextOptions<StorageDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureStorage();
    }
}
