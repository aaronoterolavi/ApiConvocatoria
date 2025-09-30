using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController : ControllerBase
    {
        private readonly EstadoService _service;

        public EstadoController(EstadoService service)
        {
            _service = service;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var estados = await _service.ListarEstadosAsync();
            return Ok(estados);
        }
    }
}
