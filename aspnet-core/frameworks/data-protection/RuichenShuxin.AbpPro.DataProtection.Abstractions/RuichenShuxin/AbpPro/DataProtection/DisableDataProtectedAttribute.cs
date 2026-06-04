namespace RuichenShuxin.AbpPro.DataProtection;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
public class DisableDataProtectedAttribute : Attribute
{
    public DisableDataProtectedAttribute()
    {
    }
}
