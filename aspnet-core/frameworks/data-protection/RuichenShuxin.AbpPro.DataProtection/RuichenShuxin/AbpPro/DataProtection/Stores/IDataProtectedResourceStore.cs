namespace RuichenShuxin.AbpPro.DataProtection;

public interface IDataProtectedResourceStore
{
    Task SetAsync(DataAccessResource resource);

    Task RemoveAsync(DataAccessResource resource);

    Task<DataAccessResource> GetAsync(string subjectName, string subjectId, string entityTypeFullName, DataAccessOperation operation);
}
