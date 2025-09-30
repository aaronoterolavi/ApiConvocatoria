using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArchivosConvocatoriaController : ControllerBase
    {
        private readonly ArchivoConvocatoriaService _service;

        public ArchivosConvocatoriaController(ArchivoConvocatoriaService service)
        {
            _service = service;
        }

        [Authorize(Roles = "1")]
        [HttpPost("subir-multiple")]
        public async Task<IActionResult> SubirArchivos(
        [FromForm] List<IFormFile> files,
        [FromForm] List<int> formatos,
        [FromForm] int codConvocatoria,
        [FromForm] int codUsuario)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No se enviaron archivos.");

            if (formatos == null || formatos.Count != files.Count)
                return BadRequest("Debe enviar un formato por cada archivo.");

            var archivos = files.Select((f, index) => new ArchivoConvocatoriaUploadDto
            {
                File = f,
                CodFormato = formatos[index]
            }).ToList();

            var result = await _service.SubirYRegistrarArchivosAsync(archivos, codConvocatoria, codUsuario);

            return Ok(result);
        }

        //[Authorize]
        [HttpGet("listar/{iCodConvocatoria}")]
        public async Task<IActionResult> ListarPorConvocatoria(
        int iCodConvocatoria,
        [FromQuery] int? iCodFormato = null)
        {
            if (iCodConvocatoria <= 0)
                return BadRequest("El código de convocatoria es inválido.");

            var archivos = await _service.ListarPorConvocatoriaAsync(iCodConvocatoria, iCodFormato);

            if (archivos == null || !archivos.Any())
                return NotFound("No se encontraron archivos para esta convocatoria.");

            return Ok(archivos);
        }

        [HttpDelete("{idAdjunto}")]
        public async Task<ActionResult<EliminarArchivoResponseDto>> EliminarArchivo(int idAdjunto)
        {
            var result = await _service.EliminarArchivoAsync(idAdjunto);

            if (result.Codigo == 1)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
