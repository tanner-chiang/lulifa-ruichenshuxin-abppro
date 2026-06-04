namespace RuichenShuxin.AbpPro.Platform;

public class IdentityRoleAddOrRemoveOrganizationUnitDto
{
    [Required]
    public Guid[] OrganizationUnitIds { get; set; }
}
