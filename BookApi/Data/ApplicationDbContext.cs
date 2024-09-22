
namespace BookApi.Data
{
    public class ApplicationDbContext:DbContext
    {

        public DbSet<Author> Authors {  get; set; }
        public DbSet<Book> Books {  get; set; }
        public DbSet<Sale> Sales {  get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Book)
                .WithOne(a => a.Author)
                .HasForeignKey(a => a.AuthorId);


            modelBuilder.Entity<Book>()
                .HasMany(a => a.Sale)
                .WithOne(a => a.Book)
                .HasForeignKey(a => a.BookId);

        }
    }
}
