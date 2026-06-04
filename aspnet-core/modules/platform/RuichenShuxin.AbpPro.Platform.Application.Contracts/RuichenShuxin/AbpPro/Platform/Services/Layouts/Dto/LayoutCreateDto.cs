namespace RuichenShuxin.AbpPro.Platform;

public class LayoutCreateDto : LayoutCreateOrUpdateDto
{
    public Guid DataId { get; set; }

    [Required]
    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]
    public string Framework { get; set; }
}
