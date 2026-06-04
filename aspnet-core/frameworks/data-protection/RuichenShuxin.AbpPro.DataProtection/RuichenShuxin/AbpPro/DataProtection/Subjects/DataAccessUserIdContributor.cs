namespace RuichenShuxin.AbpPro.DataProtection;

public class DataAccessUserIdContributor : IDataAccessSubjectContributor
{
    public string Name => UserPermissionValueProvider.ProviderName;

    public async virtual Task<List<DataAccessFilterGroup>> GetFilterGroups(DataAccessSubjectContributorContext context)
    {
        var groups = new List<DataAccessFilterGroup>();
        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
        if (currentUser.IsAuthenticated)
        {
            var resourceStore = context.ServiceProvider.GetRequiredService<IDataProtectedResourceStore>();
            var resource = await resourceStore.GetAsync(Name, currentUser.Id.ToString(), context.EntityTypeFullName, context.Operation);
            if (resource?.FilterGroup != null)
            {
                groups.Add(resource.FilterGroup);
            }
        }
        return groups;
    }

    public async virtual Task<List<string>> GetAccessdProperties(DataAccessSubjectContributorContext context)
    {
        var allowProperties = new List<string>();
        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
        if (currentUser.IsAuthenticated)
        {
            var resourceStore = context.ServiceProvider.GetRequiredService<IDataProtectedResourceStore>();
            var resource = await resourceStore.GetAsync(Name, currentUser.Id.ToString(), context.EntityTypeFullName, context.Operation);
            if (resource?.AccessedProperties.Any() == true)
            {
                allowProperties.AddIfNotContains(resource.AccessedProperties);
            }
        }
        return allowProperties;
    }
}
