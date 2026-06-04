namespace RuichenShuxin.AbpPro;

public class AuthorAlreadyExistsException : BusinessException
{
    public AuthorAlreadyExistsException(string name)
        : base(AbpProDomainErrorCodes.Author.AuthorAlreadyExists)
    {
        WithData("Name", name);
    }
}
