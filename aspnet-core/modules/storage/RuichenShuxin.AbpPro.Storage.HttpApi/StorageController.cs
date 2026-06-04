using RuichenShuxin.AbpPro.Storage.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace RuichenShuxin.AbpPro.Storage;

public abstract class StorageController : AbpControllerBase
{
    protected StorageController()
    {
        LocalizationResource = typeof(StorageResource);
    }
}
