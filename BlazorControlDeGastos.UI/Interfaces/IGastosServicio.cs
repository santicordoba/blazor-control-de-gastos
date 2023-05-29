using BlazorControlDeGastos.Model;

namespace BlazorControlDeGastos.UI.Interfaces
{
    public interface IGastosServicio
    {
        Task<IEnumerable<Gastos>> GetAllGastos();
        Task<Gastos> GetGasto(int id);
        Task SaveGasto(Gastos gastos);
        Task DeleteGasto(int id);
    }
}
