namespace RuichenShuxin.AbpPro.Wrapper;

public interface IExceptionWrapHandlerFactory
{
    IExceptionWrapHandler CreateFor(ExceptionWrapContext context);
}
