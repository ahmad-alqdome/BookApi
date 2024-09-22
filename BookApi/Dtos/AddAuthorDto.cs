namespace BookApi.Dtos
{
    public class AddAuthorDto
    {
        public string AuthorName { get; set; }
        public IFormFile AuthorPhoto { get; set; }
    }
}
