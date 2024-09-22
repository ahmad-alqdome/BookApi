
namespace BookApi.Service
{
    public interface IAuthorService
    {
        public Task<IEnumerable<GetAuthorDto>> GetAllAuthorsAsync();
        public Task<GetAuthorDto> GetAuthorAsync(int id);
        public Task<Author> AddAuthorAsnc(Author author);
        public Author Update(Author author);
        public Author Delete(Author author);

        public Task<bool> IsValidAuthorAsync(int id);



    }
}
