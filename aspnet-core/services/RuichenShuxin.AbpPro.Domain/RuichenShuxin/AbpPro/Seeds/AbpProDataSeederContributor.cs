namespace RuichenShuxin.AbpPro;

public class AbpProDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{

    public AbpProDataSeederContributor()
    {
    }

    public Task SeedAsync(DataSeedContext context)
    {
        return Task.CompletedTask;
    }
}