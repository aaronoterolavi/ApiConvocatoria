using Convocatorias.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Convocatorias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormatoController : ControllerBase
    {
        private readonly FormatoService _service;

        public FormatoController(FormatoService service)
        {
            _service = service;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            var formatos = await _service.ListarFormatosAsync();
            return Ok(formatos);
        }
    }
}
