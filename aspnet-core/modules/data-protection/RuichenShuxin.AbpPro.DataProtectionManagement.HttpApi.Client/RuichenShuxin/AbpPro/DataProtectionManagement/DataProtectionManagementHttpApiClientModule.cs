namespace RuichenShuxin.AbpPro.DataProtectionManagement;

[DependsOn(
    typeof(DataProtectionManagementApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DataProtectionManagementHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DataProtectionManagementApplicationContractsModule).Assembly,
            DataProtectionManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DataProtectionManagementHttpApiClientModule>();
        });

    }
}
