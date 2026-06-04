namespace RuichenShuxin.AbpPro.CAP;

public interface IFailedThresholdCallbackNotifier
{
    Task NotifyAsync(AbpProCAPExecutionFailedException exception);
}
