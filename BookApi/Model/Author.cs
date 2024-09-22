
namespace BookApi.Model
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required , StringLength(100)]
        public string AuthorName { get; set; }

        public byte[] AuthorPhoto { get; set; }

        public virtual ICollection<Book> Book { get; set; }


    }
}
