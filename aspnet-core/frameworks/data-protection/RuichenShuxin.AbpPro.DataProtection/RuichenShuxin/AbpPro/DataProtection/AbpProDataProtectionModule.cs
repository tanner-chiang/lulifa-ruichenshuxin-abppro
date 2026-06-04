namespace RuichenShuxin.AbpPro.DataProtection;

[DependsOn(
    typeof(AbpProDataProtectionAbstractionsModule),
    typeof(AbpDddDomainModule))]
public class AbpProDataProtectionModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(DataProtectedInterceptorRegistrar.RegisterIfNeeded);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpProDataProtectionOptions>(options =>
        {
            // 当前用户数据过滤
            options.KeywordContributors.Add(DataAccessCurrentUserContributor.Name, new DataAccessCurrentUserContributor());

            options.OperateContributors.Add(DataAccessFilterOperate.Equal, new DataAccessEqualContributor());
            options.OperateContributors.Add(DataAccessFilterOperate.NotEqual, new DataAccessNotEqualContributor());
            options.OperateContributors.Add(DataAccessFilterOperate.Less, new DataAccessLessContributor());
            options.OperateContributors.Add(DataAccessFilterOperate.LessOrEqual, new DataAccessLessOrEqualContributor());
            options.OperateContributors.Add(DataAccessFilterOperate.Greater, new DataAccessGreaterContributor());
            options.OperateContributors.Add(DataAccessFilterOperate.GreaterOrEqual, new DataAccessGreaterOrEqualContributor());
            options.OperateContributors.Add(DataAccessFilterOperate.StartsWith, new DataAccessStartsWithContributor());
            options.OperateContributors.Add(DataAccessFilterOperate.EndsWith, new DataAccessEndsWithContributor());
            options.OperateContributors.Add(DataAccessFilterOperate.Contains, new DataAccessContainsContributor());
            options.OperateContributors.Add(DataAccessFilterOperate.NotContains, new DataAccessNotContainsContributor());

            options.SubjectContributors.Add(new DataAccessClientIdContributor());
            options.SubjectContributors.Add(new DataAccessUserIdContributor());
            options.SubjectContributors.Add(new DataAccessRoleNameContributor());
            options.SubjectContributors.Add(new DataAccessOrganizationUnitContributor());

            // 权限策略提供程序
            options.StrategyContributors.Add(new DataAccessStrategyRoleNameContributor());
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("DataProtection", typeof(DataProtectionResource));
        });
    }
}
