namespace RuichenShuxin.AbpPro.DataProtectionManagement.EntityFrameworkCore;

public class DataProtectionManagementDbContext : AbpDbContext<DataProtectionManagementDbContext>, IDataProtectionManagementDbContext
{
    public virtual DbSet<EntityTypeInfo> EntityTypeInfos { get; set; }

    public DataProtectionManagementDbContext(
        DbContextOptions<DataProtectionManagementDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDataProtectionManagement();
    }
}
