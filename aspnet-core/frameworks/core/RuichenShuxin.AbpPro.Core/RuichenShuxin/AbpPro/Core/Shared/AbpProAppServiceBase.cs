namespace RuichenShuxin.AbpPro.Core;

public abstract class AbpProAppServiceBase<TResource, TModule> : ApplicationService
    where TResource : class
    where TModule : class
{
    protected AbpProAppServiceBase()
    {
        LocalizationResource = typeof(TResource);
        ObjectMapperContext = typeof(TModule);
    }
}
