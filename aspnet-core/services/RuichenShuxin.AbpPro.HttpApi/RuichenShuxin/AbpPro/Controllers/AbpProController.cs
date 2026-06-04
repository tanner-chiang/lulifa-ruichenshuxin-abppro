namespace RuichenShuxin.AbpPro;

/// <summary>
/// 通用 Controller 基类（所有 API 控制器都继承这个）
/// </summary>
public abstract class AbpProController : AbpProControllerBase<AbpProResource>
{
}

/// <summary>
/// 通用 CRUD Controller（支持任意主键类型）
/// </summary>
public abstract class AbpProController<
    TAppService,
    TEntityDto,
    TKey,
    TGetListInput,
    TCreateInput,
    TUpdateInput>
    : AbpProCoreCrudControllerBase<
        TAppService,
        TEntityDto,
        TKey,
        TGetListInput,
        TCreateInput,
        TUpdateInput,
        AbpProResource>
    where TAppService : class, ICrudAppService<TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
{
    protected AbpProController(TAppService appService)
        : base(appService)
    {
    }
}

/// <summary>
/// 默认 Guid 主键的通用 CRUD Controller
/// </summary>
public abstract class AbpProController<
    TAppService,
    TEntityDto,
    TGetListInput,
    TCreateInput,
    TUpdateInput>
    : AbpProCoreCrudControllerBaseWithGuid<
        TAppService,
        TEntityDto,
        TGetListInput,
        TCreateInput,
        TUpdateInput,
        AbpProResource>
    where TAppService : class, ICrudAppService<TEntityDto, Guid, TGetListInput, TCreateInput, TUpdateInput>
{
    protected AbpProController(TAppService appService)
        : base(appService)
    {
    }
}
