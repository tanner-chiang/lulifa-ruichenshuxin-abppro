namespace RuichenShuxin.AbpPro.DataProtectionManagement.EntityFrameworkCore;

public class EfCoreRoleEntityRuleRepository : EfCoreRepository<IDataProtectionManagementDbContext, RoleEntityRule, Guid>,
    IRoleEntityRuleRepository
{
    public EfCoreRoleEntityRuleRepository(
        IDbContextProvider<IDataProtectionManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async virtual Task<RoleEntityRule> FindEntityRuleAsync(
        string roleName, 
        string entityTypeFullName, 
        DataAccessOperation operation = DataAccessOperation.Read, 
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(x => x.RoleName == roleName && x.EntityTypeFullName == entityTypeFullName && x.Operation == operation)
            .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<List<RoleEntityRule>> GetListByEntityAsync(
        string entityTypeFullName,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(x => x.EntityTypeFullName == entityTypeFullName)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<int> GetCountAsync(
        ISpecification<RoleEntityRule> specification, 
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(specification.ToExpression())
            .CountAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<List<RoleEntityRule>> GetCountAsync(
    ISpecification<RoleEntityRule> specification, 
        string sorting = nameof(RoleEntityRule.EntityTypeFullName), 
        int maxResultCount = 10, 
        int skipCount = 0, 
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(specification.ToExpression())
            .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(RoleEntityRule.EntityTypeFullName) : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}
