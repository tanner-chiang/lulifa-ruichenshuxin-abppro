namespace RuichenShuxin.AbpPro.DataProtection;

public class DataAccessClientIdContributor : IDataAccessSubjectContributor
{
    public string Name => ClientPermissionValueProvider.ProviderName;

    public async virtual Task<List<string>> GetAccessdProperties(DataAccessSubjectContributorContext context)
    {
        var allowProperties = new List<string>();
        var currentClient = context.ServiceProvider.GetRequiredService<ICurrentClient>();
        if (currentClient.IsAuthenticated)
        {
            var resourceStore = context.ServiceProvider.GetRequiredService<IDataProtectedResourceStore>();
            var resource = await resourceStore.GetAsync(Name, currentClient.Id, context.EntityTypeFullName, context.Operation);
            if (resource?.AccessedProperties.Any() == true)
            {
                allowProperties.AddIfNotContains(resource.AccessedProperties);
            }
        }
        return allowProperties;
    }

    public async virtual Task<List<DataAccessFilterGroup>> GetFilterGroups(DataAccessSubjectContributorContext context)
    {
        var groups = new List<DataAccessFilterGroup>();
        var currentClient = context.ServiceProvider.GetRequiredService<ICurrentClient>();
        if (currentClient.IsAuthenticated)
        {
            var resourceStore = context.ServiceProvider.GetRequiredService<IDataProtectedResourceStore>();
            var resource = await resourceStore.GetAsync(Name, currentClient.Id, context.EntityTypeFullName, context.Operation);
            if (resource?.FilterGroup != null)
            {
                groups.Add(resource.FilterGroup);
            }
        }
        return groups;
    }
}
