using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColegiaturaController : ControllerBase
    {
        private readonly ColegiaturaService _service;

        public ColegiaturaController(ColegiaturaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _service.ListarAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var item = await _service.ObtenerPorIdAsync(id);
            if (item == null)
                return NotFound("No se encontró la colegiatura especificada.");
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] ColegiaturaDto dto)
        {
            var id = await _service.InsertarAsync(dto);
            return Ok(new { iCodColegiatura = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ColegiaturaDto dto)
        {
            dto.iCodColegiatura = id;
            var mensaje = await _service.ActualizarAsync(dto);
            return Ok(new { mensaje });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var mensaje = await _service.EliminarAsync(id);
            return Ok(new { mensaje });
        }
    }
}
