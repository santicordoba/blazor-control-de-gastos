using BlazorControlDeGastos.Model;

namespace BlazorControlDeGastos.BOT.Servicios
{
    public interface IGastosServicio
    {
        Task<IEnumerable<Gastos>> GetAllGastos();
        Task<IEnumerable<Gastos>> GetAllIngresos();

        Task SaveGasto(Gastos gasto);

        Task<decimal> GetBalance();
    }
}