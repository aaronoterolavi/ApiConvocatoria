using System.Transactions;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConvocatoriaFaseController : ControllerBase
    {
        private readonly ConvocatoriaFaseService _service;

        public ConvocatoriaFaseController(ConvocatoriaFaseService service)
        {
            _service = service;
        }

        [Authorize(Roles = "1")]
        [HttpPost("insertar")]
        public async Task<IActionResult> Insertar([FromBody] ConvocatoriaFaseInsertarDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _service.RegistrarFaseAsync(dto);
            if (result.Codigo == 1) return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Roles = "1")]
        [HttpPost("insertar-multiple")]
        public async Task<IActionResult> InsertarVarias([FromBody] List<ConvocatoriaFaseInsertarDto> fases)
        {
            if (fases == null || fases.Count == 0)
                return BadRequest("Debe proporcionar al menos una fase.");

            // Validar duplicados por estado
            var duplicados = fases.GroupBy(f => f.CodEstado)
                                  .Where(g => g.Count() > 1)
                                  .Select(g => g.Key)
                                  .ToList();
            if (duplicados.Any())
                return BadRequest($"Existen fases duplicadas con los mismos estados: {string.Join(", ", duplicados)}");

            // Validar fechas solapadas dentro de la lista
            var ordenadas = fases.OrderBy(f => f.FechaInicio).ToList();
            for (int i = 0; i < ordenadas.Count - 1; i++)
            {
                if (ordenadas[i].FechaFin >= ordenadas[i + 1].FechaInicio)
                    return BadRequest("Existen fases con fechas solapadas entre sí.");
            }

            try
            {
                var result = await _service.RegistrarVariasFasesAsync(fases);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensaje = ex.Message });
            }
        }

        [HttpGet("listar/{iCodConvocatoria}")]
        public async Task<IActionResult> ListarPorConvocatoria(int iCodConvocatoria)
        {
            if (iCodConvocatoria <= 0) return BadRequest("El código de convocatoria es inválido.");

            var fases = await _service.ListarPorConvocatoriaAsync(iCodConvocatoria);
            if (fases == null || !fases.Any()) return NotFound("No se encontraron fases para esta convocatoria.");

            return Ok(fases);
        }

        [HttpDelete("{iCodFase}")]
        public async Task<IActionResult> EliminarFase(int iCodFase)
        {
            var result = await _service.EliminarFaseAsync(iCodFase);
            if (result.Codigo == 1) return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] ConvocatoriaFaseActualizarDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _service.ActualizarFaseAsync(dto);
            if (result.Codigo == 1) return Ok(result);

            return BadRequest(result);
        }
    }
}
