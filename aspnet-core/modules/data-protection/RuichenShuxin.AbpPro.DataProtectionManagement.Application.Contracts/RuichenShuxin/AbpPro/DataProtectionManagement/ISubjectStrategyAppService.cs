namespace RuichenShuxin.AbpPro.DataProtectionManagement;

public interface ISubjectStrategyAppService : IApplicationService
{
    Task<SubjectStrategyDto> GetAsync(SubjectStrategyGetInput input);

    Task<SubjectStrategyDto> SetAsync(SubjectStrategySetInput input);
}
