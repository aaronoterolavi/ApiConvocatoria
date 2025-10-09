using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperienciaLaboralController : ControllerBase
    {
        private readonly ExperienciaLaboralService _service;

        public ExperienciaLaboralController(ExperienciaLaboralService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar() =>
            Ok(await _service.ListarAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var result = await _service.ObtenerPorIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] ExperienciaLaboralDto dto)
        {
            var id = await _service.InsertarAsync(dto);
            return Ok(new { iCodExperienciaLaboral = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ExperienciaLaboralDto dto)
        {
            dto.iCodExperienciaLaboral = id;
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
