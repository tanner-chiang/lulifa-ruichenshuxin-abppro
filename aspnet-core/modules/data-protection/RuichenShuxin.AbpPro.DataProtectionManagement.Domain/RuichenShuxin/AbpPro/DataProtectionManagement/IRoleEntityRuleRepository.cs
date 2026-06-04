namespace RuichenShuxin.AbpPro.DataProtectionManagement;

public interface IRoleEntityRuleRepository : IBasicRepository<RoleEntityRule, Guid>
{
    Task<RoleEntityRule> FindEntityRuleAsync(
        string roleName,
        string entityTypeFullName,
        DataAccessOperation operation = DataAccessOperation.Read,
        CancellationToken cancellationToken = default);

    Task<List<RoleEntityRule>> GetListByEntityAsync(
        string entityTypeFullName,
        CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(
        ISpecification<RoleEntityRule> specification,
        CancellationToken cancellationToken = default);

    Task<List<RoleEntityRule>> GetCountAsync(
        ISpecification<RoleEntityRule> specification,
        string sorting = nameof(RoleEntityRule.EntityTypeFullName),
        int maxResultCount = 10,
        int skipCount = 0,
        CancellationToken cancellationToken = default);
}
