using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoPostulacionController : ControllerBase
    {
        private readonly ArchivoPostulacionService _service;

        public ArchivoPostulacionController(ArchivoPostulacionService service)
        {
            _service = service;
        }

      //  [Authorize]
        [HttpPost("subir-multiple")]
        public async Task<IActionResult> SubirArchivos(
            [FromForm] List<IFormFile> files,
            [FromForm] List<int> formatos,
            [FromForm] int codPostulacion,
            [FromForm] int codUsuario)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No se enviaron archivos.");

            if (formatos == null || formatos.Count != files.Count)
                return BadRequest("Debe enviar un formato por cada archivo.");

            var archivos = files.Select((f, index) => new ArchivoPostulacionUploadDto
            {
                File = f,
                iCodFormato = formatos[index]
            }).ToList();

            var result = await _service.SubirYRegistrarArchivosAsync(archivos, codPostulacion, codUsuario);
            return Ok(result);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var result = await _service.ListarAsync();
            return Ok(result);
        }

        [HttpGet("listar-por-Postulante")]
        public async Task<IActionResult> ListarPorPostulacion([FromQuery] int? iCodConvocatoria = null,
     [FromQuery] int? iCodUsuario = null)
        {
            var result = await _service.ListarPorPostulacionAsync(iCodConvocatoria, iCodUsuario);
            return Ok(result);
        }
    }
}
