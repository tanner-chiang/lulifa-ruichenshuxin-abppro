namespace RuichenShuxin.AbpPro;

public class BookExportListInput : LimitedResultRequestDto, ISortedResultRequest
{
    [Required]
    public string FileName { get; set; }
    public string? Filterr { get; set; }
    public string? Sorting {  get; set; }
}
