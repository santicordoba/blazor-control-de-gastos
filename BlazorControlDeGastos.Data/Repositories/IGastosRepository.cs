using BlazorControlDeGastos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorControlDeGastos.Data.Repositories
{
    public interface IGastosRepository
    {
        Task<IEnumerable<Gastos>> GetAllGastos();
        Task<Gastos> GetOneGasto(int id);
        Task<bool> AddGasto(Gastos gasto);
        Task<bool> UpdateGasto(Gastos gasto);
        Task<bool> DeleteGasto(int id);
    }
}
