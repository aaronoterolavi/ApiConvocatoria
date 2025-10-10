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

        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] CursoDiplomadoDTO dto)
        {
            await _service.InsertarAsync(dto);
            return Ok("Curso diplomado registrado correctamente.");
        }

        [HttpGet("listar/{iCodUsuario}")]
        public async Task<IActionResult> Listar(int iCodUsuario)
        {
            var result = await _service.ListarAsync(iCodUsuario);
            return Ok(result);
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] CursoDiplomadoDTO dto)
        {
            await _service.ActualizarAsync(dto);
            return Ok("Curso diplomado actualizado correctamente.");
        }

        [HttpDelete("eliminar/{iCodCursoDiplomado}")]
        public async Task<IActionResult> Eliminar(int iCodCursoDiplomado)
        {
            await _service.EliminarAsync(iCodCursoDiplomado);
            return Ok("Curso diplomado eliminado correctamente.");
        }
    }
}
