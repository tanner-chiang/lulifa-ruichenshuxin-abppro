namespace RuichenShuxin.AbpPro.CAP;

/// <summary>
/// [DisableConventionalRegistration] 表示 ABP 框架不会自动把它注册为依赖注入服务，你可能会自己手动注册或者在特定场景下使用。
/// </summary>
[DisableConventionalRegistration]
public class FailedThresholdCallbackNotifier : IFailedThresholdCallbackNotifier
{
    protected PlatformCapOptions Options { get; }
    protected IExceptionNotifier ExceptionNotifier { get; }

    public FailedThresholdCallbackNotifier(
        IOptions<PlatformCapOptions> options,
        IExceptionNotifier exceptionNotifier)
    {
        Options = options.Value;
        ExceptionNotifier = exceptionNotifier;
    }

    public async virtual Task NotifyAsync(AbpProCAPExecutionFailedException exception)
    {
        // 通过额外的选项来控制是否发送消息处理失败的事件
        if (Options.EventBus.NotifyFailedCallback)
        {
            await ExceptionNotifier.NotifyAsync(exception);
        }
    }
}
