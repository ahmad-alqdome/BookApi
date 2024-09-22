namespace BookApi.Dtos
{
    public class AddBookDto
    {
        
        [Required, StringLength(100)]
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public IFormFile BookPhoto { get; set; }

       

    }
}
