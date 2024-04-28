namespace SmallBookLibrary.Services
{
    public class BookService : GenericRepository<Book>, IBookService
    {

        public BookService(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Book>> GetBooksAsync()
        {
            var books=  GetAll()
                        .Where(x => x.IsDeleted == false)
                      .OrderByDescending(x => x.ModifiedOn)
                       .ToListAsync();
           //var bookslist = await books.ConfigureAwait(false);
            return await books;
        }

        public async Task<Book> GetBookAsync(Guid Id)
        {
            return await GetById(Id);

        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            return await Create(book);
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            return await Update(book);
        }

    }
}
