using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoDiplomadoController : ControllerBase
    {
        private readonly CursoDiplomadoService _service;

        public CursoDiplomadoController(CursoDiplomadoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] CursoDiplomadoDto dto)
        {
            var id = await _service.InsertAsync(dto);
            return Ok(new { iCodCursoDiplomado = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] CursoDiplomadoDto dto)
        {
            dto.iCodCursoDiplomado = id;
            var mensaje = await _service.UpdateAsync(dto);
            return Ok(new { mensaje });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var mensaje = await _service.DeleteAsync(id);
            return Ok(new { mensaje });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("postulante/{iCodPostulante}")]
        public async Task<IActionResult> ListarPorPostulante(int iCodPostulante)
        {
            var lista = await _service.GetByPostulanteAsync(iCodPostulante);
            return Ok(lista);
        }
    }
}
