namespace RuichenShuxin.AbpPro.Core;

public class AbpProCultureProvider : AcceptLanguageHeaderRequestCultureProvider
{
    public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    {
        var result = await base.DetermineProviderCultureResult(httpContext);

        if (result?.Cultures.FirstOrDefault().Value?.StartsWith("zh-", StringComparison.OrdinalIgnoreCase) == true)
        {
            return new ProviderCultureResult(AbpProCoreConsts.Languages.ZhHans);
        }
        return result;

    }
}
