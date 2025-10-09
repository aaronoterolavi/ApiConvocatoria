using Microsoft.AspNetCore.Mvc;
using Convocatorias.Application.DTOs;
using Convocatorias.Application.Services;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfimaticaNivelIntermedioController : ControllerBase
    {
        private readonly OfimaticaNivelIntermedioService _service;

        public OfimaticaNivelIntermedioController(OfimaticaNivelIntermedioService service)
        {
            _service = service;
        }

        [HttpPost("insert")]
        public IActionResult Insert([FromBody] OfimaticaNivelIntermedioDTO dto)
        {
            try
            {
                int id = _service.Insert(dto);
                return Ok(new { success = true, iCodOfimaticaNivelIntermedio = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] bool bTieneConocimiento)
        {
            try
            {
                _service.Update(id, bTieneConocimiento);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("getByPostulante/{iCodPostulante}")]
        public IActionResult GetByPostulante(int iCodPostulante)
        {
            var result = _service.GetByPostulante(iCodPostulante);
            return result != null ? Ok(result) : NotFound();
        }
    }
}
