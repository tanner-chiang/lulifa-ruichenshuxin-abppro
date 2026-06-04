namespace RuichenShuxin.AbpPro.AspNetCore.Mvc.Wrapper;

public interface IActionResultWrapper
{
    void Wrap(FilterContext context);
}
