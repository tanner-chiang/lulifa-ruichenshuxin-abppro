namespace RuichenShuxin.AbpPro.Core;

public class AbpProDataSeedWorker : BackgroundService
{
    protected IDataSeeder DataSeeder { get; }
    public AbpProDataSeedWorker(IDataSeeder dataSeeder)
    {
        DataSeeder = dataSeeder;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await DataSeeder.SeedAsync();
    }
}
