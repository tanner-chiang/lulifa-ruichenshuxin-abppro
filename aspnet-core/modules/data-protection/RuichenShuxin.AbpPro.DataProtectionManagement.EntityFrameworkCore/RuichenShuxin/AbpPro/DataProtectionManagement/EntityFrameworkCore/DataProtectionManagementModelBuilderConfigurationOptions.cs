namespace RuichenShuxin.AbpPro.DataProtectionManagement.EntityFrameworkCore;

public class DataProtectionManagementModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
{
    public DataProtectionManagementModelBuilderConfigurationOptions(
        [NotNull] string tablePrefix = "",
        [CanBeNull] string schema = null)
        : base(
            tablePrefix,
            schema)
    {

    }
}
