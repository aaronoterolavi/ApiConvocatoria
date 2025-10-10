using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;

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

        [HttpGet("usuario/{iCodUsuario}")]
        public async Task<IActionResult> ListarPorUsuario(int iCodUsuario)
        {
            var lista = await _service.ListarPorUsuarioAsync(iCodUsuario);
            return Ok(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] FormacionAcademicaDTO dto)
        {
            var id = await _service.InsertarAsync(dto);
            return Ok(new { mensaje = "Registro insertado correctamente.", iCodFormacionAcademica = id });
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] FormacionAcademicaDTO dto)
        {
            await _service.ActualizarAsync(dto);
            return Ok(new { mensaje = "Registro actualizado correctamente." });
        }

        [HttpDelete("{iCodFormacionAcademica}")]
        public async Task<IActionResult> Eliminar(int iCodFormacionAcademica)
        {
            await _service.EliminarAsync(iCodFormacionAcademica);
            return Ok(new { mensaje = "Registro eliminado correctamente." });
        }
    }
}
