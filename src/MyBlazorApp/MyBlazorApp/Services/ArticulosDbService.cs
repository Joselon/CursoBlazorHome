using Microsoft.EntityFrameworkCore;
using MyBlazorApp.Data;
using MyBlazorApp.ViewModels;

namespace MyBlazorApp.Services
{
    public class ArticulosDbService : IArticulosService

    {
        private readonly AppDbContext _context;

        public ArticulosDbService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Articulo>> GetAllAsync()
        {
            return await _context.Articulos
                                 .AsNoTracking()
                                 .ToListAsync();
        }
    }
}
