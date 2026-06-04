using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.JQuery;

namespace RuichenShuxin.AbpPro;

[DependsOn(typeof(JQueryScriptContributor))]
public class AbpProGlobalScriptContributor : BundleContributor
{
    public override Task ConfigureBundleAsync(BundleConfigurationContext context)
    {
        context.Files.Add("/scripts/abp-wrapper.js");

        return Task.CompletedTask;
    }
}