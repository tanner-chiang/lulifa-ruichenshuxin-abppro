namespace RuichenShuxin.AbpPro.DataProtection;

public interface IDataProtectionRepository<TEntity> : IDataProtectedEnabled
{
    Task<IQueryable<TEntity>> GetQueryableAsync();
}
