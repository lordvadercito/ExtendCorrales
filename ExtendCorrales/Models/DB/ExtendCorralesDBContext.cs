using Microsoft.EntityFrameworkCore;

namespace ExtendCorrales.Models.DB
{
    public class ExtendCorralesDBContext : DbContext
    {
        public ExtendCorralesDBContext(DbContextOptions<ExtendCorralesDBContext> options)
            : base(options)
        {

        }

        public DbSet<ExtendCorral> ExtendCorrales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExtendCorral>().ToTable("ExtendCorral");
        }
    }
}
