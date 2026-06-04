namespace RuichenShuxin.AbpPro.Platform;

public class TenantConnectionGetByNameInput
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [DynamicStringLength(typeof(TenantConnectionStringConsts), nameof(TenantConnectionStringConsts.MaxNameLength))]
    public string Name { get; set; }
}
