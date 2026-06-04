namespace RuichenShuxin.AbpPro.Platform;

public class RoleMenuStartupInput
{
    [Required]
    [StringLength(80)]
    public string RoleName { get; set; }

    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]
    public string Framework { get; set; }
}
