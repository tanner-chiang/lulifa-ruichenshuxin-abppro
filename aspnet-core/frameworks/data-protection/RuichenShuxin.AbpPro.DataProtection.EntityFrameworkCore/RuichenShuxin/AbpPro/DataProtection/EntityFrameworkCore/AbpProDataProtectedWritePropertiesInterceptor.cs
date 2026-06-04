namespace RuichenShuxin.AbpPro.DataProtection.EntityFrameworkCore;

public class AbpProDataProtectedWritePropertiesInterceptor : SaveChangesInterceptor, ITransientDependency
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; } = default!;
    public IOptions<AbpProDataProtectionOptions> DataProtectionOptions => LazyServiceProvider.LazyGetRequiredService<IOptions<AbpProDataProtectionOptions>>();
    public ICurrentUser CurrentUser => LazyServiceProvider.LazyGetRequiredService<ICurrentUser>();
    public IDataFilter DataFilter => LazyServiceProvider.LazyGetRequiredService<IDataFilter>();

    public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (DataFilter.IsEnabled<IDataProtected>() && eventData.Context != null)
        {
            foreach (var entry in eventData.Context.ChangeTracker.Entries().ToList())
            {
                if (entry.State.IsIn(EntityState.Modified))
                {
                    var allowProperties = new List<string>();
                    var entity = entry.Entity;
                    var entityType = entry.Entity.GetType();
                    var subjectContext = new DataAccessSubjectContributorContext(entityType.FullName, DataAccessOperation.Write, LazyServiceProvider);
                    foreach (var contributor in DataProtectionOptions.Value.SubjectContributors)
                    {
                        var properties = await contributor.GetAccessdProperties(subjectContext);
                        allowProperties.AddIfNotContains(properties);
                    }

                    // 仅配置了字段级控制才生效
                    if (allowProperties.Count != 0)
                    {
                        if (DataProtectionOptions.Value.EntityIgnoreProperties.TryGetValue(entityType, out var entityIgnoreProps))
                        {
                            allowProperties.AddIfNotContains(entityIgnoreProps);
                        }

                        allowProperties.AddIfNotContains(DataProtectionOptions.Value.GlobalIgnoreProperties);

                        foreach (var property in entry.Properties)
                        {
                            if (!allowProperties.Contains(property.Metadata.Name))
                            {
                                property.IsModified = false;
                            }
                        }
                    }
                }
            }
        }
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
