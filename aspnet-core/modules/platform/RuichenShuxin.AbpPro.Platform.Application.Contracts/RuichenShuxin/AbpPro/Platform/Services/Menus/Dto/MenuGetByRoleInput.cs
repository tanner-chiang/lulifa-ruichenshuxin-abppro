namespace RuichenShuxin.AbpPro.Platform;

public class MenuGetByRoleInput
{
    [Required]
    [StringLength(80)]
    public string Role { get; set; }

    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]
    public string Framework { get; set; }
}
