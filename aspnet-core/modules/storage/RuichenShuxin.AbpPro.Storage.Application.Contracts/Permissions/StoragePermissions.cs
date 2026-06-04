using Volo.Abp.Reflection;

namespace RuichenShuxin.AbpPro.Storage.Permissions;

public class StoragePermissions
{
    public const string GroupName = "Storage";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(StoragePermissions));
    }
}
