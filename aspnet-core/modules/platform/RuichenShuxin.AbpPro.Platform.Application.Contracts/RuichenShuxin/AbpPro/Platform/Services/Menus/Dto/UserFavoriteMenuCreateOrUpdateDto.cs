namespace RuichenShuxin.AbpPro.Platform;

public abstract class UserFavoriteMenuCreateOrUpdateDto
{
    [Required]
    public Guid MenuId { get; set; }

    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]
    public string Color { get; set; }

    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength128))]
    public string AliasName { get; set; }

    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength512))]
    public string Icon { get; set; }
}
