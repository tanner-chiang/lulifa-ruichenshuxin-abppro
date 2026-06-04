namespace RuichenShuxin.AbpPro.DataProtection;

public interface ICurrentDataAccessAccessor
{
    DataAccessOperation[] Current { get; set; }
}
