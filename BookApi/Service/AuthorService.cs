
namespace BookApi.Service
{


    public class AuthorService : IAuthorService
    {

        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorGetDto>> GetAllAuthorsAsync()
        {
            var all= await _context.Authors
                .Select( data=>
                new AuthorGetDto
                 {
                 AuthorId=data.AuthorId,
                    AuthorName=data.AuthorName ,
                    AuthorPhoto=data.AuthorPhoto 
                }
                
                ).ToListAsync();



            return all;
        }

        public async Task<AuthorGetDto> GetAuthorAsync(int id)
        {
            var author = await _context.Authors
                .Where(data=>data.AuthorId==id)
                .Select(data =>new AuthorGetDto { AuthorId = data.AuthorId ,  AuthorName=data.AuthorName , AuthorPhoto = data.AuthorPhoto })
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
