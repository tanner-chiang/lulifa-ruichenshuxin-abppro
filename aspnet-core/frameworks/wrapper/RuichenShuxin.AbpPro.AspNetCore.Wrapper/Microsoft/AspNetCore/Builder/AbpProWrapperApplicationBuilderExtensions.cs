namespace Microsoft.AspNetCore.Builder;

public static class AbpProWrapperApplicationBuilderExtensions
{
    private const string ExceptionHandlingMiddlewareMarker = "_AbpExceptionHandlingMiddleware_Added";

    public static IApplicationBuilder UseWrapperExceptionHandling(this IApplicationBuilder app)
    {
        if (app.Properties.ContainsKey(ExceptionHandlingMiddlewareMarker))
        {
            return app;
        }

        app.Properties[ExceptionHandlingMiddlewareMarker] = true;
        return app.UseMiddleware<AbpProExceptionHandlingWrapperMiddleware>();
    }
}
