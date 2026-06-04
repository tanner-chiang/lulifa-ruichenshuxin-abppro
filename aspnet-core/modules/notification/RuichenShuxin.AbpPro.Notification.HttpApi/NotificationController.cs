using RuichenShuxin.AbpPro.Notification.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace RuichenShuxin.AbpPro.Notification;

public abstract class NotificationController : AbpControllerBase
{
    protected NotificationController()
    {
        LocalizationResource = typeof(NotificationResource);
    }
}
