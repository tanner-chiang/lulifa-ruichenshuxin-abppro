namespace RuichenShuxin.AbpPro;

public class GetAuthorListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}