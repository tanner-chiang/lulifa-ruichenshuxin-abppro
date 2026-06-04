namespace RuichenShuxin.AbpPro.Platform;

/// <summary>
/// 布局管理
/// </summary>
[Route("api/platform/layouts")]
public class LayoutController : PlatformController<
    ILayoutAppService,
    LayoutDto,
    GetLayoutListInput,
    LayoutCreateDto,
    LayoutUpdateDto>
{
    public LayoutController(ILayoutAppService appService) : base(appService)
    {
    }

    [HttpGet]
    [Route("all")]
    public async virtual Task<ListResultDto<LayoutDto>> GetAllListAsync()
    {
        return await AppService.GetAllListAsync();
    }

}
