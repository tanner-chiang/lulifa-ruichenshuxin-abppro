namespace RuichenShuxin.AbpPro.DataProtectionManagement;

public class RoleEntityRuleGetInput
{
    [Required]
    [DynamicStringLength(typeof(RoleEntityRuleConsts), nameof(RoleEntityRuleConsts.MaxRuletNameLength))]
    public string RoleName { get; set; }

    [Required]
    public Guid EntityTypeId { get; set; }

    [Required]
    public DataAccessOperation Operation { get; set; }
}
