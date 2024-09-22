namespace BookApi.Dtos
{
    public class AuthorGetDto
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public byte[] AuthorPhoto { get; set; }
    }
}
