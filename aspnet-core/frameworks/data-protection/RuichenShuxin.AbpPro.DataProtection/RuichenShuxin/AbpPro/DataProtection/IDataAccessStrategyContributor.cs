namespace RuichenShuxin.AbpPro.DataProtection;

public interface IDataAccessStrategyContributor
{
    string Name { get; }
    Task<DataAccessStrategyState> GetOrNullAsync(DataAccessStrategyContributorContext context);
}
