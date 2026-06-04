namespace RuichenShuxin.AbpPro.Platform;

public class GetLayoutListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }

    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]
    public string Framework { get; set; }
}
