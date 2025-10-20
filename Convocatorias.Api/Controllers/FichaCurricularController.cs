using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FichaCurricularController : ControllerBase
    {
        private readonly FichaCurricularService _service;

        public FichaCurricularController(FichaCurricularService service)
        {
            _service = service;
        }

        public class GenerarFichaRequest
        {
            public int iCodUsuario { get; set; }
        }

        /// <summary>
        /// Genera el PDF del Anexo N°03 - Ficha de Resumen Curricular.
        /// </summary>
        [HttpPost("generar")]
        public async Task<IActionResult> GenerarFicha([FromBody] GenerarFichaRequest request)
        {
            if (request == null || request.iCodUsuario <= 0)
                return BadRequest("Debe especificar un código de usuario válido.");

            var pdfBytes = await _service.GenerarPdfAsync(request.iCodUsuario);
            var nombreArchivo = $"anexo_03_{DateTime.Now.Year}_ficha_de_resumen_curricular.pdf";

            // fuerza la descarga directa del archivo
            return File(pdfBytes, "application/pdf", nombreArchivo);
        }
    }
}
