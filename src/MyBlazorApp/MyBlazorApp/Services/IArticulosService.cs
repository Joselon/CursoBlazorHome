using Microsoft.EntityFrameworkCore;
using MyBlazorApp.ViewModels;

namespace MyBlazorApp.Services
{
    public interface IArticulosService
    {
        Task<List<Articulo>> GetAllAsync();
        Task<Pagina<Articulo>> GetPageAsync(int page, int pageSize);
       
    }
}
