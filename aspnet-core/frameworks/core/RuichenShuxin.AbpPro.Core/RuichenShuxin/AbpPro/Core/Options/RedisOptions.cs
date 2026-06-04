namespace RuichenShuxin.AbpPro.Core;

public class RedisOptions : RedisConnectionOptions, IEnabledOptions
{
    public bool IsEnabled { get; set; }

}
