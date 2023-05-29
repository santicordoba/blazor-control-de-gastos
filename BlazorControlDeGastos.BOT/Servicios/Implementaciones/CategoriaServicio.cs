using BlazorControlDeGastos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorControlDeGastos.BOT.Servicios.Implementaciones
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly HttpClient _httpClient;

        public CategoriaServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Categoria>> GetAllCategorias()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Categoria>>($"api/categoria");
        }
    }
}
