namespace BookApi.Dtos
{
    public class GetBookDto
    {

        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string bookPhoto { get; set; }
        public string AuthorName { get; set; } // Include only the Author's name


    }
}
