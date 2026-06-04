namespace RuichenShuxin.AbpPro.DataProtectionManagement;

public interface IEntityTypeInfoAppService : IApplicationService
{
    Task<EntityTypeInfoDto> GetAsync(Guid id);

    Task<PagedResultDto<EntityTypeInfoDto>> GetListAsync(GetEntityTypeInfoListInput input);
}
