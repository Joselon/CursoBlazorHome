using Microsoft.EntityFrameworkCore;
using MyBlazorApp.ViewModels;

namespace MyBlazorApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Articulo> Articulos => Set<Articulo>();
    }
}
