namespace RuichenShuxin.AbpPro.Platform;

public class MenuCreateDto : MenuCreateOrUpdateDto
{
    [Required]
    public Guid LayoutId { get; set; }
}
