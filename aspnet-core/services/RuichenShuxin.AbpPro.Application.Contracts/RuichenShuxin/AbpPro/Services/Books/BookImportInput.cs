namespace RuichenShuxin.AbpPro;

public class BookImportInput
{
    [Required]
    public IRemoteStreamContent Content { get; set; }   
}
