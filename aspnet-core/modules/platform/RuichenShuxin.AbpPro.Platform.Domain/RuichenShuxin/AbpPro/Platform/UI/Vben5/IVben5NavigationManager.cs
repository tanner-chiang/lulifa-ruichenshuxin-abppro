namespace RuichenShuxin.AbpPro.Platform;

public interface IVben5NavigationManager
{
    Task<IReadOnlyCollection<ApplicationMenu>> GetAll();
}
