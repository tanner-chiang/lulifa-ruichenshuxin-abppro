namespace RuichenShuxin.AbpPro.Platform;

public abstract class TenantCreateOrUpdateBase : ExtensibleObject
{
    [Required]
    [DynamicStringLength(typeof(TenantConsts), nameof(TenantConsts.MaxNameLength))]

    public string Name { get; set; }

}
