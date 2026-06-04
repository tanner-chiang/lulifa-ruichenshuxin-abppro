namespace RuichenShuxin.AbpPro.DataProtectionManagement;

public class GetEntityTypeInfoListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }

    public bool? IsAuditEnabled { get; set; }
}
