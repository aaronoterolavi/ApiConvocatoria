using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConvocatoriaEvaluadorController : ControllerBase
    {
        private readonly ConvocatoriaEvaluadorService _service;

        public ConvocatoriaEvaluadorController(ConvocatoriaEvaluadorService service)
        {
            _service = service;
        }

        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarEvaluador([FromQuery] int iCodConvocatoria, [FromQuery] int iCodUsuarioEvaluador, [FromQuery] int iCodUsuarioRegistra)
        {
            var mensaje = await _service.AsignarEvaluadorAConvocatoriaAsync(iCodConvocatoria, iCodUsuarioEvaluador, iCodUsuarioRegistra);
            return Ok(new { mensaje });
        }

        [HttpPost("desactivar")]
        public async Task<IActionResult> DesactivarEvaluador([FromQuery] int iCodConvocatoria, [FromQuery] int iCodUsuarioEvaluador, [FromQuery] int iCodUsuarioAccion)
        {
            var mensaje = await _service.DesactivarAsignacionEvaluadorAsync(iCodConvocatoria, iCodUsuarioEvaluador, iCodUsuarioAccion);
            return Ok(new { mensaje });
        }

        [HttpGet("evaluadores/{iCodConvocatoria}")]
        public async Task<ActionResult<List<ConvocatoriaEvaluadorDto>>> ListarEvaluadores(int iCodConvocatoria)
        {
            var lista = await _service.ListarEvaluadoresPorConvocatoriaAsync(iCodConvocatoria);
            return Ok(lista);
        }

        [HttpGet("convocatorias/{iCodUsuarioEvaluador}")]
        public async Task<ActionResult<List<ConvocatoriaEvaluadorDto>>> ListarConvocatorias(int iCodUsuarioEvaluador)
        {
            var lista = await _service.ListarConvocatoriasPorEvaluadorAsync(iCodUsuarioEvaluador);
            return Ok(lista);
        }
    }
}
