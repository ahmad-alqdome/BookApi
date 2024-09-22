
namespace BookApi.Model
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required , StringLength(100)]
        public string Title { get; set; }
        public int AuthorId { get; set; }

        public byte[] BookPhoto { get; set; }   

        public virtual Author Author { get; set; }
        public virtual ICollection<Sale> Sale { get; set; }
    }
}
