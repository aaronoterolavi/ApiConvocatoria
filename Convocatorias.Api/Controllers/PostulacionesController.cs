using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostulacionesController : ControllerBase
    {
        private readonly PostulacionService _service;

        public PostulacionesController(PostulacionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Inserta una postulación usando PA_InsertarPostulacion
        /// Body esperado: { "iCodUsuario":1, "iCodConvocatoria":2, "iCodUsuarioRegistra":3 }

        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] PostulacionDto body)
        {
            if (body == null)
                return BadRequest("Body requerido.");

            var result = await _service.InsertarAsync(body.iCodUsuario, body.iCodConvocatoria, body.iCodUsuarioRegistra ?? 0);

            if (result.Mensaje.StartsWith("Error", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { mensaje = result.Mensaje });

            return Ok(new
            {
                mensaje = result.Mensaje,
                iCodPostulacion = result.iCodPostulacion
            });
        }

        /// <summary>
        /// Actualiza una postulación usando PA_ActualizarPostulacion
        /// Body esperado: { "iCodPostulacion":1, "iCodUsuario":1, "iCodConvocatoria":2, "iCodUsuarioRegistra":3 }
        /// </summary>
        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] PostulacionDto body)
        {
            if (body == null) return BadRequest("Body requerido.");
            var msg = await _service.ActualizarAsync(
                body.iCodPostulacion, body.iCodUsuario, body.iCodConvocatoria, body.iCodUsuarioRegistra ?? 0);

            if (msg.StartsWith("Error", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { mensaje = msg });

            return Ok(new { mensaje = msg });
        }

        /// <summary>
        /// Eliminación lógica usando PA_EliminarPostulacion
        /// </summary>
        [HttpDelete("eliminar/{iCodPostulacion:int}")]
        public async Task<IActionResult> Eliminar(int iCodPostulacion)
        {
            var msg = await _service.EliminarAsync(iCodPostulacion);
            if (msg.StartsWith("Error", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { mensaje = msg });

            return Ok(new { mensaje = msg });
        }

        /// <summary>
        /// Lista (no paginado) usando PA_ListarPostulaciones
        /// </summary>
        [HttpGet("listar")]
        public async Task<IActionResult> Listar(
            [FromQuery] int? iCodUsuario = null,
            [FromQuery] int? iCodConvocatoria = null,
            [FromQuery] bool soloActivos = true)
        {
            var data = await _service.ListarAsync(iCodUsuario, iCodConvocatoria, soloActivos);
            return Ok(data);
        }

        /// <summary>
        /// Lista paginada usando PA_ListarPostulacionesPaginado
        /// </summary>
        [HttpGet("listar-paginado")]
        public async Task<IActionResult> ListarPaginado(
            [FromQuery] int? iCodPostulacion = null,
            [FromQuery] int? iCodUsuario = null,
            [FromQuery] int? iCodConvocatoria = null,
            [FromQuery] int? iCodTipoConvocatoria = null,
            [FromQuery] int? iCodUnidadZonal = null,
            [FromQuery] string? vNumDocumento = null,
            [FromQuery] string? vNombreCompleto = null,
            [FromQuery] string? vTituloConvocatoria = null,
            [FromQuery] string? vTipoConvocatoria = null,
            [FromQuery] string? vUnidadZonal = null,
            [FromQuery] DateTime? FechaPostulacionDesde = null,
            [FromQuery] DateTime? FechaPostulacionHasta = null,
            [FromQuery] bool soloActivos = true,
            [FromQuery] int PageNumber = 1,
            [FromQuery] int PageSize = 10)
        {
            var (items, total) = await _service.ListarPaginadoAsync(
                iCodPostulacion, iCodUsuario, iCodConvocatoria, iCodTipoConvocatoria, iCodUnidadZonal,
                vNumDocumento, vNombreCompleto, vTituloConvocatoria, vTipoConvocatoria, vUnidadZonal,
                FechaPostulacionDesde, FechaPostulacionHasta, soloActivos, PageNumber, PageSize);

            return Ok(new
            {
                PageNumber,
                PageSize,
                Total = total,
                Items = items
            });
        }
    }
}
