namespace RuichenShuxin.AbpPro.Core;

public class OracleConnectionStringChecker : IDataBaseConnectionStringChecker, ITransientDependency
{
    public virtual async Task<DataBaseConnectionStringCheckResult> CheckAsync(string connectionString)
    {
        var result = new DataBaseConnectionStringCheckResult();

        try
        {
            var connString = new OracleConnectionStringBuilder(connectionString)
            {
                ConnectionTimeout = 1
            };

            await using var conn = new OracleConnection(connString.ConnectionString);
            await conn.OpenAsync();
            result.Connected = true;
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
