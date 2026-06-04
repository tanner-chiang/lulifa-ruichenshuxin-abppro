namespace RuichenShuxin.AbpPro.Platform;

public class MenuGetByUserInput
{
    [Required]
    public Guid UserId { get; set; }

    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]
    public string Framework { get; set; }
}
