namespace RuichenShuxin.AbpPro;

public interface IAuthorAppService : ICrudAppService<
        AuthorDto,
        Guid,
        GetAuthorListDto,
        CreateAuthorDto,
        UpdateAuthorDto>
{

}
