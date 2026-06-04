namespace RuichenShuxin.AbpPro.Platform;

public class GetMenuInput
{
    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]
    public string Framework { get; set; }
}
