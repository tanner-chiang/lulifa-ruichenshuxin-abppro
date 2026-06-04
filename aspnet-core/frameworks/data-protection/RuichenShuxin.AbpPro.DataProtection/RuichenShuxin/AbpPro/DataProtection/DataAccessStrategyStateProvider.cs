namespace RuichenShuxin.AbpPro.DataProtection;

public class DataAccessStrategyStateProvider : IDataAccessStrategyStateProvider, ITransientDependency
{
    private readonly AbpProDataProtectionOptions _options;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public DataAccessStrategyStateProvider(
        IOptions<AbpProDataProtectionOptions> options, 
        IServiceScopeFactory serviceScopeFactory)
    {
        _options = options.Value;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async virtual Task<DataAccessStrategyState> GetOrNullAsync()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = new DataAccessStrategyContributorContext(scope.ServiceProvider);

        foreach (var contributor in _options.StrategyContributors)
        {
            var strategyState = await contributor.GetOrNullAsync(context);
            if (strategyState != null)
            {
                return strategyState;
            }
        }

        return null;
    }
}
