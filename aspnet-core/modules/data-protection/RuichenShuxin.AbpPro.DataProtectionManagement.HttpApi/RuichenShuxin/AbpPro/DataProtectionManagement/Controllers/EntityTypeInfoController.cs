namespace RuichenShuxin.AbpPro.DataProtectionManagement;

[Route($"api/{DataProtectionManagementRemoteServiceConsts.ModuleName}/entity-type-infos")]
public class EntityTypeInfoController : DataProtectionManagementController, IEntityTypeInfoAppService
{
    private readonly IEntityTypeInfoAppService _service;

    public EntityTypeInfoController(IEntityTypeInfoAppService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<EntityTypeInfoDto> GetAsync(Guid id)
    {
        return _service.GetAsync(id);
    }

    [HttpGet]
    public virtual Task<PagedResultDto<EntityTypeInfoDto>> GetListAsync(GetEntityTypeInfoListInput input)
    {
        return _service.GetListAsync(input);
    }
}
