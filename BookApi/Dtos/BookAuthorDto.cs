namespace BookApi.Dtos
{
    public class BookAuthorDto
    {
        
        [Required, StringLength(100)]
        public string Title { get; set; }
        public int AuthorId { get; set; }

        public IFormFile BookPhoto { get; set; }

       

    }
}
