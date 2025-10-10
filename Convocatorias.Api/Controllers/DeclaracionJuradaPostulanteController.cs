using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeclaracionJuradaPostulanteController : ControllerBase
    {
        private readonly DeclaracionJuradaPostulanteService _service;

        public DeclaracionJuradaPostulanteController(DeclaracionJuradaPostulanteService service)
        {
            _service = service;
        }

        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] DeclaracionJuradaPostulanteDTO dto)
        {
            var mensaje = await _service.InsertarAsync(dto);
            return Ok(new { mensaje });
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] DeclaracionJuradaPostulanteDTO dto)
        {
            var mensaje = await _service.ActualizarAsync(dto);
            return Ok(new { mensaje });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var mensaje = await _service.EliminarAsync(id);
            return Ok(new { mensaje });
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var lista = await _service.ListarAsync();
            return Ok(lista);
        }
    }
}
