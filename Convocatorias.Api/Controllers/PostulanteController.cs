using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.Services;
using Convocatorias.Application.DTOs;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostulanteController : ControllerBase
    {
        private readonly PostulanteService _service;

        public PostulanteController(PostulanteService service)
        {
            _service = service;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var lista = await _service.ListarPostulantesAsync();
            return Ok(lista);
        }

        [HttpGet("obtener/{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var item = await _service.ObtenerPorIdAsync(id);
            if (item == null) return NotFound("Postulante no encontrado.");
            return Ok(item);
        }

        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] PostulanteDto dto)
        {
            var mensaje = await _service.InsertarPostulanteAsync(dto);
            return Ok(new { mensaje });
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] PostulanteDto dto)
        {
            var mensaje = await _service.ActualizarPostulanteAsync(dto);
            return Ok(new { mensaje });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var mensaje = await _service.EliminarPostulanteAsync(id);
            return Ok(new { mensaje });
        }
    }
}
