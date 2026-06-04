namespace Microsoft.AspNetCore.Cors;

public static class AbpProCorsPolicyBuilderExtensions
{
    public static CorsPolicyBuilder WithAbpProWrapExposedHeaders(this CorsPolicyBuilder corsPolicyBuilder)
    {
        return corsPolicyBuilder
            .WithExposedHeaders(
                AbpProHttpWrapConsts.AbpWrapResult,
                AbpProHttpWrapConsts.AbpDontWrapResult);
    }
}
