namespace RuichenShuxin.AbpPro.DataProtection;

public interface IDataProtectedStrategyStateStore
{
    Task SetAsync(DataAccessStrategyState state);

    Task RemoveAsync(DataAccessStrategyState state);

    Task<DataAccessStrategyState> GetOrNullAsync(string subjectName, string subjectId);
}
