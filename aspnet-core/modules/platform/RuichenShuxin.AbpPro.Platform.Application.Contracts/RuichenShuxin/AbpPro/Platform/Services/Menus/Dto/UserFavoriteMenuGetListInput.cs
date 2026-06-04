namespace RuichenShuxin.AbpPro.Platform;
public class UserFavoriteMenuGetListInput
{
    [DynamicStringLength(typeof(PlatformConsts), nameof(PlatformConsts.MaxLength64))]
    public string Framework { get; set; }
}
