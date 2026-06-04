namespace RuichenShuxin.AbpPro.Core;

public interface IHasRedisOptions
{
    RedisConnectionOptions Redis { get; set; }
}
