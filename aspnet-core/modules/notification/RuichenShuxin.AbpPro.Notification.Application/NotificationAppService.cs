using RuichenShuxin.AbpPro.Notification.Localization;
using Volo.Abp.Application.Services;

namespace RuichenShuxin.AbpPro.Notification;

public abstract class NotificationAppService : ApplicationService
{
    protected NotificationAppService()
    {
        LocalizationResource = typeof(NotificationResource);
        ObjectMapperContext = typeof(NotificationApplicationModule);
    }
}
