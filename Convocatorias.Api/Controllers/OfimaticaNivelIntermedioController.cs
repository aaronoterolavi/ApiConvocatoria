using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfimaticaNivelIntermedioController : ControllerBase
    {
        private readonly OfimaticaNivelIntermedioService _service;

        public OfimaticaNivelIntermedioController(OfimaticaNivelIntermedioService service)
        {
            _service = service;
        }

        [HttpPost("insertar")]
        public IActionResult Insertar([FromBody] OfimaticaNivelIntermedioDTO dto)
        {
            var mensaje = _service.Insertar(dto);
            return Ok(new { mensaje });
        }

        [HttpPut("actualizar")]
        public IActionResult Actualizar([FromBody] OfimaticaNivelIntermedioDTO dto)
        {
            var mensaje = _service.Actualizar(dto);
            return Ok(new { mensaje });
        }

        [HttpDelete("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var mensaje = _service.Eliminar(id);
            return Ok(new { mensaje });
        }

        [HttpGet("listar")]
        public IActionResult Listar([FromQuery] int? iCodUsuario = null)
        {
            var (lista, mensaje) = _service.Listar(iCodUsuario);
            return Ok(new { mensaje, lista });
        }
    }
}
