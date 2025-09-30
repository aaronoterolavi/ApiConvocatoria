using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnidadZonalController : ControllerBase
    {
        private readonly UnidadZonalService _service;

        public UnidadZonalController(UnidadZonalService service)
        {
            _service = service;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var unidades = await _service.ListarUnidadesAsync();
            return Ok(unidades);
        }
    }
}
