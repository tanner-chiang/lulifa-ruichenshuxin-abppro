namespace RuichenShuxin.AbpPro.DataProtection;

public interface IDataAccessEntityTypeInfoProvider
{
    Task<EntityTypeInfoModel> GetEntitTypeInfoAsync(DataAccessEntitTypeInfoContext context);
}
