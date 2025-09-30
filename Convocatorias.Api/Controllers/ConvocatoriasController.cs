using Convocatorias.Application.DTOs;
using Convocatorias.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConvocatoriasController : ControllerBase
    {
        private readonly IConvocatoriaService _service;

        public ConvocatoriasController(IConvocatoriaService service)
        {
            _service = service;
        }

        [Authorize(Roles = "1")]
        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] ConvocatoriaInsertarDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CrearConvocatoriaAsync(dto);

            return result.Codigo == 1 ? Ok(result) : BadRequest(result);
        }

        [HttpGet("paginado")]
        public async Task<IActionResult> GetConvocatoriasPaginado(
            int pageNumber = 1,
            int pageSize = 10,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null,
            int? codTipoConvocatoria = null,
            bool? bActivo = null,
            string? buscar = null)
        {
            var result = await _service.ListarConvocatoriasPaginadoAsync(
                pageNumber, pageSize, fechaInicio, fechaFin, codTipoConvocatoria,bActivo, buscar);

            return Ok(result);
        }

        [HttpPut("{idConvocatoria}")]
        public async Task<ActionResult<ResponseDto>> Actualizar(int idConvocatoria, [FromBody] ConvocatoriaUpdateDto dto)
        {
            if (idConvocatoria != dto.iCodConvocatoria)
                return BadRequest("El id de la ruta no coincide con el del cuerpo.");

            var result = await _service.ActualizarAsync(dto);
            return result.Codigo == 1 ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{idConvocatoria}")]
        public async Task<ActionResult<ResponseDto>> Eliminar(int idConvocatoria)
        {
            var result = await _service.EliminarAsync(idConvocatoria);
            return result.Codigo == 1 ? Ok(result) : BadRequest(result);
        }
    }
}
