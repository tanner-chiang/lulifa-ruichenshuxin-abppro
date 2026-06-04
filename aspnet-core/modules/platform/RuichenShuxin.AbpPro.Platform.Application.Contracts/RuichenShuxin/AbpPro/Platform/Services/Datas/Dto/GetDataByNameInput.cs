namespace RuichenShuxin.AbpPro.Platform;

public class GetDataByNameInput
{
    [Required]
    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]
    public string Name { get; set; }
}
