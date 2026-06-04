namespace RuichenShuxin.AbpPro.DataProtectionManagement;

public class OrganizationUnitEntityRuleCreateDto : EntityRuleCreateOrUpdateDto
{
    [Required]
    public Guid EntityTypeId { get; set; }

    [Required]
    public Guid OrgId { get; set; }

    [Required]
    [DynamicStringLength(typeof(OrganizationUnitEntityRuleConsts), nameof(OrganizationUnitEntityRuleConsts.MaxCodeLength))]
    public string OrgCode { get; set; }
}
