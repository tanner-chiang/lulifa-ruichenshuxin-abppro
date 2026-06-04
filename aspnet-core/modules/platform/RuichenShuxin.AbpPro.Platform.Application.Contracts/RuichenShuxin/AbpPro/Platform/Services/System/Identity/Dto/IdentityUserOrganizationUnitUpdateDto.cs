namespace RuichenShuxin.AbpPro.Platform;

public class IdentityUserOrganizationUnitUpdateDto
{
    [Required]
    public Guid[] OrganizationUnitIds { get; set; }
}
