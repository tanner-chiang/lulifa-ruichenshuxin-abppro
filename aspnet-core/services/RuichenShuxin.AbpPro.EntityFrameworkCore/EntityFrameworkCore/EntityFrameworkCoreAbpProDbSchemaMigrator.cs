namespace RuichenShuxin.AbpPro.EntityFrameworkCore;

public class EntityFrameworkCoreAbpProDbSchemaMigrator
    : IAbpProDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreAbpProDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        await _serviceProvider
            .GetRequiredService<AbpProDbContext>()
            .Database
            .MigrateAsync();
    }
}
