using BlazorControlDeGastos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorControlDeGastos.BOT.Servicios.Implementaciones
{
    public class GastosServicio : IGastosServicio
    {
        private readonly HttpClient _httpClient;

        public GastosServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Gastos>> GetAllGastos()
        {
            var gastosLista = await _httpClient.GetFromJsonAsync<IEnumerable<Gastos>>($"api/gastos");
            return gastosLista.Where(gastosLista => gastosLista.TipoGasto == TipoGasto.Gasto);
        }

        public async Task<IEnumerable<Gastos>> GetAllIngresos()
        {
            var gastosLista = await _httpClient.GetFromJsonAsync<IEnumerable<Gastos>>($"api/gastos");
            return gastosLista.Where(gastosLista => gastosLista.TipoGasto == TipoGasto.Ingreso);
        }

        public async Task SaveGasto(Gastos gasto)
        {
            if (gasto.Id == 0)
            {
                // No se ejecuta la api

                var response = await _httpClient.PostAsJsonAsync<Gastos>("api/gastos", gasto);
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}");
            }
            else
            {
                await _httpClient.PutAsJsonAsync<Gastos>("api/gastos", gasto);
            }
        }

        public async Task<decimal> GetBalance()
        {
            var gastosLista = await _httpClient.GetFromJsonAsync<IEnumerable<Gastos>>($"api/gastos");
            var ingresos = gastosLista.Where(x => x.TipoGasto == TipoGasto.Ingreso).Sum(x => x.Monto);
            var gastos = gastosLista.Where(x => x.TipoGasto == TipoGasto.Gasto).Sum(x => x.Monto);
            return ingresos - gastos;

        }

    }
}
