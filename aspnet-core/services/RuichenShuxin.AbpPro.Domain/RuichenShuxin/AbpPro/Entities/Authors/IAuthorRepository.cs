namespace RuichenShuxin.AbpPro;

public interface IAuthorRepository : IRepository<Author, Guid>
{
    Task<Author?> FindByNameAsync(string name);

    Task<List<Author>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null
    );
}
