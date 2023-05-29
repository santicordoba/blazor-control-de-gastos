using BlazorControlDeGastos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorControlDeGastos.Data.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllCategorias();
        Task<Categoria> GetCategoria(int id);
        Task<bool> InsertarCategoria(Categoria categoria);
        Task<bool> UpdateCategoria(Categoria categoria);
        Task<bool> DeleteCategoria(int id);
    }
}
