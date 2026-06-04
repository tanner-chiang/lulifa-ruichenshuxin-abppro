namespace RuichenShuxin.AbpPro.Core;

public class AbpProCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services.ConfigureOptions<AppOptions>()
                        .ConfigureOptions<AuthServerOptions>()
                        .ConfigureOptions<PlatformCapOptions>()
                        .ConfigureOptions<MultiTenancyOptions>()
                        .ConfigureOptions<RedisOptions>();

    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<TenantConnectionStringCheckOptions>(options =>
        {
            options.ConnectionStringCheckers[AbpProCoreConsts.DatabaseProviderNames.MySql] = new MySqlConnectionStringChecker();
            options.ConnectionStringCheckers[AbpProCoreConsts.DatabaseProviderNames.Oracle] = new OracleConnectionStringChecker();
            options.ConnectionStringCheckers[AbpProCoreConsts.DatabaseProviderNames.Postgres] = new NpgsqlConnectionStringChecker();
            options.ConnectionStringCheckers[AbpProCoreConsts.DatabaseProviderNames.Sqlite] = new SqliteConnectionStringChecker();
            options.ConnectionStringCheckers[AbpProCoreConsts.DatabaseProviderNames.SqlServer] = new SqlServerConnectionStringChecker();
        });
    }

}
