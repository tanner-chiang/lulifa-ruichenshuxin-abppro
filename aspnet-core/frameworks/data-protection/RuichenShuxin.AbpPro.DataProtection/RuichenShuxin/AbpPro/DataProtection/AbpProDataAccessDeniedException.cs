namespace RuichenShuxin.AbpPro.DataProtection;

public class AbpProDataAccessDeniedException : BusinessException
{
    public AbpProDataAccessDeniedException()
    {
    }

    public AbpProDataAccessDeniedException(string message)
        : base("DataProtection:010001", message)
    {
    }
}
