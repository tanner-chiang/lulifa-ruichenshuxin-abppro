namespace RuichenShuxin.AbpPro.DataProtection.EntityFrameworkCore;

public class AbpProDataProtectedWriteEntityInterceptor : SaveChangesInterceptor, ITransientDependency
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; } = default!;
    public IOptions<AbpProDataProtectionOptions> DataProtectionOptions => LazyServiceProvider.LazyGetRequiredService<IOptions<AbpProDataProtectionOptions>>();
    public ICurrentUser CurrentUser => LazyServiceProvider.LazyGetRequiredService<ICurrentUser>();
    public IDataFilter DataFilter => LazyServiceProvider.LazyGetRequiredService<IDataFilter>();
    public IDataAuthorizationService DataAuthorizationService => LazyServiceProvider.LazyGetRequiredService<IDataAuthorizationService>();

    public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (DataFilter.IsEnabled<IDataProtected>() && eventData.Context != null)
        {
            var updateEntites = eventData.Context.ChangeTracker.Entries()
                .Where(entry => entry.State.IsIn(EntityState.Modified) && (entry.Entity is not ISoftDelete || entry.Entity is ISoftDelete delete && delete.IsDeleted == false))
                .Select(entry => entry.Entity as IEntity);
            if (updateEntites.Any())
            {
                var updateGrant = await DataAuthorizationService.AuthorizeAsync(DataAccessOperation.Write, updateEntites);
                if (!updateGrant.Succeeded)
                {
                    var entityKeys = updateEntites
                        .Select(entity => (entity is IEntity abpEntity ? abpEntity.GetKeys() : new string[1] { entity.ToString() }).ToString())
                        .JoinAsString(";");
                    throw new AbpProDataAccessDeniedException(
                        $"Delete data permission not granted to entity {updateEntites.First().GetType()} for data {entityKeys}!");
                }
            }
            
            var deleteEntites = eventData.Context.ChangeTracker.Entries()
                .Where(entry => entry.State.IsIn(EntityState.Deleted) || entry.Entity is ISoftDelete delete && delete.IsDeleted == true)
                .Select(entry => entry.Entity as IEntity);
            if (deleteEntites.Any())
            {
                var deleteGrant = await DataAuthorizationService.AuthorizeAsync(DataAccessOperation.Delete, deleteEntites);
                if (!deleteGrant.Succeeded)
                {
                    var entityKeys = deleteEntites
                        .Select(entity => (entity is IEntity abpEntity ? abpEntity.GetKeys() : new string[1] { entity.ToString() }).ToString())
                        .JoinAsString(";");
                    throw new AbpProDataAccessDeniedException(
                        $"Delete data permission not granted to entity {deleteEntites.First().GetType()} for data {entityKeys}!");
                }
            }
        }
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
