namespace RuichenShuxin.AbpPro;
public interface IBookAppService :
    ICrudAppService<
        BookDto,
        Guid,
        BookGetListInput,
        CreateBookDto,
        UpdateBookDto>
{
    Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();
    /// <summary>
    /// 获取实体可访问规则
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<EntityTypeInfoModel> GetEntityRuleAsync(EntityTypeInfoGetModel input);
}