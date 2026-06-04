namespace RuichenShuxin.AbpPro.DataProtectionManagement;

public interface IEntityTypeInfoRepository : IBasicRepository<EntityTypeInfo, Guid>
{
    Task<EntityTypeInfo> FindByTypeAsync(
        string typeFullName,
        CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(
        ISpecification<EntityTypeInfo> specification,
        CancellationToken cancellationToken = default);

    Task<List<EntityTypeInfo>> GetListAsync(
        ISpecification<EntityTypeInfo> specification,
        string sorting = nameof(EntityTypeInfo.Id),
        int maxResultCount = 10,
        int skipCount = 0,
        CancellationToken cancellationToken = default);
}
