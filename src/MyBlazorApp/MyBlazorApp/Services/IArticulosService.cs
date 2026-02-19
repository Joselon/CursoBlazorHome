using MyBlazorApp.ViewModels;

namespace MyBlazorApp.Services
{
    public interface IArticulosService
    {
        Task<List<Articulo>> GetAllAsync();
    }
}
