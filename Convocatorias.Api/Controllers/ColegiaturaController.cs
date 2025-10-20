using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColegiaturaController : ControllerBase
    {
        private readonly ColegiaturaService _service;

        public ColegiaturaController(ColegiaturaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var lista = _service.Listar();
            return Ok(lista);
        }

        [HttpGet("usuario/{iCodUsuario}")]
        public IActionResult ListarPorUsuario(int iCodUsuario)
        {
            var lista = _service.ListarPorUsuario(iCodUsuario);
            return Ok(lista);
        }

        [HttpPost]
        public IActionResult Insertar([FromBody] ColegiaturaDto dto)
        {
            var id = _service.Insertar(dto);
            return Ok(new { iCodColegiatura = id });
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] ColegiaturaDto dto)
        {
            dto.iCodColegiatura = id;
            _service.Actualizar(dto);
            return Ok(new { mensaje = "Registro actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            _service.Eliminar(id);
            return NoContent();
        }
    }
}
