using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Model

{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions<UrlContext> options) : base(options)
        {
            
        }
        public DbSet<UrlModel> Urls { get; set; }
        
    }
}
