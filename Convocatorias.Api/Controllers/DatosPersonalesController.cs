using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatosPersonalesController : ControllerBase
    {
        private readonly DatosPersonalesService _service;

        public DatosPersonalesController(DatosPersonalesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var datos = await _service.ListarAsync();
            return Ok(datos);
        }

        [HttpGet("{iCodUsuario}")]
        public async Task<IActionResult> ObtenerPorUsuario(int iCodUsuario)
        {
            var dato = await _service.ObtenerPorUsuarioAsync(iCodUsuario);
            if (dato == null) return NotFound();
            return Ok(dato);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] DatosPersonalesDTO dto)
        {
            await _service.InsertarAsync(dto);
            return Ok(new { mensaje = "Registro insertado correctamente." });
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] DatosPersonalesDTO dto)
        {
            await _service.ActualizarAsync(dto);
            return Ok(new { mensaje = "Registro actualizado correctamente." });
        }

        [HttpDelete("{iCodDatosPersonales}")]
        public async Task<IActionResult> Eliminar(int iCodDatosPersonales)
        {
            await _service.EliminarAsync(iCodDatosPersonales);
            return Ok(new { mensaje = "Registro eliminado correctamente." });
        }
    }
}
