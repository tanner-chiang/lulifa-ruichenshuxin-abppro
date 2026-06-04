namespace RuichenShuxin.AbpPro.DataProtection;

public class DataAccessKeywordContributorContext
{
    public IServiceProvider ServiceProvider { get; }
    public LambdaExpression Expression { get; }
    public DataAccessKeywordContributorContext(
        IServiceProvider serviceProvider,
        LambdaExpression expression)
    {
        ServiceProvider = serviceProvider;
        Expression = expression;
    }
}
