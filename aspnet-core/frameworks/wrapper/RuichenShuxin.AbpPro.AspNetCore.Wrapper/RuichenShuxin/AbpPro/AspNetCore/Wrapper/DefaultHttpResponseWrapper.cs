namespace RuichenShuxin.AbpPro.AspNetCore.Wrapper;

public class DefaultHttpResponseWrapper : IHttpResponseWrapper, ITransientDependency
{
    public ILogger<DefaultHttpResponseWrapper> Logger { protected get; set; }

    protected AbpProWrapperOptions Options { get; }

    public DefaultHttpResponseWrapper(IOptions<AbpProWrapperOptions> options)
    {
        Options = options.Value;

        Logger = NullLogger<DefaultHttpResponseWrapper>.Instance;
    }

    public virtual void Wrap(HttpResponseWrapperContext context)
    {
        if (!context.HttpContext.Response.HasStarted)
        {
            context.HttpContext.Response.StatusCode = context.HttpStatusCode;
            if (context.HttpHeaders != null)
            {
                foreach (var header in context.HttpHeaders)
                {
                    if (!context.HttpContext.Response.Headers.ContainsKey(header.Key))
                    {
                        context.HttpContext.Response.Headers.Append(header.Key, header.Value);
                    }
                }
            }
            if (!context.HttpContext.Response.Headers.ContainsKey(AbpProHttpWrapConsts.AbpWrapResult))
            {
                context.HttpContext.Response.Headers.Append(AbpProHttpWrapConsts.AbpWrapResult, "true");
            }
        }
        else
        {
            Logger.LogWarning("HTTP response has already started, cannot set headers and status code!");
        }
    }
}
