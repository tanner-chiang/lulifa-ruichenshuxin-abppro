namespace RuichenShuxin.AbpPro.Core;

/// <summary>
/// Controller 基类（最顶层，定义资源类型）
/// </summary>
[RemoteService]
public abstract class AbpProControllerBase<TResource> : AbpControllerBase
    where TResource : class
{
    protected AbpProControllerBase()
    {
        LocalizationResource = typeof(TResource);
    }
}

/// <summary>
/// 通用的 CRUD Controller 基类（支持任意主键类型）
/// </summary>
public abstract class AbpProCoreCrudControllerBase<
    TAppService,
    TEntityDto,
    TKey,
    TGetListInput,
    TCreateInput,
    TUpdateInput,
    TResource>
    : AbpProControllerBase<TResource>
    where TAppService : ICrudAppService<TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where TResource : class
{
    protected readonly TAppService AppService;

    protected AbpProCoreCrudControllerBase(TAppService appService)
    {
        AppService = appService;
    }

    [HttpPost]
    public virtual Task<TEntityDto> CreateAsync(TCreateInput input)
        => AppService.CreateAsync(input);
   

    [HttpDelete("{id}")]
    public virtual Task DeleteAsync(TKey id)
        => AppService.DeleteAsync(id);

    [HttpGet("{id}")]
    public virtual Task<TEntityDto> GetAsync(TKey id)
        => AppService.GetAsync(id);


    [HttpPut("{id}")]
    public virtual Task<TEntityDto> UpdateAsync(TKey id, TUpdateInput input)
        => AppService.UpdateAsync(id, input);

    [HttpGet]
    public virtual Task<PagedResultDto<TEntityDto>> GetListAsync(TGetListInput input)
        => AppService.GetListAsync(input);

}

/// <summary>
/// 默认使用 Guid 主键的 CRUD Controller 基类
/// </summary>
public abstract class AbpProCoreCrudControllerBaseWithGuid<
    TAppService,
    TEntityDto,
    TGetListInput,
    TCreateInput,
    TUpdateInput,
    TResource>
    : AbpProCoreCrudControllerBase<
        TAppService,
        TEntityDto,
        Guid,
        TGetListInput,
        TCreateInput,
        TUpdateInput,
        TResource>
    where TAppService : ICrudAppService<TEntityDto, Guid, TGetListInput, TCreateInput, TUpdateInput>
    where TResource : class
{
    protected AbpProCoreCrudControllerBaseWithGuid(TAppService appService)
        : base(appService)
    {
    }
    /// <summary>
    /// 方便访问 AppService 实例（可调用自定义方法）
    /// </summary>
    protected TAppService App => AppService;
}

