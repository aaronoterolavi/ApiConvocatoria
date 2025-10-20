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
        public IActionResult Listar()
        {
            var data = _service.Listar();
            return Ok(data);
        }

        [HttpGet("usuario/{iCodUsuario}")]
        public IActionResult ListarPorUsuario(int iCodUsuario)
        {
            var data = _service.ListarPorUsuario(iCodUsuario);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] ExperienciaLaboralDto dto)
        {
            var id = _service.Insertar(dto);
            return Ok(new { iCodExperienciaLaboral = id });
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] ExperienciaLaboralDto dto)
        {
            try
            {
                dto.iCodExperienciaLaboral = id;
                _service.Actualizar(dto);
                return Ok(new { mensaje = "Registro actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message, detalle = ex.StackTrace });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            _service.Eliminar(id);
            return NoContent();
        }
    }
}
