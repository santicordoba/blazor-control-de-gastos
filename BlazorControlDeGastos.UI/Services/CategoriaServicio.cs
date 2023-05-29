using BlazorControlDeGastos.Model;
using BlazorControlDeGastos.UI.Interfaces;

namespace BlazorControlDeGastos.UI.Services
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly HttpClient _httpClient;

        public CategoriaServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task DeleteCategoria(int id)
        {
            await _httpClient.DeleteAsync($"api/categoria/{id}");
        }

        public async Task<IEnumerable<Categoria>> GetAllCategorias()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Categoria>>($"api/categoria");

        }

        public async Task<Categoria> GetCategoria(int id)
        {
            return await _httpClient.GetFromJsonAsync<Categoria>($"api/categoria/{id}");
        }

        public async Task SaveCategoria(Categoria categoria)
        {
            if(categoria.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<Categoria>("api/categoria", categoria);
            }
            else
            {
                await _httpClient.PutAsJsonAsync<Categoria>("api/categoria", categoria);
            }
        }
    }
}
