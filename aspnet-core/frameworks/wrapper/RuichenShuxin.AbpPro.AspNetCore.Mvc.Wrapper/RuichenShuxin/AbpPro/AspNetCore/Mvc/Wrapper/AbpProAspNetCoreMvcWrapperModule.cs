namespace RuichenShuxin.AbpPro.AspNetCore.Mvc.Wrapper;

[DependsOn(
    typeof(AbpProWrapperModule),
    typeof(AbpProAspNetCoreWrapperModule))]
public class AbpProAspNetCoreMvcWrapperModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpProAspNetCoreMvcWrapperModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AbpProMvcWrapperResource>("en")
                .AddVirtualJson("/RuichenShuxin/AbpPro/AspNetCore/Mvc/Wrapper/Localization/Resources");
        });

        Configure<MvcOptions>(mvcOptions =>
        {
            // Wrap Result Filter
            mvcOptions.Filters.AddService(typeof(AbpProWrapResultFilter));
        });

        Configure<AbpProWrapperOptions>(options =>
        {
            // 即使重写端点也不包装返回结果
            // api/abp/api-definition
            // options.IgnoreReturnTypes.Add<ApplicationApiDescriptionModel>();
            // api/abp/application-configuration
            //  options.IgnoreReturnTypes.Add<ApplicationConfigurationDto>();
            // api/abp/application-localization
            // options.IgnoreReturnTypes.Add<ApplicationLocalizationDto>(); 
            // 文件流
            options.IgnoreReturnTypes.Add<IRemoteStreamContent>();
            // options.IgnoreReturnTypes.Add<FileResult>();

            //options.IgnoreReturnTypes.Add<ViewResult>();
            //options.IgnoreReturnTypes.Add<ViewEngineResult>();
            //options.IgnoreReturnTypes.Add<ViewComponentResult>();

            //options.IgnoreReturnTypes.Add<RedirectToActionResult>();
            //options.IgnoreReturnTypes.Add<RedirectToPageResult>();
            //options.IgnoreReturnTypes.Add<RedirectToRouteResult>();

            //options.IgnoreReturnTypes.Add<SignInResult>();
            //options.IgnoreReturnTypes.Add<SignOutResult>();
            //options.IgnoreReturnTypes.Add<ForbidResult>();

            // options.IgnoreControllers.Add<AbpApplicationLocalizationController>();
            // options.IgnoreControllers.Add<AbpApplicationConfigurationController>();

            // Api Endpoints
            options.IgnoreControllers.Add<AbpApiDefinitionController>();
            // Abp/ServiceProxyScript
            options.IgnoreControllers.Add<AbpServiceProxyScriptController>();
            // Application Configuration Script
            options.IgnoreControllers.Add<AbpApplicationConfigurationScriptController>();

            // 官方模块不包装结果
            options.IgnoreNamespaces.Add("Volo.Abp");

            // oidc端点不包装结果
            options.IgnorePrefixUrls.Add("/connect");

            // 返回本地化的 404 错误消息
            options.MessageWithEmptyResult = (serviceProvider) =>
            {
                var localizer = serviceProvider.GetRequiredService<IStringLocalizer<AbpProMvcWrapperResource>>();
                return localizer["Wrapper:NotFound"];
            };
        });
    }
}
