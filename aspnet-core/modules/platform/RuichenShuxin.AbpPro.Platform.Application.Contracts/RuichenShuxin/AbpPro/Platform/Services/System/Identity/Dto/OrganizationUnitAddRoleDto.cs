namespace RuichenShuxin.AbpPro.Platform;
public class OrganizationUnitAddRoleDto
{
    [Required]
    public List<Guid> RoleIds { get; set; }
}
