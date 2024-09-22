
namespace BookApi.Service
{
    public interface IAuthorService
    {
        public Task<IEnumerable<AuthorGetDto>> GetAllAuthorsAsync();
        public Task<AuthorGetDto> GetAuthorAsync(int id);
        public Task<Author> AddAuthorAsnc(Author author);
        public Author Update(Author author);
        public Author Delete(Author author);

        public Task<bool> IsValidAuthorAsync(int id);



    }
}
