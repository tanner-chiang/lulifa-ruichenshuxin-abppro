namespace RuichenShuxin.AbpPro.DataProtection;

public interface IDataAccessScope
{
    DataAccessOperation[] Operations { get; }
    IDisposable BeginScope(DataAccessOperation[] operations = null);
}
