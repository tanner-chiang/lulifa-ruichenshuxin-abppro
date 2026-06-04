namespace RuichenShuxin.AbpPro.DataProtection;

public class DataAccessStrategyContributorContext
{
    public IServiceProvider ServiceProvider { get; }
    public DataAccessStrategyContributorContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}
