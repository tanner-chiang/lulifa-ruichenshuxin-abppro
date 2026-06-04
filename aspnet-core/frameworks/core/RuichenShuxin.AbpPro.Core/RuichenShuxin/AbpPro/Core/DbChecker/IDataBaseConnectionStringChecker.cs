namespace RuichenShuxin.AbpPro.Core;

public interface IDataBaseConnectionStringChecker
{
    Task<DataBaseConnectionStringCheckResult> CheckAsync(string connectionString);

}
