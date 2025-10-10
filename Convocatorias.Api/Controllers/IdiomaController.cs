using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdiomaController : ControllerBase
    {
        private readonly IdiomaService _service;

        public IdiomaController(IdiomaService service)
        {
            _service = service;
        }

        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] IdiomaDTO dto)
        {
            var mensaje = await _service.InsertarAsync(dto);
            return Ok(new { mensaje });
        }

        [HttpGet("listar/{iCodUsuario}")]
        public async Task<IActionResult> Listar(int iCodUsuario)
        {
            var result = await _service.ListarAsync(iCodUsuario);
            return Ok(result);
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] IdiomaDTO dto)
        {
            var mensaje = await _service.ActualizarAsync(dto);
            return Ok(new { mensaje });
        }

        [HttpDelete("eliminar/{iCodIdioma}")]
        public async Task<IActionResult> Eliminar(int iCodIdioma)
        {
            var mensaje = await _service.EliminarAsync(iCodIdioma);
            return Ok(new { mensaje });
        }
    }
}
