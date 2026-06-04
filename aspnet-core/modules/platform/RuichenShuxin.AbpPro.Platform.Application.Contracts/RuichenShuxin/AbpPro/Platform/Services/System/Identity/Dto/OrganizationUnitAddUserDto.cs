namespace RuichenShuxin.AbpPro.Platform;

public class OrganizationUnitAddUserDto
{
    [Required]
    public List<Guid> UserIds { get; set; }
}
