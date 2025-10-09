using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormacionAcademicaController : ControllerBase
    {
        private readonly FormacionAcademicaService _service;

        public FormacionAcademicaController(FormacionAcademicaService service)
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
                return NotFound("No se encontró la formación académica especificada.");
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] FormacionAcademicaDto dto)
        {
            var id = await _service.InsertarAsync(dto);
            return Ok(new { iCodFormacionAcademica = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] FormacionAcademicaDto dto)
        {
            dto.iCodFormacionAcademica = id;
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
