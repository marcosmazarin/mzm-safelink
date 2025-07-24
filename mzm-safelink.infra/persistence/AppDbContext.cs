using Microsoft.EntityFrameworkCore;
using mzm_safelink.domain.entities;

namespace mzm_safelink.infra.persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Url> Urls { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Url>()
                .HasIndex(u => u.CodeUrl)
                .IsUnique();
        }
    }
}