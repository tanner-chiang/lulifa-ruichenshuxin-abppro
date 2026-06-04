namespace RuichenShuxin.AbpPro;

[Authorize(AbpProPermissions.Books.Default)]
public class BookAppService : AbpProAppService, IBookAppService //implement the IBookAppService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly AuthorManager _authorManager;

    protected IDataAccessEntityTypeInfoProvider EntityTypeInfoProvider => LazyServiceProvider.LazyGetRequiredService<IDataAccessEntityTypeInfoProvider>();

    public BookAppService(
        IBookRepository bookRepository,
        AuthorManager authorManager,
        IAuthorRepository authorRepository)
    {
        _authorManager = authorManager;
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
    }

    [Authorize(AbpProPermissions.Books.Create)]
    [DisableDataProtected]// 任何人都可创建
    public async virtual Task<BookDto> CreateAsync(CreateBookDto input)
    {
        var author = await _authorRepository.GetAsync(input.AuthorId);
        var book = new Book(
            GuidGenerator.Create(),
            input.Name,
            input.Type,
            input.PublishDate,
            input.Price,
            author.Id);

        await _bookRepository.InsertAsync(book);

        var bookDto = ObjectMapper.Map<Book, BookDto>(book);
        bookDto.AuthorName = author.Name;

        return bookDto;
    }

    [Authorize(AbpProPermissions.Books.Edit)]
    [DataProtected(DataAccessOperation.Write)]// 仅启用数据更新过滤器
    public async virtual Task<BookDto> UpdateAsync(Guid id, UpdateBookDto input)
    {
        var book = await _bookRepository.GetAsync(id);

        if (!input.Name.IsNullOrWhiteSpace() && !string.Equals(input.Name, book.Name, StringComparison.InvariantCultureIgnoreCase))
        {
            book.Name = input.Name;
        }
        if (input.Type.HasValue && input.Type != book.Type)
        {
            book.Type = input.Type.Value;
        }
        if (input.PublishDate.HasValue && input.PublishDate != book.PublishDate)
        {
            book.PublishDate = input.PublishDate.Value;
        }
        if (input.Price.HasValue && input.Price != book.Price)
        {
            book.Price = input.Price.Value;
        }

        if (input.AuthorId != book.AuthorId)
        {
            var newAuthor = await _authorRepository.GetAsync(input.AuthorId);
            book.AuthorId = newAuthor.Id;
        }

        await _bookRepository.UpdateAsync(book);
        var author = await _authorRepository.GetAsync(input.AuthorId);

        var bookDto = ObjectMapper.Map<Book, BookDto>(book);
        bookDto.Name = author.Name;

        return bookDto;
    }

    public async virtual Task<BookDto> GetAsync(Guid id)
    {
        //Get the IQueryable<Book> from the repository
        var queryable = await _bookRepository.GetQueryableAsync();

        //Prepare a query to join books and authors
        var query = from book in queryable
                    join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                    where book.Id == id
                    select new { book, author };

        //Execute the query and get the book with author
        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Book), id);
        }

        var bookDto = ObjectMapper.Map<Book, BookDto>(queryResult.book);
        bookDto.AuthorName = queryResult.author.Name;
        return bookDto;
    }

    public async virtual Task<PagedResultDto<BookDto>> GetListAsync(BookGetListInput input)
    {
        var queryable = await _bookRepository.GetQueryableAsync();
        //Prepare a query to join books and authors
        var query = from book in queryable
                    join author in await _authorRepository.GetQueryableAsync() on book.AuthorId equals author.Id
                    select new { book, author };

        //Paging
        query = query
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.book.Name.Contains(input.Filter))
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        //Convert the query result to a list of BookDto objects
        var bookDtos = queryResult.Select(x =>
        {
            var bookDto = ObjectMapper.Map<Book, BookDto>(x.book);
            bookDto.AuthorName = x.author.Name;
            return bookDto;
        }).ToList();

        //Get the total count with another query
        var totalCount = await AsyncExecuter.CountAsync(query);

        return new PagedResultDto<BookDto>(
            totalCount,
            bookDtos
        );
    }


    [Authorize(AbpProPermissions.Books.Delete)]
    [DataProtected(DataAccessOperation.Delete)]// 仅启用数据删除过滤器
    public async virtual Task DeleteAsync(Guid id)
    {
        var book = await _bookRepository.GetAsync(id);

        await _bookRepository.DeleteAsync(book);
    }

    public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
    {
        var authors = await _authorRepository.GetListAsync();

        return new ListResultDto<AuthorLookupDto>(
            ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
        );
    }

    private static string NormalizeSorting(string? sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"book.{nameof(Book.Name)}";
        }

        if (sorting.Contains("authorName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "authorName",
                "author.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"book.{sorting}";
    }

    public async virtual Task<EntityTypeInfoModel> GetEntityRuleAsync(EntityTypeInfoGetModel input)
    {
        var entityType = typeof(Book);
        var resourceType = LocalizationResource ?? typeof(DefaultResource);

        var context = new DataAccessEntitTypeInfoContext(
            entityType,
            resourceType,
            input.Operation,
            LazyServiceProvider);

        return await EntityTypeInfoProvider.GetEntitTypeInfoAsync(context);
    }
}