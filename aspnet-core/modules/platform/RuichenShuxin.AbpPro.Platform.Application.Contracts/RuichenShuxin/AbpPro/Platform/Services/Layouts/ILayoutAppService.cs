namespace RuichenShuxin.AbpPro.Platform;

public interface ILayoutAppService :
    ICrudAppService<
        LayoutDto,
        Guid,
        GetLayoutListInput,
        LayoutCreateDto,
        LayoutUpdateDto>
{
    Task<ListResultDto<LayoutDto>> GetAllListAsync();
}
