namespace SmallBookLibrary.Interfaces
{
    public interface IBookService
    {
        Task<Book> CreateBookAsync(Book book);
        Task<Book> GetBookAsync(Guid Id);
        Task<List<Book>> GetBooksAsync();
        Task<Book> UpdateBookAsync(Book book);
    }
}