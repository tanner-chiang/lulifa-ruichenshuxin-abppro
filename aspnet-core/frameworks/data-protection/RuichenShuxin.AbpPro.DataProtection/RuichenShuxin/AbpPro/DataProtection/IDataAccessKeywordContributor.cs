namespace RuichenShuxin.AbpPro.DataProtection;

public interface IDataAccessKeywordContributor
{
    bool IsExternal { get; }

    string Keyword { get; }

    Expression Contribute(DataAccessKeywordContributorContext context);
}
