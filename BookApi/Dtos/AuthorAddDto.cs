namespace BookApi.Dtos
{
    public class AuthorAddDto
    {
        public string AuthorName { get; set; }

        public IFormFile AuthorPhoto { get; set; }

    }
}
