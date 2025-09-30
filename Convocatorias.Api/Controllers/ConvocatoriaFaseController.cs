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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.RegistrarFaseAsync(dto);
            return Ok(result);


        }

        // ✅ Nuevo endpoint para insertar varias fases
        [Authorize(Roles = "1")]
        [HttpPost("insertar-multiple")]
        public async Task<IActionResult> InsertarVarias([FromBody] List<ConvocatoriaFaseInsertarDto> fases)
        {
            if (fases == null || fases.Count == 0)
                return BadRequest("Debe proporcionar al menos una fase.");

            var result = await _service.RegistrarVariasFasesAsync(fases);
            return Ok(result);
        }

       // [Authorize] // todos los usuarios autenticados pueden consultar
        [HttpGet("listar/{iCodConvocatoria}")]
        public async Task<IActionResult> ListarPorConvocatoria(int iCodConvocatoria)
        {
            if (iCodConvocatoria <= 0)
                return BadRequest("El código de convocatoria es inválido.");

            var fases = await _service.ListarPorConvocatoriaAsync(iCodConvocatoria);

            if (fases == null || !fases.Any())
                return NotFound("No se encontraron fases para esta convocatoria.");

            return Ok(fases);
        }
    }
}
