using BlazorControlDeGastos.Model;
using BlazorControlDeGastos.UI.Interfaces;

namespace BlazorControlDeGastos.UI.Services
{
    public class GastosServicio : IGastosServicio
    {
        private readonly HttpClient _httpClient;

        public GastosServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteGasto(int id)
        {
            await _httpClient.DeleteAsync($"api/gastos/{id}");
        }

        public async Task<IEnumerable<Gastos>> GetAllGastos()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Gastos>>($"api/gastos");
        }

        public async Task<Gastos> GetGasto(int id)
        {
            return await _httpClient.GetFromJsonAsync<Gastos>($"api/gastos/{id}");
        }

        public async Task SaveGasto(Gastos gasto)
        {
            if(gasto.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<Gastos>("api/gastos", gasto);
            }
            else
            {
                await _httpClient.PutAsJsonAsync<Gastos>("api/gastos", gasto);
            }
        }
    }
}
