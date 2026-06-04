namespace RuichenShuxin.AbpPro;

public class AuthorDto : EntityDto<Guid>
{
    public string Name { get; set; }

    public DateTime BirthDate { get; set; }

    public string? ShortBio { get; set; }
}
