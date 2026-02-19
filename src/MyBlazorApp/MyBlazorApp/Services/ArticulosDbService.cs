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

        public async Task<Pagina<Articulo>> GetPageAsync(int page, int pageSize)
        {
            var query = _context.Articulos.AsNoTracking();

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(a => a.Id)          // ordenar para consistencia
                .Skip((page - 1) * pageSize) // saltar páginas
                .Take(pageSize)              // tomar solo pageSize elementos
                .ToListAsync();

            return new Pagina<Articulo>
            {
                Page = page,
                PageSize = pageSize,
                TotalRecords = total,
                Data = items
            };
        }
    }
}
