namespace RuichenShuxin.AbpPro.Core;

public class SqlServerConnectionStringChecker : IDataBaseConnectionStringChecker, ITransientDependency
{
    public virtual async Task<DataBaseConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new DataBaseConnectionStringCheckResult();

        try
        {
            var connString = new SqlConnectionStringBuilder(connectionString)
            {
                ConnectTimeout = 1
            };

            var oldDatabaseName = connString.InitialCatalog;
            connString.InitialCatalog = "master";

            await using var conn = new SqlConnection(connString.ConnectionString);
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
