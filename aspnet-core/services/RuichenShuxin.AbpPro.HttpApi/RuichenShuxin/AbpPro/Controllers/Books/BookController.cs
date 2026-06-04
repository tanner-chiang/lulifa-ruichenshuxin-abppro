namespace RuichenShuxin.AbpPro;

/// <summary>
/// 书籍管理
/// </summary>
[Route($"api/business/books")]
public class BookController : AbpProController<
    IBookAppService,
    BookDto,
    BookGetListInput,
    CreateBookDto,
    UpdateBookDto>
{
    public BookController(IBookAppService appService) : base(appService)
    {
    }

    [HttpGet]
    [Route("lookup")]
    public virtual Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
    {
        return AppService.GetAuthorLookupAsync();
    }

    [HttpGet]
    [Route("entity")]
    public virtual Task<EntityTypeInfoModel> GetEntityRuleAsync(EntityTypeInfoGetModel input)
    {
        return AppService.GetEntityRuleAsync(input);
    }
}
