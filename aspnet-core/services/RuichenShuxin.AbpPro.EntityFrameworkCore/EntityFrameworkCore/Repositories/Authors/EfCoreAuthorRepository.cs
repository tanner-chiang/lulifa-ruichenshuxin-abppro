namespace RuichenShuxin.AbpPro.EntityFrameworkCore;

public class EfCoreAuthorRepository
    : EfCoreRepository<AbpProDbContext, Author, Guid>,
        IAuthorRepository
{
    public EfCoreAuthorRepository(
        IDbContextProvider<AbpProDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Author?> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(author => author.Name == name);
    }

    public async Task<List<Author>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                author => author.Name.Contains(filter!)
            )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
}
