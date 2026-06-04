namespace RuichenShuxin.AbpPro.Platform;

public static class PlatformConsts
{

    public static int MaxLength64 { get; set; } = AbpProCoreConsts.MaxLength64;

    public static int MaxLength128 { get; set; } = AbpProCoreConsts.MaxLength128;

    public static int MaxLength256 { get; set; } = AbpProCoreConsts.MaxLength256;

    public static int MaxLength512 { get; set; } = AbpProCoreConsts.MaxLength512;

    public static int MaxLength1024 { get; set; } = AbpProCoreConsts.MaxLength1024;

    /// <summary>
    /// 编号不足位补足字符，如编号为 3，长度5位，补足字符为 0，则编号为00003
    /// </summary>
    public static char CodePrefix { get; set; } = '0';
    /// <summary>
    /// 编号长度
    /// </summary>
    public const int CodeUnitLength = 5;

    /// <summary>
    /// 最大深度
    /// </summary>
    /// <remarks>
    /// 默认为4,仅支持四级子菜单
    /// </remarks>
    public const int MaxDepth = 4;

    public const int MaxCodeLength = MaxDepth * (CodeUnitLength + 1) - 1;

}
