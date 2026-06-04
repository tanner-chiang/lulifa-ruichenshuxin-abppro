namespace RuichenShuxin.AbpPro.Platform;

public class UserFavoriteMenuCreateDto : UserFavoriteMenuCreateOrUpdateDto
{
    [Required]
    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]

    public string Framework { get; set; }
}
