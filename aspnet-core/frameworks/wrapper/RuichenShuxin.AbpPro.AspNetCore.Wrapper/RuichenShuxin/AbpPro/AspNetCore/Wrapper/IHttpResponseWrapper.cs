namespace RuichenShuxin.AbpPro.AspNetCore.Wrapper;

public interface IHttpResponseWrapper
{
    void Wrap(HttpResponseWrapperContext context);
}
