using BookApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private List<string> allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;   //1 MB

        public BookController(IAuthorService authorService, IBookService bookService)
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
           
            return Ok(await _bookService.GetAllBooksAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var book = await _bookService.GetBooksAsync(id);

            return book is not null ?  Ok(book) : NotFound("The Specific Book is Not Found");
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync([FromForm] AddBookDto bookAuthorDto)
        {

            if (bookAuthorDto.BookPhoto == null)
            {
                return BadRequest("The Photo is Null");
            }
            if (!allowedExtenstions.Contains(Path.GetExtension(bookAuthorDto.BookPhoto.FileName).ToLower()))
            {
                return BadRequest("The invalid file");
            }

            if (_maxAllowedPosterSize < bookAuthorDto.BookPhoto.Length)
            {
                return BadRequest("The photo size is higher");
            }

            using var dataStream = new MemoryStream();

            await bookAuthorDto.BookPhoto.CopyToAsync(dataStream);

            var book = new Book
            {
                Title = bookAuthorDto.Title,
                BookPhoto = dataStream.ToArray(),
                AuthorId = bookAuthorDto.AuthorId,
            };

            await _bookService.AddBookAsync(book);

            return Ok(book);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] AddBookDto bookAuthorDto)
        {
            var existingBook = await _bookService.GetBooksAsync(id);
            var newBook=new Book();
            if (existingBook == null)
                return BadRequest("The book is not found");  

            if (bookAuthorDto.BookPhoto != null)
            {
                if (!allowedExtenstions.Contains(Path.GetExtension(bookAuthorDto.BookPhoto.FileName).ToLower()))
                {
                    return BadRequest("Invalid file format");
                }

                if (_maxAllowedPosterSize < bookAuthorDto.BookPhoto.Length)
                {
                    return BadRequest("The photo size exceeds the allowed limit");
                }

                using var dataStream = new MemoryStream();
                await bookAuthorDto.BookPhoto.CopyToAsync(dataStream);

                newBook.BookPhoto = dataStream.ToArray();
            }
            newBook.BookId= id;
            newBook.Title = bookAuthorDto.Title;
            newBook.AuthorId = bookAuthorDto.AuthorId;

             _bookService.UpdateBookAsync(newBook);

            return Ok(newBook);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>  Delete (int id)
        {
            var authorDto = await _bookService.GetBooksAsync(id);

            var book = new Book()
            {
            BookId = id,
            Title = authorDto.Title,
            AuthorId = authorDto.AuthorId,


            };

            _bookService.DeleteBookAsync(book);
            return Ok(authorDto);


        }

    }


}
