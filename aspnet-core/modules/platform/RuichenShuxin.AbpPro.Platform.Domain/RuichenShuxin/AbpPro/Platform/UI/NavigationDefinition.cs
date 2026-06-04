namespace RuichenShuxin.AbpPro.Platform;

public class NavigationDefinition
{
    public ApplicationMenu Menu { get; }
    public NavigationDefinition(ApplicationMenu menu)
    {
        Menu = menu;
    }
}
