namespace RuichenShuxin.AbpPro.DataProtection;

public interface IDataAccessStrategyStateProvider
{
    Task<DataAccessStrategyState> GetOrNullAsync();
}
