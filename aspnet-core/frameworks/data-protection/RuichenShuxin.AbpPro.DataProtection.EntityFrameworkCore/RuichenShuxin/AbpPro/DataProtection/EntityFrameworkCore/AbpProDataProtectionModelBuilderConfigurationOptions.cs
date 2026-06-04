namespace RuichenShuxin.AbpPro.DataProtection.EntityFrameworkCore;

public class AbpProDataProtectionModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
{
    public AbpProDataProtectionModelBuilderConfigurationOptions(
       [NotNull] string tablePrefix = "",
       [CanBeNull] string schema = null)
       : base(
           tablePrefix,
           schema)
    {

    }
}
