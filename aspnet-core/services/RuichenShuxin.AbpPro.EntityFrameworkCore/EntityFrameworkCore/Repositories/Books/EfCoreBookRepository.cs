namespace RuichenShuxin.AbpPro.EntityFrameworkCore;

public class EfCoreBookRepository : EfCoreDataProtectionRepository<AbpProDbContext, Book, Guid, BookAuth>, IBookRepository
{
    public EfCoreBookRepository(
        [NotNull] IDbContextProvider<AbpProDbContext> dbContextProvider,
        [NotNull] IDataAuthorizationService dataAuthorizationService,
        [NotNull] IEntityTypeFilterBuilder entityTypeFilterBuilder,
        [NotNull] IEntityPropertyResultBuilder entityPropertyResultBuilder)
        : base(dbContextProvider, dataAuthorizationService, entityTypeFilterBuilder, entityPropertyResultBuilder)
    {
    }
}
