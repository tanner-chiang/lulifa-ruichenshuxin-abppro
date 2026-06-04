namespace RuichenShuxin.AbpPro.DataProtectionManagement.EntityFrameworkCore;

public class EfCoreOrganizationUnitEntityRuleRepository : EfCoreRepository<IDataProtectionManagementDbContext, OrganizationUnitEntityRule, Guid>,
    IOrganizationUnitEntityRuleRepository
{
    public EfCoreOrganizationUnitEntityRuleRepository(
        IDbContextProvider<IDataProtectionManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async virtual Task<OrganizationUnitEntityRule> FindEntityRuleAsync(
        string orgCode,
        string entityTypeFullName, 
        DataAccessOperation operation = DataAccessOperation.Read, 
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(x => x.OrgCode == orgCode && x.EntityTypeFullName == entityTypeFullName && x.Operation == operation)
            .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<List<OrganizationUnitEntityRule>> GetListByEntityAsync(
        string entityTypeFullName,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(x => x.EntityTypeFullName == entityTypeFullName)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<int> GetCountAsync(
        ISpecification<OrganizationUnitEntityRule> specification, 
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(specification.ToExpression())
            .CountAsync(GetCancellationToken(cancellationToken));
    }

    public async virtual Task<List<OrganizationUnitEntityRule>> GetCountAsync(
    ISpecification<OrganizationUnitEntityRule> specification, 
        string sorting = nameof(OrganizationUnitEntityRule.EntityTypeFullName), 
        int maxResultCount = 10, 
        int skipCount = 0, 
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(specification.ToExpression())
            .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(OrganizationUnitEntityRule.EntityTypeFullName) : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}
