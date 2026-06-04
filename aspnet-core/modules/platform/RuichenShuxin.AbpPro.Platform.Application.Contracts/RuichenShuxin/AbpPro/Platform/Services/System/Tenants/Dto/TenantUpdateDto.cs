namespace RuichenShuxin.AbpPro.Platform;
public class TenantUpdateDto : TenantCreateOrUpdateBase, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; }
}