using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly TipoDocumentoService _service;

        public TipoDocumentoController(TipoDocumentoService service)
        {
            _service = service;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var tipos = await _service.ListarTiposDocumentoAsync();
            return Ok(tipos);
        }
    }
}
