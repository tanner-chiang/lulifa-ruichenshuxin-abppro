namespace RuichenShuxin.AbpPro.Core;

public class MySqlConnectionStringChecker : IDataBaseConnectionStringChecker, ITransientDependency
{
    public virtual async Task<DataBaseConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new DataBaseConnectionStringCheckResult();

        try
        {
            var connString = new MySqlConnectionStringBuilder(connectionString)
            {
                ConnectionLifeTime = 1
            };

            var oldDatabaseName = connString.Database;
            connString.Database = AbpProCoreConsts.DatabaseProviderNames.MySql;

            await using var conn = new MySqlConnection(connString.ConnectionString);
            await conn.OpenAsync();
            result.Connected = true;
            await conn.ChangeDatabaseAsync(oldDatabaseName);
            result.DatabaseExists = true;

            await conn.CloseAsync();

            return result;
        }
        catch (Exception e)
        {
            result.Error = e;
            return result;
        }
    }
}
