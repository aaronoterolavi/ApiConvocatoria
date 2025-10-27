using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormatoArchivoPostulacionController : ControllerBase
    {
        private readonly FormatoArchivoPostulacionService _service;

        public FormatoArchivoPostulacionController(FormatoArchivoPostulacionService service)
        {
            _service = service;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var result = await _service.ListarAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] FormatoArchivoPostulacionInsertarDto dto)
        {
            var result = await _service.InsertarAsync(dto);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] FormatoArchivoPostulacionActualizarDto dto)
        {
            var result = await _service.ActualizarAsync(dto);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("eliminar/{iCodFormato}")]
        public async Task<IActionResult> Eliminar(int iCodFormato)
        {
            var result = await _service.EliminarAsync(iCodFormato);
            return Ok(result);
        }
    }
}
