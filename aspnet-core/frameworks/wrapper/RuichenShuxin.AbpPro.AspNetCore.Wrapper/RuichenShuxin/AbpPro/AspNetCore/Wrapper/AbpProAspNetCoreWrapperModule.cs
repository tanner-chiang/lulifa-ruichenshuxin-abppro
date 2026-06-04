namespace RuichenShuxin.AbpPro.AspNetCore.Wrapper;

[DependsOn(
    typeof(AbpProWrapperModule),
    typeof(AbpAspNetCoreModule))]
public class AbpProAspNetCoreWrapperModule : AbpModule
{

}
