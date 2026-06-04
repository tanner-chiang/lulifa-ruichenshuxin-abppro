namespace RuichenShuxin.AbpPro.DataProtectionManagement.EntityFrameworkCore;

public class EfCoreSubjectStrategyRepository : EfCoreRepository<IDataProtectionManagementDbContext, SubjectStrategy, Guid>, ISubjectStrategyRepository
{
    public EfCoreSubjectStrategyRepository(
        IDbContextProvider<IDataProtectionManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async virtual Task<SubjectStrategy> FindBySubjectAsync(
        string subjectName,
        string subjectId,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(x => x.SubjectName == subjectName && x.SubjectId == subjectId)
            .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
    }
}
