
using BookApi.Model;

namespace BookApi.Service
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetBookDto>> GetAllBooksAsync()
        {
            var allBooks = await _context.Books
         .Include(b => b.Author)
         .Select(b => new GetBookDto
         {
             BookId = b.BookId,
             Title = b.Title,
             bookPhoto =  Convert.ToBase64String(b.BookPhoto) ,
             AuthorId = b.AuthorId,
             AuthorName = b.Author.AuthorName // Only include the Author's name
         })
         .ToListAsync();

            return allBooks;

        }

        public async Task<GetBookDto> GetBooksAsync(int id)
        {
            var book = await _context.Books
        .Include(b => b.Author)
        .Where(b => b.BookId == id) // Filter here for clarity
        .Select(b => new GetBookDto
        {
            BookId = b.BookId,
            Title = b.Title,
            bookPhoto = Convert.ToBase64String(b.BookPhoto), // Handle null photo
            AuthorId = b.AuthorId,
            AuthorName = b.Author.AuthorName // Only include the Author's name
        })
        .FirstOrDefaultAsync();
            return book;
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            _context.SaveChanges();
            return book;
        }

        public Book UpdateBookAsync(Book book)
        {
         _context.Books.Update(book);
            _context.SaveChanges();
            return book;

        }
        public Book DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
            return book;

        }
        public async Task<bool> IsValidBookAsync(int id)
        {
            return await _context.Books.AnyAsync(g => g.BookId == id);
        }
    }
}
