namespace RuichenShuxin.AbpPro.Platform;

public class MenuGetListInput : PagedAndSortedResultRequestDto
{
    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]
    public string Framework { get; set; }

    public string Filter { get; set; }

    public Guid? ParentId { get; set; }

    public Guid? LayoutId { get; set; }
}
