namespace RuichenShuxin.AbpPro.Platform;

public class DataCreateOrUpdateDto
{
    [Required]
    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]
    public string Name { get; set; }

    [Required]
    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength128))]
    public string DisplayName { get; set; }

    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength1024))]
    public string Description { get; set; }
}
