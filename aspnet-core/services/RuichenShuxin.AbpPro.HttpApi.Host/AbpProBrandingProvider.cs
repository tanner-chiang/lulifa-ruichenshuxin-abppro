namespace RuichenShuxin.AbpPro;

[Dependency(ReplaceServices = true)]
public class AbpProBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<AbpProResource> _localizer;

    public AbpProBrandingProvider(IStringLocalizer<AbpProResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
