using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoConvocatoriaController : ControllerBase
    {
        private readonly TipoConvocatoriaService _service;

        public TipoConvocatoriaController(TipoConvocatoriaService service)
        {
            _service = service;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var tipos = await _service.ListarTiposAsync();
            return Ok(tipos);
        }
    }
}
