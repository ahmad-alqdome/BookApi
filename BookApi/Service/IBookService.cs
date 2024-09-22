namespace BookApi.Service
{
    public interface IBookService
    {
        
        public Task<IEnumerable<BookDetailsDto>> GetAllBooksAsync();
        public Task<BookDetailsDto> GetBooksAsync(int id);

        public Task<Book> AddBookAsync(Book book);

        public Book UpdateBookAsync(Book book);
        public Book DeleteBookAsync(Book book);

        public Task<bool> IsValidBookAsync(int id);



    }
}
