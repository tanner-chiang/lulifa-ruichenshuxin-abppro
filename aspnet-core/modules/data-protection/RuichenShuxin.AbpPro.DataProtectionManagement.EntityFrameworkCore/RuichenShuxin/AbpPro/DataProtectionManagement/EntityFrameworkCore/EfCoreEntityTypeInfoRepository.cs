namespace RuichenShuxin.AbpPro.DataProtectionManagement.EntityFrameworkCore;

public class EfCoreEntityTypeInfoRepository : EfCoreRepository<IDataProtectionManagementDbContext, EntityTypeInfo, Guid>, IEntityTypeInfoRepository
{
    public EfCoreEntityTypeInfoRepository(
        IDbContextProvider<IDataProtectionManagementDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }

    public async virtual Task<EntityTypeInfo> FindByTypeAsync(
        string typeFullName, 
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(x => x.TypeFullName == typeFullName)
            .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<int> GetCountAsync(
        ISpecification<EntityTypeInfo> specification, 
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(specification.ToExpression())
            .CountAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<List<EntityTypeInfo>> GetListAsync(
        ISpecification<EntityTypeInfo> specification,
        string sorting = nameof(EntityTypeInfo.Id),
        int maxResultCount = 10,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(specification.ToExpression())
            .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(EntityTypeInfo.Id) : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async override Task<IQueryable<EntityTypeInfo>> WithDetailsAsync()
    {
        return (await base.WithDetailsAsync())
            .Include(x => x.Properties)
            .ThenInclude(x => x.Enums);
    }
}
