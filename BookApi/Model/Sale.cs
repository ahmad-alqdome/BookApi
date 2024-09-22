
namespace BookApi.Model
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public int SaleQuantity { get; set; }

        public int SalePrice { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

    }
}
