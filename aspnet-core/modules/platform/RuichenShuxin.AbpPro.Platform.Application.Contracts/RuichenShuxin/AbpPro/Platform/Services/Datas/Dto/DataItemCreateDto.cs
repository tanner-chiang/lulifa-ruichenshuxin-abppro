namespace RuichenShuxin.AbpPro.Platform;

public class DataItemCreateDto : DataItemCreateOrUpdateDto
{
    [Required]
    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]
    public string Name { get; set; }
}
