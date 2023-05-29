using BlazorControlDeGastos.Model;

namespace BlazorControlDeGastos.UI.Interfaces
{
    public interface ICategoriaServicio
    {
        Task<IEnumerable<Categoria>> GetAllCategorias();
        Task<Categoria> GetCategoria(int id);
        Task SaveCategoria(Categoria categoria);
        Task DeleteCategoria(int id);
    }
}
