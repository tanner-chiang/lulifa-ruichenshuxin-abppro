namespace RuichenShuxin.AbpPro.DataProtectionManagement.EntityFrameworkCore;

public interface IDataProtectionManagementDbContext : IEfCoreDbContext
{
    DbSet<EntityTypeInfo> EntityTypeInfos { get; set; }
}
