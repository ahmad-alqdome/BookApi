
namespace BookApi.Service
{


    public class AuthorService : IAuthorService
    {

        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetAuthorDto>> GetAllAuthorsAsync()
        {
            var allAuthors= await _context.Authors
                .Select( data=>
                new GetAuthorDto
                {
                 AuthorId=data.AuthorId,
                    AuthorName=data.AuthorName ,
                    AuthorPhoto=Convert.ToBase64String(data.AuthorPhoto) 
                }
                
                ).ToListAsync();

            return allAuthors;
        }

        public async Task<GetAuthorDto> GetAuthorAsync(int id)
        {
            var author = await _context.Authors
                .Where(data=>data.AuthorId==id)
                .Select(data =>new GetAuthorDto { 
                 AuthorId = data.AuthorId 
                ,AuthorName=data.AuthorName 
                ,AuthorPhoto = Convert.ToBase64String(data.AuthorPhoto) })
                .FirstOrDefaultAsync();
              
               
            return author;
        }

        public async Task<Author> AddAuthorAsnc(Author author)
        {
            await _context.Authors.AddAsync (author);
            _context.SaveChanges();

            return author;
        }


        public Author Update(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges ();
            return author;
        }
        public Author Delete(Author author)
        {
            _context.Authors.Remove(author);
            _context.SaveChanges();
            return author;
        }

        public async  Task<bool> IsValidAuthorAsync(int id)
        {
            return await _context.Authors.AnyAsync(a => a.AuthorId == id);
        }

     
    
    }
}
