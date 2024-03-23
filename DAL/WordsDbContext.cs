using ServerWordle.DAL.DBO;
using Microsoft.EntityFrameworkCore;

namespace ServerWordle.DAL
{
    public class WordsDbContext : DbContext
    {
        public WordsDbContext(DbContextOptions<WordsDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Word> Word { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Word>()
                .HasKey(x => x.id_word);

            modelBuilder.Entity<Word>()
                .Property(x => x.id_word);

            modelBuilder.Entity<Word>()
                .Property(x => x.word_otd)
                .HasMaxLength(5);

            modelBuilder.Entity<Word>()
                .Property(x => x.date);
        }
    }
}
