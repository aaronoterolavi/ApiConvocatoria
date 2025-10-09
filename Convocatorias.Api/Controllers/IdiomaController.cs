using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdiomaController : ControllerBase
    {
        private readonly IdiomaService _service;

        public IdiomaController(IdiomaService service)
        {
            _service = service;
        }

        [HttpPost("insert")]
        public IActionResult Insert([FromBody] IdiomaDTO dto)
        {
            try
            {
                var id = _service.Insert(dto);
                return Ok(new { Message = "Idioma registrado correctamente.", iCodIdioma = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] IdiomaDTO dto)
        {
            try
            {
                _service.Update(dto);
                return Ok(new { Message = "Idioma actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok(new { Message = "Idioma eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
                return NotFound(new { Message = "Idioma no encontrado." });

            return Ok(result);
        }

        [HttpGet("getbypostulante/{idPostulante}")]
        public IActionResult GetByPostulante(int idPostulante)
        {
            var result = _service.GetByPostulante(idPostulante);
            return Ok(result);
        }
    }
}
