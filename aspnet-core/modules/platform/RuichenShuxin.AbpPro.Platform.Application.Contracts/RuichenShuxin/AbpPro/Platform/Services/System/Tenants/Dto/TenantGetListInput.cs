namespace RuichenShuxin.AbpPro.Platform;

public class TenantGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}