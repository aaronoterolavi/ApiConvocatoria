using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BonificacionesAdicionalesController : ControllerBase
    {
        private readonly BonificacionesAdicionalesService _service;

        public BonificacionesAdicionalesController(BonificacionesAdicionalesService service)
        {
            _service = service;
        }

        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] BonificacionesAdicionalesDTO dto)
        {
            var mensaje = await _service.InsertarAsync(dto);
            return Ok(new { mensaje });
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] BonificacionesAdicionalesDTO dto)
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
        public async Task<IActionResult> Listar([FromQuery] int? iCodUsuario)
        {
            var (lista, mensaje) = await _service.ListarAsync(iCodUsuario);
            return Ok(new { mensaje, lista });
        }
    }
}
