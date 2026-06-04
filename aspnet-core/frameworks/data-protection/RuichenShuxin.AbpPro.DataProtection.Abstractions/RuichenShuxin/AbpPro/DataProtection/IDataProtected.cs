namespace RuichenShuxin.AbpPro.DataProtection;

public interface IDataProtected
{
    Guid? CreatorId { get; }
}