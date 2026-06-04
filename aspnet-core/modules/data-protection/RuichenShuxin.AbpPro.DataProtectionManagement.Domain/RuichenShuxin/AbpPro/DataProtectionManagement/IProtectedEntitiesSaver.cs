namespace RuichenShuxin.AbpPro.DataProtectionManagement;

public interface IProtectedEntitiesSaver
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}
