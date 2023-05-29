using BlazorControlDeGastos.Data.Repositories;
using BlazorControlDeGastos.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorControlDeGastos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastosController : Controller
    {
        private readonly IGastosRepository _gastosRepository;

        public GastosController(IGastosRepository gastosRepository)
        {
            _gastosRepository = gastosRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGastos()
        {
            return Ok(await _gastosRepository.GetAllGastos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGasto(int id)
        {
            return Ok(await _gastosRepository.GetOneGasto(id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertarGasto([FromBody] Gastos gasto)
        {
            if (gasto == null)
                return BadRequest();
            if (gasto.Monto < 0)
            {
                ModelState.AddModelError("Monto", "El valor de monto debe ser mayor a 0");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await _gastosRepository.AddGasto(gasto);
            return Created("Created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGasto([FromBody] Gastos gasto)
        {
            if (gasto == null)
            {
                return BadRequest();
            }
            if (gasto.Monto < 0)
            {
                ModelState.AddModelError("Monto", "El valor de monto debe ser mayor a 0");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _gastosRepository.UpdateGasto(gasto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasto(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            await _gastosRepository.DeleteGasto(id);
            return NoContent(); // EXITOSO
        }

    }
}
