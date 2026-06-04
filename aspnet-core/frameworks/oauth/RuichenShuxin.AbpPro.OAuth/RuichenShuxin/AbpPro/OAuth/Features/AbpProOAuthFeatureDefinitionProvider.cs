namespace RuichenShuxin.AbpPro.OAuth;

public class AbpProOAuthFeatureDefinitionProvider : FeatureDefinitionProvider
{
    public override void Define(IFeatureDefinitionContext context)
    {
        var group = context.AddGroup(
              name: AbpProOAuthFeatureNames.GroupName,
              displayName: L("Features:ExternalOAuthLogin"));

        group.AddFeature(
            name: AbpProOAuthFeatureNames.GitHub.Enable,
            defaultValue: "true",
            displayName: L("Features:GithubOAuthEnable"),
            description: L("Features:GithubOAuthEnableDesc"),
            valueType: new ToggleStringValueType(new BooleanValueValidator()));
        group.AddFeature(
            name: AbpProOAuthFeatureNames.Gitee.Enable,
            defaultValue: "true",
            displayName: L("Features:GiteeOAuthEnable"),
            description: L("Features:GiteeOAuthEnableDesc"),
            valueType: new ToggleStringValueType(new BooleanValueValidator()));
        group.AddFeature(
            name: AbpProOAuthFeatureNames.QQ.Enable,
            defaultValue: "true",
            displayName: L("Features:QQOAuthEnable"),
            description: L("Features:QQOAuthEnableDesc"),
            valueType: new ToggleStringValueType(new BooleanValueValidator()));
        group.AddFeature(
            name: AbpProOAuthFeatureNames.WeChat.Enable,
            defaultValue: "true",
            displayName: L("Features:WeChatOAuthEnable"),
            description: L("Features:WeChatOAuthEnableDesc"),
            valueType: new ToggleStringValueType(new BooleanValueValidator()));
        group.AddFeature(
            name: AbpProOAuthFeatureNames.WeCom.Enable,
            defaultValue: "true",
            displayName: L("Features:WeComOAuthEnable"),
            description: L("Features:WeComOAuthEnableDesc"),
            valueType: new ToggleStringValueType(new BooleanValueValidator()));
        group.AddFeature(
            name: AbpProOAuthFeatureNames.Bilibili.Enable,
            defaultValue: "true",
            displayName: L("Features:BilibiliOAuthEnable"),
            description: L("Features:BilibiliOAuthEnableDesc"),
            valueType: new ToggleStringValueType(new BooleanValueValidator()));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpProOAuthResource>(name);
    }

}
