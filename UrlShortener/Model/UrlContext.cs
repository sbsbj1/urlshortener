using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Model

{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions<UrlContext> options) : base(options)
        {
            
        }
        public DbSet<UrlModel> Urls { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.email)
                .IsUnique();


            modelBuilder.Entity<UrlModel>()
                .HasOne(e => e.User)
                .WithMany(e => e.UrlModels)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }
        
    }
}
