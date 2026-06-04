namespace RuichenShuxin.AbpPro.OAuth;

[ExposeServices(typeof(LoginModel))]
public class AbpProLoginModel : LoginModel
{
    private static readonly Dictionary<string, string> _providerFeaturesMap = new Dictionary<string, string>
    {
        [GitHubAuthenticationDefaults.AuthenticationScheme] = AbpProOAuthFeatureNames.GitHub.Enable,
        [GiteeAuthenticationDefaults.AuthenticationScheme] = AbpProOAuthFeatureNames.Gitee.Enable,
        [QQAuthenticationDefaults.AuthenticationScheme] = AbpProOAuthFeatureNames.QQ.Enable,
        [WeixinAuthenticationDefaults.AuthenticationScheme] = AbpProOAuthFeatureNames.WeChat.Enable,
        [WorkWeixinAuthenticationDefaults.AuthenticationScheme] = AbpProOAuthFeatureNames.WeCom.Enable,
        [BilibiliAuthenticationDefaults.AuthenticationScheme] = AbpProOAuthFeatureNames.Bilibili.Enable,
    };
    private readonly IFeatureChecker _featureChecker;

    public AbpProLoginModel(
        IFeatureChecker featureChecker,
        IAuthenticationSchemeProvider schemeProvider,
        IOptions<AbpAccountOptions> accountOptions,
        IOptions<IdentityOptions> identityOptions,
        IdentityDynamicClaimsPrincipalContributorCache identityDynamicClaimsPrincipalContributorCache,
        IWebHostEnvironment webHostEnvironment)
        : base(schemeProvider, accountOptions, identityOptions, identityDynamicClaimsPrincipalContributorCache, webHostEnvironment)
    {
        _featureChecker = featureChecker;
    }

    public override async Task<IActionResult> OnGetAsync()
    {
        var returnUrl = Request.Query["ReturnUrl"].FirstOrDefault();

        returnUrl = Uri.UnescapeDataString(returnUrl ?? "");

        if (QueryHelpers.ParseQuery(returnUrl).TryGetValue("provider", out var provider))
        {
            var providers = await GetExternalProviders();

            if (providers.Any(p => p.AuthenticationScheme.Equals(provider, StringComparison.OrdinalIgnoreCase)))
            {
                return await OnPostExternalLogin(provider);
            }
        }
        return await base.OnGetAsync();
    }

    protected async override Task<List<ExternalProviderModel>> GetExternalProviders()
    {
        var enabledProviders = new List<ExternalProviderModel>();

        var providers = await base.GetExternalProviders();

        foreach (var provider in providers)
        {
            if (_providerFeaturesMap.TryGetValue(provider.AuthenticationScheme, out var providerFeature))
            {
                if (await _featureChecker.IsEnabledAsync(providerFeature))
                {
                    enabledProviders.Add(provider);
                }
            }
        }

        return enabledProviders;

    }
}
