namespace RuichenShuxin.AbpPro;

public class BookGetListInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
