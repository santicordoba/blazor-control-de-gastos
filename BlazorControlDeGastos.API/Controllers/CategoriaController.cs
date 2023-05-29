using BlazorControlDeGastos.Data.Repositories;
using BlazorControlDeGastos.Model;
using Microsoft.AspNetCore.Mvc;

namespace BlazorControlDeGastos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategorias()
        {
            return Ok(await _categoriaRepository.GetAllCategorias());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoria(int id)
        {
            return Ok(await _categoriaRepository.GetCategoria(id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertarCategoria([FromBody] Categoria categoria)
        {
            if (categoria == null)
                return BadRequest();

            if (categoria.DescCategoria.Trim() == string.Empty)
            {
                ModelState.AddModelError("DescCategoria", "Debe ingresar una categoria");
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _categoriaRepository.InsertarCategoria(categoria);
            return Created("Created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategoria([FromBody] Categoria categoria)
        {
            if(categoria == null)
            {
                return BadRequest();
            }

            if(categoria.DescCategoria.Trim() == string.Empty)
            {
                ModelState.AddModelError("DescCategoria", "Debe ingresar una categoria");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoriaRepository.UpdateCategoria(categoria);
            return NoContent(); // EXITOSO 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            await _categoriaRepository.DeleteCategoria(id);
            return NoContent(); // EXITOSO

        }

    }
}
