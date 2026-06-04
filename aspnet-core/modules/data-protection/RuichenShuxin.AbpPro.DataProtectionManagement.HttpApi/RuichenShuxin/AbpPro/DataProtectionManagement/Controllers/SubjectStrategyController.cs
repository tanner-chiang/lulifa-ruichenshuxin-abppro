namespace RuichenShuxin.AbpPro.DataProtectionManagement;

[Route($"api/{DataProtectionManagementRemoteServiceConsts.ModuleName}/subject-strategys")]
public class SubjectStrategyController : DataProtectionManagementController, ISubjectStrategyAppService
{
    private readonly ISubjectStrategyAppService _service;
    public SubjectStrategyController(ISubjectStrategyAppService service)
    {
        _service = service;
    }

    [HttpGet]
    public virtual Task<SubjectStrategyDto> GetAsync(SubjectStrategyGetInput input)
    {
        return _service.GetAsync(input);
    }

    [HttpPut]
    [Authorize(DataProtectionManagementPermissionNames.SubjectStrategy.Change)]
    public virtual Task<SubjectStrategyDto> SetAsync(SubjectStrategySetInput input)
    {
        return _service.SetAsync(input);
    }
}
