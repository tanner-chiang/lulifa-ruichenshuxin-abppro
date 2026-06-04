namespace RuichenShuxin.AbpPro.DataProtectionManagement;

[DependsOn(typeof(AbpDddDomainSharedModule))]
[DependsOn(typeof(AbpProDataProtectionAbstractionsModule))]
public class DataProtectionManagementDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DataProtectionManagementDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DataProtectionResource>()
                .AddVirtualJson("/RuichenShuxin/AbpPro/DataProtectionManagement/Localization/Resources");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace(DataProtectionManagementErrorCodes.Namespace, typeof(DataProtectionResource));
        });
    }
}
