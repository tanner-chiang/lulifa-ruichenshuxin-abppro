namespace RuichenShuxin.AbpPro.DataProtectionManagement;

public interface ISubjectStrategyRepository : IBasicRepository<SubjectStrategy, Guid>
{
    Task<SubjectStrategy> FindBySubjectAsync(
        string subjectName,
        string subjectId,
        CancellationToken cancellationToken = default);
}
