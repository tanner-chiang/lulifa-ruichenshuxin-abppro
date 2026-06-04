using Volo.Abp.DependencyInjection;

namespace RuichenShuxin.AbpPro.Platform;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IAbpApplicationConfigurationAppService))]
public class SystemApplicationConfigurationAppService : PlatformAppService, IAbpApplicationConfigurationAppService
{
    protected readonly AbpApplicationConfigurationAppService AbpApplicationConfigurationAppService;

    public SystemApplicationConfigurationAppService(AbpApplicationConfigurationAppService abpApplicationConfigurationAppService)
    {
        AbpApplicationConfigurationAppService = abpApplicationConfigurationAppService;
    }


    public virtual async Task<ApplicationConfigurationDto> GetAsync(ApplicationConfigurationRequestOptions options)
    {
        var result = await AbpApplicationConfigurationAppService.GetAsync(options);

        return result;
    }
}
