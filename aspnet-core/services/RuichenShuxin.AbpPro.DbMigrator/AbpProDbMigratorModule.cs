namespace RuichenShuxin.AbpPro.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpProEntityFrameworkCoreModule),
    typeof(DataProtectionManagementApplicationContractsModule),
    typeof(PlatformApplicationContractsModule),
    typeof(AbpProApplicationContractsModule)
)]
public class AbpProDbMigratorModule : AbpModule
{
}
