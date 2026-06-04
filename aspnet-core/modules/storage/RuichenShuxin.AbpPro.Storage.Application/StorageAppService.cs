using RuichenShuxin.AbpPro.Storage.Localization;
using Volo.Abp.Application.Services;

namespace RuichenShuxin.AbpPro.Storage;

public abstract class StorageAppService : ApplicationService
{
    protected StorageAppService()
    {
        LocalizationResource = typeof(StorageResource);
        ObjectMapperContext = typeof(StorageApplicationModule);
    }
}
