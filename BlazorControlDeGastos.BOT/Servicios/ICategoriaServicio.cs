using BlazorControlDeGastos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorControlDeGastos.BOT.Servicios
{
    public interface ICategoriaServicio
    {
        Task<IEnumerable<Categoria>> GetAllCategorias();

    }
}
