namespace RuichenShuxin.AbpPro.DataProtection;

public interface IDataProtectedStrategyStateCache
{
    Task SetAsync(DataAccessStrategyState state);

    Task RemoveAsync(DataAccessStrategyState state);

    Task<DataAccessStrategyStateCacheItem> GetAsync(string subjectName, string subjectId);
}
