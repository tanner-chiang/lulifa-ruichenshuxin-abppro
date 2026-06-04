namespace RuichenShuxin.AbpPro;

[DependsOn(
     typeof(AbpAutoMapperModule),
     typeof(AbpDddDomainModule),
     typeof(AbpProDomainSharedModule))]
public class AbpProDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources.Get<AbpProResource>()
                .AddBaseTypes(typeof(DataProtectionResource));
        });

        Configure<AbpProDataProtectionOptions>(options =>
        {
            // ������Բ����趨����
            options.EntityIgnoreProperties.Add(typeof(Book), [nameof(Book.AuthorId)]);
        });
    }
}
